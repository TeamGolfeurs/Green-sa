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
            IEnumerable<Tuple<Shot, List<Club>>> item = partie.Shots.Select(s => new Tuple<Shot, List<Club>>(s, partie.Clubs));
            ListShotPartie.ItemsSource = item;
            
            //Définition du filtre pour la distance 
            Filter<Shot>.Filtre f2 = (c => true);
            //var ListDistance = new List<String>();
            //foreach (Shot shot in partie.getListShot())
            // { ListDistance.Add(Convert.ToString(shot.getDistance())); }
            // ListClubsPartie.ItemsSource = ListDistance;

            //Définition du filtre pour la liste déroulante ajouter
            Filter<Club>.Filtre filterlisteD = (c => true);
            ListClubPartie.ItemsSource = partie.Clubs;

            //Définition du filtre pour le score du trou
            Filter<Shot>.Filtre filterScore = (c => true);

            int d = partie.Shots.Count;
            if (isPutterAjoutShot.IsToggled)
            {
                d = +1;
                var club = ListShotPartie.SelectedItem as Club;
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