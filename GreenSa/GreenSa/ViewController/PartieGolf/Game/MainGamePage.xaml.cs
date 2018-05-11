using GreenSa.Models;
using GreenSa.Models.GolfModel;
using GreenSa.Models.Tools;
using GreenSa.Models.Tools.GPS_Maps;
using GreenSa.Models.Tools.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace GreenSa.ViewController.PartieGolf.Game
{
    /**
     * Page principale du jeu
     * Affiche la carte, boutons etc
     * Voir diagramme "onMainGamePageAppearing"
     * */
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainGamePage : ContentPage
    {
        
        public const String BEGIN_STATE = "BEGIN";
        public const String LOCK_STATE = "LOCK";
        public const String NEXT_STATE = "NEXT";
         
        private String state;
        private Partie partie;

        public MainGamePage(Partie partie)
        {
            InitializeComponent();
            state = BEGIN_STATE;
            this.partie = partie;
            map.MoveToRegion(
            MapSpan.FromCenterAndRadius(
                new Position(48.1116654, -1.6843768), Distance.FromMiles(30)));

            BindingContext = partie;
            partie.CurrentClub = partie.Clubs.First();
            ListClubPartie.SelectedItem = partie.CurrentClub;
            //message which come from the markerListenerDrag,
            //when the target pin is moved =>update the display of the distance
            MessagingCenter.Subscribe<CustomPin>(this,CustomPin.UPDATEDMESSAGE, (sender) => {
                updateDistance();
            });
           /* if (Navigation.NavigationStack.Count == 3)
            {*/
             /*   Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);*/
            //}
        }
        /**
        * Méthode qui s'execute automatiquement au chargement de la page
        * Affiche le résumé de la partie avec possibilité de correction et de ne pas enregistrer cette partie dans les stats
        * */
        async protected override void OnAppearing()
        {
            base.OnAppearing();

            if (partie.hasNextHole())
            {
                Hole nextHole = partie.getNextHole();
                map.setHolePosition(nextHole.Position);
                Title = "Trou " + partie.getIndexHole().Item1 + "/" + partie.getIndexHole().Item2;

                MyPosition position = new MyPosition(0, 0) ;
                bool success = false;
                do//make sure that the GPS is avaible
                {
                    try
                    {
                        position = await localize();
                        success = true;
                        map.setUserPosition(position);
                        partie.CurrentClub = partie.CurrentClub;//just to update hte circle

                    }
                    catch (NotAvaibleException e)
                    {
                        await DisplayAlert("Gps non disponible", "La localisation GPS n'est pas disponible, assurez-vous de l'avoir activé.", "OK");
                    }
                } while (!success);
                
                try
                {
                    WindInfo windInfo = WindService.getCurrentWindInfo();
                    windImg.Source = windInfo.icon;
                    forceVent.Text = windInfo.strength + " km/h";
                }
                catch (NotAvaibleException e)
                {
                    await DisplayAlert("Vent non disponible", "L'information concernant le vent n'est pas disponible", "OK");
                }

                updateDistance();
            
            }
            else
            {
                await Navigation.PushAsync(new GameFinishedPage(partie));
                return;
            }


        }
        public async Task<MyPosition> localize()
        {
            localisationState.Text = "Localisation en cours...";
            mainButton.IsEnabled = false;
            MyPosition position = await GpsService.getCurrentPosition();
            localisationState.Text = "";
            mainButton.IsEnabled = true;

            return position;
        }

        private void updateDistance()
        {
           distTotal.Text= string.Format("{0:0.0}", map.getDistanceUserHole()) +"m";
           distSplit.Text = string.Format("{0:0.0}", map.getDistanceUserTarget())  + "m / "+ string.Format("{0:0.0}", map.getDistanceTargetHole())+" m";
        }

        /**
* Méthode qui met à jour l'état du jeu 
*/
        private void setNextState()
        {
            string newLabel = "";
            switch (state)
            {
                case BEGIN_STATE:
                case NEXT_STATE:
                    state = LOCK_STATE;
                    newLabel = "LOCK";
                    break;
                case LOCK_STATE: state = NEXT_STATE;
                    newLabel = "NEXT";
                    DisplayAlert("Cible verrouillée", "Tirez puis déplacez-vous au niveau de la balle et appuyez sur le bouton 'NEXT'","OK");
                    break;
                default: newLabel = "??";
                    break;
            }

            mainButton.Text = newLabel;
        }

        /* Méthode qui s'execute au click sur le bouton principal.
        * **/
        private async void onMainButtonClicked(object sender, SelectedItemChangedEventArgs e)
        {
            if(state==LOCK_STATE)
            {
                map.lockTarget();
            }
            else if(state==NEXT_STATE)
            {
                MyPosition newUserPosition = await localize();
                MyPosition start = map.getUserPosition();
                partie.addPositionForCurrentHole(start,new MyPosition(map.TargetPin.Position.Latitude, map.TargetPin.Position.Longitude), newUserPosition);
                map.setUserPosition(newUserPosition);
                map.setTargetMovable();
            }
            setNextState();
        }

        /* Méthode qui s'execute au click sur le bouton de la selection du club.
        * **/
        private async void onClubSelectionClicked(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushModalAsync(new ClubSelectionInGamePage(partie));
        }

        /* Méthode qui s'execute au click sur le bouton principal.
         * **/
        private async void onHoleFinishedButtonClicked(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushModalAsync(new HoleFinishedPage(partie));

        }

        private async void onRelocalizeAction(object sender, EventArgs e)
        {
            MyPosition newUserPosition = await localize();
            map.setUserPosition(newUserPosition);
        }
        protected override bool OnBackButtonPressed()
        {            
                Navigation.PopToRootAsync();
                return true;
        }


        private void ListClubPartie_SelectedIndexChanged(object sender, EventArgs e)
        {
            partie.CurrentClub =(Club) ListClubPartie.SelectedItem;
        }

        private void moyenne_Toggled(object sender, ToggledEventArgs e)
        {
            MessagingCenter.Send<MainGamePage,bool>(this, "updateTheCircleVisbility", moyenne.IsToggled);
        }
    }
}