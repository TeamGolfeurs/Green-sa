using GreenSa.Models;
using GreenSa.Models.GolfModel;
using GreenSa.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenSa.ViewController.PartieGolf.Configuration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GolfSelectionPage : ContentPage
    {
        public GolfSelectionPage(Partie partie)
        {
            InitializeComponent();
        }

        /**
         * Méthode qui s'execute automatiquement au chargement de la page
         * Demande à la classe GestionGolf
         * */
        protected override void OnAppearing()
        {
            Filter<GolfCourse>.Filtre f = (c => true);
            //get the list from

            base.OnAppearing();
        }

        /*
         * Appelée à la sélection d'un golf
         * doit mettre à jour la partie, et ouvrir la page parametre suivant (ClubSelection)
         * */
        private async void onGolfSelection(object sender, SelectedItemChangedEventArgs e)
        {
        }


    }
}