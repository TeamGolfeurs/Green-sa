using GreenSa.Models.GolfModel;
using GreenSa.Models.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        /**
         * This method is executed when the page is loaded
         * */
        protected override void OnAppearing()
        {
            base.OnAppearing();
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

        /**
         * Cancels the back button default action
         */
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}