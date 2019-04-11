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

namespace GreenSa.ViewController.Play.Game
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
        private bool holFini;
        private double dUserTarget;

        public MainGamePage(Partie partie)
        {
            InitializeComponent();
            holFini = true;
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
            MessagingCenter.Subscribe<CustomPin>(this,CustomPin.UPDATEDMESSAGE,  (sender) => {
                 updateDistance(true);
            });
            MessagingCenter.Subscribe<System.Object>(this, CustomPin.UPDATEDMESSAGE_CIRCLE,  (sender) => {
                 updateDistance(true);
            });
            MessagingCenter.Subscribe<HoleFinishedPage,bool>(this, "ReallyFinit", (sender,val) => {
                holFini = val;
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
            if (!holFini)
                return;

            holFini = false;
            if (partie.hasNextHole())
            {
                Hole nextHole = partie.getNextHole();
                GestionGolfs.calculAverageAsync(partie.Clubs);
                map.setHolePosition(nextHole);
                Title = "Trou " + partie.getIndexHole().Item1 + "/" + partie.getIndexHole().Item2;

                MyPosition position = new MyPosition(0, 0) ;
                bool success = false;
                do//make sure that the GPS is avaible
                {
                    try
                    {
                        position = await localize();
                        success = true;
                        map.setUserPosition(position, partie.Shots.Count);
                        partie.CurrentClub = partie.CurrentClub;//just to update hte circle
                        partie.updateUICircle();

                    }
                    catch (Exception e)
                    {
                        await DisplayAlert("Gps non disponible", "La localisation GPS n'est pas disponible, assurez-vous de l'avoir activé.", "OK");
                        OnBackButtonPressed();
                    }
                } while (!success);

                 updateDistance();

                try
                {
                    WindService service = new WindService();

                    await Task.Run(async () =>
                    {
                        WindInfo windInfo = await service.getCurrentWindInfo(map.getUserPosition());
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await UpdateWindServiceUI(windInfo);
                        });
                        
                    });

                }
                catch (Exception e)
                {
                    await DisplayAlert("Vent non disponible", "L'information concernant le vent n'est pas disponible", "OK");
                }

            
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
            trouTerm.IsEnabled = false;
            MyPosition position = await GpsService.getCurrentPosition();
            localisationState.Text = "";
            mainButton.IsEnabled = true;
            trouTerm.IsEnabled = true;

            return position;
        }

        public async Task UpdateWindServiceUI(WindInfo windInfo)
        {
            try
            {
                windImg.Source = windInfo.icon;
                forceVent.Text = windInfo.strength + " km/h";
                await windImg.RotateTo(90 + windInfo.direction);
            }
            catch (Exception e)
            {

            }
        }

        private  void updateDistance(bool circle=false)
        {
                dUserTarget = map.getDistanceUserTarget();
                distTotal.Text = string.Format("{0:0.0}", map.getDistanceUserHole()) + "m";
                distSplit.Text = string.Format("{0:0.0}", dUserTarget) + "m / " + string.Format("{0:0.0}", map.getDistanceTargetHole()) + " m";
                if(circle)
                {
                    Club c =  GestionGolfs.giveMeTheBestClubForThatDistance(partie.Clubs, dUserTarget);
                    if (!c.Equals(ListClubPartie.SelectedItem))
                        ListClubPartie.SelectedItem = c;
                }
            
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
        private async void onMainButtonClicked(object sender, EventArgs e)
        {
            if(state==LOCK_STATE)
            {
                map.lockTarget();
            }
            else if(state==NEXT_STATE)
            {
                MyPosition newUserPosition = await localize();
                MyPosition start = map.getUserPosition();
                Shot s = partie.addPositionForCurrentHole(start,new MyPosition(map.TargetPin.Position.Latitude, map.TargetPin.Position.Longitude), newUserPosition);
                //this.DisplayAlert("", s.ShotType, "ok");
                map.setUserPosition(newUserPosition,partie.Shots.Count);
                map.setTargetMovable();
                if(moyenne.IsToggled)
                    partie.updateUICircle();

            }
            setNextState();
        }

        /* Méthode qui s'execute au click sur le bouton principal.
         * **/
        private async void onHoleFinishedButtonClicked(object sender, EventArgs e)
        {
            HoleFinishedPage p = new HoleFinishedPage(partie);
            await Navigation.PushModalAsync(p);
        }

        private async void onRelocalizeAction(object sender, EventArgs e)
        {
            MyPosition newUserPosition = await localize();
            map.setUserPosition(newUserPosition, partie.Shots.Count);
            if (moyenne.IsToggled)
                partie.updateUICircle();
        }
        protected override bool OnBackButtonPressed()
        {            
                Navigation.PopToRootAsync();
                return true;
        }


        private void ListClubPartie_SelectedIndexChanged(object sender, EventArgs e)
        {
            partie.CurrentClub =(Club) ListClubPartie.SelectedItem;
            if (moyenne.IsToggled)
                partie.updateUICircle();
        }

        private void moyenne_Toggled(object sender, EventArgs e)
        {
            MessagingCenter.Send<MainGamePage,bool>(this, "updateTheCircleVisbility", moyenne.IsToggled);
        }
    }
}