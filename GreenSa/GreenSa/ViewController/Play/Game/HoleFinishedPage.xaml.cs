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
            score.Text = (partie.Shots.Count-partie.getNextHole().Par)+"" ;
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
                MessagingCenter.Send<HoleFinishedPage, bool>(this, "ReallyFinit", true);
                validNext.IsEnabled = false;
                validNext.Text = "En cours";
                partie.holeFinished(save.IsToggled);
                await Navigation.PopModalAsync();
                validNext.IsEnabled = true;
                validNext.Text = "Valid";
            }
        }

        private void AddShotButtonClicked(object sender, EventArgs e)
        {
            Shot s = new Shot(Club.PUTTER, null, null, null, DateTime.Now);
            partie.Shots.Add(s);
            List<Club> l = new List<Club>();
            l.Add(Club.PUTTER);
            item.Add(new Tuple<Shot, IEnumerable<Club>>(s, l));
            score.Text = (partie.Shots.Count - partie.getNextHole().Par) + "";

        }

        protected override bool OnBackButtonPressed()
        {
            MessagingCenter.Send<HoleFinishedPage, bool>(this, "ReallyFinit",false);
            return base.OnBackButtonPressed();
        }

    }
}