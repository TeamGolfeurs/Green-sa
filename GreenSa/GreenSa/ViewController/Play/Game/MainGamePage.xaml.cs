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
         
        private int state; //0 pour prêt à taper et 1 pour prêt à localiser
        private Partie partie;
        private bool holFini;
        private double dUserTarget;

        public MainGamePage(Partie partie)
        {
            InitializeComponent();

            forceVent.Margin = new Thickness(35, 10, 0, 0);
            windImg.Margin = new Thickness(35, 5, 0, 0);
            windImg.HeightRequest = responsiveDesign(25);
            ball.HeightRequest = responsiveDesign(91);
            ball.Margin = responsiveDesign(10);
            radar.HeightRequest = responsiveDesign(92);
            radar.Margin = responsiveDesign(10);
            load.HeightRequest = responsiveDesign(92);
            load.Margin = responsiveDesign(10);
            clubs.HeightRequest = responsiveDesign(45);
            clubs.Margin = new Thickness(responsiveDesign(15), 0, 0, 0);
            ball_in.HeightRequest = responsiveDesign(45);
            ball_in.Margin = new Thickness(0, 0, responsiveDesign(14), responsiveDesign(7));
            numclub.TextColor = Color.FromHex("009245");
            numclub.Margin = new Thickness(responsiveDesign(34), 0, 0, responsiveDesign(16));

            numcoup.FontSize = responsiveDesign(40);
            numcoup.Margin = responsiveDesign(-5);
            parTrou.FontSize = responsiveDesign(15);
            parTrou.Margin = responsiveDesign(38);
            distTrou.FontSize = responsiveDesign(17);
            distTrou.Margin = new Thickness(0, responsiveDesign(10), responsiveDesign(21), 0);
            forceVent.FontSize = responsiveDesign(12);
            forceVent.Margin = new Thickness(responsiveDesign(28), responsiveDesign(23), 0, 0);
            windImg.Margin = new Thickness(responsiveDesign(42), responsiveDesign(8), 0, 0);
            windImg.HeightRequest = responsiveDesign(15);
            numclub.IsEnabled = false;

            load.IsEnabled = false;
            load.IsVisible = false;
            radar.IsEnabled = false;
            radar.IsVisible = false;
            ball.IsEnabled = true;
            ball.IsVisible = true;

            holFini = true;
            state = 0;
            this.partie = partie;
            map.MoveToRegion(
            MapSpan.FromCenterAndRadius(
                    new Position(48.1116654, -1.6843768), Distance.FromMiles(30)));

            BindingContext = partie;
            partie.CurrentClub = partie.Clubs.First();
            //ListClubPartie.SelectedItem = partie.CurrentClub;
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
                numcoup.Text = partie.getIndexHole().Item1.ToString();
                MyPosition position = new MyPosition(0, 0) ;
                bool success = false;
                do//make sure that the GPS is avaible
                {
                    try
                    {
                        position = await localize();
                        success = true;
                        map.setUserPosition(position, partie.Shots.Count);
                        partie.CurrentClub = partie.CurrentClub;//just to update the circle
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

        private int responsiveDesign(int pix)
        {
            return (int)((pix * 4.1 / 1440.0) * Application.Current.MainPage.Width);
        }

        public async Task<MyPosition> localize()
        {
            load.IsEnabled = true;
            load.IsVisible = true;
            radar.IsEnabled = false;
            radar.IsVisible = false;
            ball.IsEnabled = false;
            ball.IsVisible = false;
            MyPosition position = await GpsService.getCurrentPosition();
            load.IsEnabled = false;
            load.IsVisible = false;
            radar.IsEnabled = false;
            radar.IsVisible = false;
            ball.IsEnabled = true;
            ball.IsVisible = true;

            return position;
        }

        public async Task UpdateWindServiceUI(WindInfo windInfo)
        {
            try
            {
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
                distTrou.Text = string.Format("{0:0.0}", map.getDistanceUserHole()) + "m";
                //distSplit.Text = string.Format("{0:0.0}", dUserTarget) + "m / " + string.Format("{0:0.0}", map.getDistanceTargetHole()) + " m";
                if(circle)
                {
                    Club c =  GestionGolfs.giveMeTheBestClubForThatDistance(partie.Clubs, dUserTarget);
                    //if (!c.Equals(ListClubPartie.SelectedItem))
                    //    ListClubPartie.SelectedItem = c;
                }
            
        }

        /**
* Méthode qui met à jour l'état du jeu quand on clique sur le boutton principal
*/
        private async void onMainButtonClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(state);
            switch (state)
            {
                case 0:
                    load.IsEnabled = false;
                    load.IsVisible = false;
                    radar.IsEnabled = true;
                    radar.IsVisible = true;
                    ball.IsEnabled = false;
                    ball.IsVisible = false;
                    map.lockTarget();
                    state = 1;
                    break;

                case 1:
                    MyPosition newUserPosition = await localize();
                    MyPosition start = map.getUserPosition();
                    partie.addPositionForCurrentHole(start, new MyPosition(map.TargetPin.Position.Latitude, map.TargetPin.Position.Longitude), newUserPosition);
                    map.setUserPosition(newUserPosition, partie.Shots.Count);
                    map.setTargetMovable();
                    //if(moyenne.IsToggled)
                    //partie.updateUICircle();
                    load.IsEnabled = false;
                    load.IsVisible = false;
                    radar.IsEnabled = false;
                    radar.IsVisible = false;
                    ball.IsEnabled = true;
                    ball.IsVisible = true;
                    state = 0;
                    break;

                default: //par defaut prêt à taper
                    load.IsEnabled = false;
                    load.IsVisible = false;
                    radar.IsEnabled = false;
                    radar.IsVisible = false;
                    ball.IsEnabled = true;
                    ball.IsVisible = true;
                    state = 0;
                    break;
            }
        }
        private void changeClubIcon()
        {
            switch (partie.CurrentClub.Name)
            {
                case "Bois3":
                    clubs.Source = "bois.png";
                    numclub.Text = "3";
                    break;
                case "Bois5":
                    clubs.Source = "bois.png";
                    numclub.Text = "5";
                    break;
                case "Driver":
                    clubs.Source = "bois.png";
                    numclub.Text = "D";
                    break;
                case "Putter":
                    clubs.Source = "put.png";
                    numclub.Text = "";
                    break;
                case "Fer3":
                    clubs.Source = "fer.png";
                    numclub.Text = "3";
                    break;
                case "Fer4":
                    clubs.Source = "fer.png";
                    numclub.Text = "4";
                    break;
                case "Fer5":
                    clubs.Source = "fer.png";
                    numclub.Text = "5";
                    break;
                case "Fer6":
                    clubs.Source = "fer.png";
                    numclub.Text = "6";
                    break;
                case "Fer7":
                    clubs.Source = "fer.png";
                    numclub.Text = "7";
                    break;
                case "Fer8":
                    clubs.Source = "fer.png";
                    numclub.Text = "8";
                    break;
                case "Fer9":
                    clubs.Source = "fer.png";
                    numclub.Text = "9";
                    break;
                case "Hybride":
                    clubs.Source = "fer.png";
                    numclub.Text = "H";
                    break;
                case "Sandwedge":
                    clubs.Source = "fer.png";
                    numclub.Text = "SW";
                    break;
                case "Pitching":
                    clubs.Source = "fer.png";
                    numclub.Text = "P";
                    break;
                default:
                    clubs.Source = "fer.png";
                    numclub.Text = "3";
                    break;

            }
        }
        /* Méthode qui s'execute au click sur le bouton de la selection du club.
        * **/
        private async void onClubSelectionClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ClubSelectionInGamePage(partie));
            changeClubIcon();
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
            //if (moyenne.IsToggled)
            //    partie.updateUICircle();
        }
        protected override bool OnBackButtonPressed()
        {            
                Navigation.PopToRootAsync();
                return true;
        }


        private void ListClubPartie_SelectedIndexChanged(object sender, EventArgs e)
        {
            //partie.CurrentClub =(Club) ListClubPartie.SelectedItem;
            //if (moyenne.IsToggled)
            //    partie.updateUICircle();
        }

        private void moyenne_Toggled(object sender, EventArgs e)
        {
            //MessagingCenter.Send<MainGamePage,bool>(this, "updateTheCircleVisbility", moyenne.IsToggled);
        }
    }
}