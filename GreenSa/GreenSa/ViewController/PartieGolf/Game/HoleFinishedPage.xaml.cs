using GreenSa.Models.GolfModel;
using GreenSa.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenSa.ViewController.PartieGolf.Game
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HoleFinishedPage : ContentPage
    {
        private Partie partie;

        public HoleFinishedPage(Partie partie)
        {
            InitializeComponent();
            this.partie = partie;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //Définition du filtre pour la liste des clubs des shots de la partie
            Filter<Club>.Filtre f = (c => true);
            var ListClubShots = new List<Club>();
            foreach (Shot shot in partie.getListShot())
            {
                ListClubShots.Add(shot.getClubDuShot());
            }
            ListClubsPartie.ItemsSource = ListClubShots;


            //Définition du filtre pour la distance 
            Filter<Shot>.Filtre f2 = (c => true);
            //var ListDistance = new List<String>();
            //foreach (Shot shot in partie.getListShot())
            // { ListDistance.Add(Convert.ToString(shot.getDistance())); }
            // ListClubsPartie.ItemsSource = ListDistance;

            //Définition du filtre pour la liste déroulante ajouter
            Filter<Club>.Filtre filterlisteD = (c => true);
            ListClubPartie.ItemsSource = partie.getListClub();

            //Définition du filtre pour le score du trou
            Filter<Shot>.Filtre filterScore = (c => true);

            int d = partie.getListShot().Count;
            if (AjoutShot.IsToggled)
            {
                d = +1;
                var club = ListClubsPartie.SelectedItem as Club;
                ListClubShots.Add(club);
            }
            score.Text = Convert.ToString(d);
        }

        private void validButtonClicked(object sender, EventArgs e)
        {
            partie.holeFinished(save.IsToggled);
            Navigation.PopModalAsync();
        }
    }
}