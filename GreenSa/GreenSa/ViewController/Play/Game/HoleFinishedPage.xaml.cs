using GreenSa.Models.GolfModel;
using GreenSa.Models.Tools;
using GreenSa.Models.ViewElements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenSa.ViewController.Play.Game
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
            item = new ObservableCollection<Tuple<Shot, IEnumerable<Club>>>(partie.Shots.Select(s => new Tuple<Shot, IEnumerable<Club>>(s, partie.Clubs)));
            ListShotPartie.ItemsSource = item;
            updateScoreText();
        }

        private void updateScoreText()
        {
            score.Text = partie.getCurrentScore() + "";
        }

        private async void validButtonClicked(object sender, EventArgs e)
        {
            if (partie.Shots.Count == 0)
            {
                await DisplayAlert("0 coups rentrés", "Impossible de valider avec aucun shot", "OK");
                return;
            }
            var confirm = await this.DisplayAlert("Trou sivant", "Passer au trou suivant ?", "Oui", "Non");
            if (confirm)
            {
                MessagingCenter.Send<HoleFinishedPage, int>(this, "ReallyFinit", 1);
                validNext.IsEnabled = false;
                validNext.Text = "En cours";
                partie.holeFinished(true);
                await Navigation.PopModalAsync();
                validNext.IsEnabled = true;
                validNext.Text = "Passer au trou suivant";
            }
        }

        private async void stopPartieClicked(object sender, EventArgs e)
        {
            if (this.partie.holeFinishedCount == 0)
            {
                await this.DisplayAlert("Erreur", "Vous devez au moins avoir fait 1 trou pour enregistrer une partie", "Ok");
            } else
            {
                var confirm = await this.DisplayAlert("Arrêter la partie", "Voulez vous vraiment arrêter la partie maintenant ? (ce trou ne sera pas compté dans les statistiques)", "Oui", "Non");
                if (confirm)
                {
                    MessagingCenter.Send<HoleFinishedPage, int>(this, "ReallyFinit", 2);
                    await Navigation.PopModalAsync();
                }
            }
            
        }


        private void OnPenalityCompleted(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            var tgr = picker.GestureRecognizers[0] as TapGestureRecognizer;
            var id = (DateTime)tgr.CommandParameter;
            Shot shot = partie.Shots.Find(s => s.Date.Equals(id));
            int penalityCount = 0;
            if (picker.SelectedItem != null)
            {
                penalityCount = (int) picker.SelectedItem;
            }
            if (shot != null)
            {
                shot.SetPenalityCount(penalityCount);
                this.updateScoreText();
            }
        }


        private async void OnShotDeletedClicked(object sender, EventArgs e)
        {
            var image = sender as Image;
            var tgr = image.GestureRecognizers[0] as TapGestureRecognizer;
            var id = (DateTime) tgr.CommandParameter;
            Shot shot = partie.Shots.Find(s => s.Date.Equals(id));
            var confirm = true;
            if(!shot.Club.IsPutter())
            {
                confirm = await this.DisplayAlert("Suppression", "Voulez vous vraiment supprimer ce coup ?", "Oui", "Non");
            }
            if (confirm)
            {
                item.Remove(item.ToList().Find(tuple => tuple.Item1.Equals(shot)));

                partie.Shots.Remove(shot);
                updateScoreText();
            }
        }


        private void AddShotButtonClicked(object sender, EventArgs e)
        {
            Shot s = new Shot(Club.PUTTER, null, null, null, DateTime.Now);
            partie.Shots.Add(s);
            List<Club> l = new List<Club>();
            l.Add(Club.PUTTER);
            item.Add(new Tuple<Shot, IEnumerable<Club>>(s, l));
            updateScoreText();
        }

        protected override bool OnBackButtonPressed()
        {
            MessagingCenter.Send<HoleFinishedPage, int>(this, "ReallyFinit",  0);
            return base.OnBackButtonPressed();
        }

        

    }
}