using GreenSa.Models.GolfModel;
using GreenSa.Models.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<Tuple<Shot, IEnumerable<Club>>> item;
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
            item = new ObservableCollection<Tuple<Shot, IEnumerable<Club>>>(partie.Shots.Select(s => new Tuple<Shot, IEnumerable<Club>>(s, partie.Clubs)));
            ListShotPartie.ItemsSource = item;
            
            //Définition du filtre pour la distance 
            Filter<Shot>.Filtre f2 = (c => true);
            //var ListDistance = new List<String>();
            //foreach (Shot shot in partie.getListShot())
            // { ListDistance.Add(Convert.ToString(shot.getDistance())); }
            // ListClubsPartie.ItemsSource = ListDistance;

            //Définition du filtre pour la liste déroulante ajouter
            Filter<Club>.Filtre filterlisteD = (c => true);
            //ListClubPartieNewShot.ItemsSource = partie.Clubs;

            //Définition du filtre pour le score du trou
            Filter<Shot>.Filtre filterScore = (c => true);

            int d = partie.Shots.Count;
           
            score.Text = Convert.ToString(d);
        }

        private async void validButtonClicked(object sender, EventArgs e)
        {
            if (partie.Shots.Count == 0)
            {
                await DisplayAlert("0 coups rentrés", "Impossible de valider avec aucun shot", "OK");
                return;
            }
            partie.holeFinished(save.IsToggled);
            await Navigation.PopModalAsync();
        }

        private void AddShotButtonClicked(object sender, EventArgs e)
        {
            Shot s = new Shot(Club.PUTTER, null, null, null, DateTime.Now);
            partie.Shots.Add(s);
            List<Club> l = new List<Club>();
            l.Add(Club.PUTTER);
            item.Add(new Tuple<Shot, IEnumerable<Club>>(s, l));
        }

    }
}