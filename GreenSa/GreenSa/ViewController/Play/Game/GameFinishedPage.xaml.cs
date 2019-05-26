using GreenSa.Models.GolfModel;
using GreenSa.Models.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenSa.ViewController.Profile.MyGames;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenSa.ViewController.Play.Game
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameFinishedPage : ContentPage
    {
        private Partie partie;
        public GameFinishedPage(Partie partie)
        {
            InitializeComponent();
            this.partie = partie;
            coupe.HeightRequest = responsiveDesign(80);
            coupe.Margin = new Thickness(0, responsiveDesign(10), 0, 0);
            numero.Margin = new Thickness(responsiveDesign(25), responsiveDesign(25), 0, 0);
            par.Margin = new Thickness(responsiveDesign(205), responsiveDesign(25), 0, 0);
            score.Margin = new Thickness(responsiveDesign(265), responsiveDesign(25), 0, 0);
            parlegende.Margin = new Thickness(responsiveDesign(200), responsiveDesign(5), 0, 0);
            scorelegende.Margin = new Thickness(responsiveDesign(260), responsiveDesign(5), 0, 0);
        }

        /**
         * This method is executed when the page is loaded
         * */
        protected override void OnAppearing()
        {
            base.OnAppearing();
            updateScoreText();
            updateParText();
            numero.Text = partie.Holes.Count.ToString() + " trous :";
            System.Diagnostics.Debug.WriteLine(partie.ScoreOfThisPartie.scoreHoles.Count);
        }

        private void updateScoreText()
        {
            int sco = 0;
            foreach (ScoreHole sh in partie.ScoreOfThisPartie.scoreHoles)
            {
                sco += sh.Score;
            }
            if (sco >= 0)
            {
                score.Text = "+" + sco.ToString();
            }
            else
            {
                score.Text = sco.ToString();
            }
        }

        private void updateParText()
        {

            int count = 0;
            foreach (ScoreHole sh in partie.ScoreOfThisPartie.scoreHoles)
            {
                count += sh.Hole.Par;
            }
                par.Text = count.ToString();
        }

        /**
         * This method is called when the button to go back to main menu is clicked
         */
        private async void OnGoBackClicked(object sender, EventArgs e)
        {
            Profil profil = StatistiquesGolf.getProfil();
            await partie.gameFinished(profil.SaveStats);
            await Navigation.PopToRootAsync();
        }

        private async void OnCardClicked(object sender, EventArgs e)
        {
            Profil profil = StatistiquesGolf.getProfil();
            await partie.gameFinished(profil.SaveStats);
            await Navigation.PushModalAsync(new DetailsPartiePage(partie.ScoreOfThisPartie));
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private int responsiveDesign(int pix)
        {
            return (int)((pix * 4.1 / 1440.0) * Application.Current.MainPage.Width);
        }
    }
}