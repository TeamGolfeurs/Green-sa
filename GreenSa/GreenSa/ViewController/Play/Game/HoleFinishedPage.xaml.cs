using GreenSa.Models.GolfModel;
using GreenSa.Models.Profiles;
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
            this.partie.holeFinishedCount = 0;
            hole_finished.Margin = new Thickness(-8, responsiveDesign(19), 0, responsiveDesign(20));
            ListShotPartie.Margin = new Thickness(responsiveDesign(10), responsiveDesign(34), responsiveDesign(10), responsiveDesign(58));
            club.Margin = new Thickness(responsiveDesign(30), responsiveDesign(5), 0, 0);
            distance.Margin = new Thickness(responsiveDesign(140), responsiveDesign(5), 0, 0);
            pen.Margin = new Thickness(responsiveDesign(242), responsiveDesign(5), 0, 0);
            numero.Margin = new Thickness(responsiveDesign(25), responsiveDesign(25), 0, 0);
            par.Margin = new Thickness(responsiveDesign(205), responsiveDesign(25), 0, 0);
            score.Margin = new Thickness(responsiveDesign(265), responsiveDesign(25), 0, 0);
            parlegende.Margin = new Thickness(responsiveDesign(200), responsiveDesign(5), 0, 0);
            scorelegende.Margin = new Thickness(responsiveDesign(260), responsiveDesign(5), 0, 0);
            next.BackgroundColor = Color.FromHex("39B54A");
            next.Margin = new Thickness(0, responsiveDesign(5), responsiveDesign(5), responsiveDesign(5));
            stop.Margin = new Thickness(responsiveDesign(5), responsiveDesign(5), 0, responsiveDesign(5));
            next.WidthRequest = stop.Width;
            add.Margin = new Thickness(responsiveDesign(5), responsiveDesign(15), responsiveDesign(5), 0);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            item = new ObservableCollection<Tuple<Shot, IEnumerable<Club>>>(partie.Shots.Select(s => new Tuple<Shot, IEnumerable<Club>>(s, partie.Clubs)));
            ListShotPartie.ItemsSource = item;
            numero.Text = "Trou n°" + partie.getIndexHole().Item1.ToString() + " :";
            hole_finished.Text = "TROU N°" + partie.getIndexHole().Item1.ToString() + " TERMINE !";
            par.Text = partie.getNextHole().Par.ToString();
            updateScoreText();
            if (!partie.hasNextHole())
            {
                next.Text = "Fin de partie";
            }
        }

        private int responsiveDesign(int pix)
        {
            return (int)((pix * 4.1 / 1440.0) * Application.Current.MainPage.Width);
        }

        private void updateScoreText()
        {
            if (partie.getCurrentScore() >= 0)
            {
                score.Text = "+"+partie.getCurrentScore().ToString();
            }
            else
            {
                score.Text = partie.getCurrentScore().ToString();
            }
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
                next.IsEnabled = false;
                Profil profil = StatistiquesGolf.getProfil();
                partie.holeFinished(profil.SaveStats);
                await Navigation.PopModalAsync();
                next.IsEnabled = true;
            }
        }

        private async void stopPartieClicked(object sender, EventArgs e)
        {
            if (partie.Shots.Count == 0)
            {
                await this.DisplayAlert("Erreur", "Vous devez avoir joué ce trou pour arrêter la partie ici", "Ok");
            } else
            {
                var confirm = await this.DisplayAlert("Arrêter la partie", "Voulez vous vraiment arrêter la partie après ce trou ?", "Oui", "Non");
                if (confirm)
                {
                    MessagingCenter.Send<HoleFinishedPage, int>(this, "ReallyFinit", 2);
                    Profil profil = StatistiquesGolf.getProfil();
                    partie.holeFinished(profil.SaveStats);
                    await Navigation.PopModalAsync();
                }
            }
            
        }

        /*private void OnClubChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            DateTime id = picker.DateId;
            Shot shot = partie.Shots.Find(s => s.Date.Equals(id));
            shot.UpdateShotType();
        }*/


        private void OnPenalityCompleted(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            picker.TextColor = Color.White;
            var tgr = picker.GestureRecognizers[0] as TapGestureRecognizer;
            DateTime id = (DateTime)tgr.CommandParameter;
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
            DateTime id = (DateTime) tgr.CommandParameter;
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