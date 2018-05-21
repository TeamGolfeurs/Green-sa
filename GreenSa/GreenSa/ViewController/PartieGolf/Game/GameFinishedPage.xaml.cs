using GreenSa.Models.GolfModel;
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
    public partial class GameFinishedPage : ContentPage
    {
        private Partie partie;
        public GameFinishedPage(Partie partie)
        {
            InitializeComponent();
            this.partie = partie;
        }

        /**
       * Méthode qui s'execute automatiquement au chargement de la page
       * Affiche le résumé de la partie avec possibilité de correction et de ne pas enregistrer cette partie dans les stats
       * */
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void OnGoBackClicked(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PopToRootAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopToRootAsync();
            return true;
        }
    }
}