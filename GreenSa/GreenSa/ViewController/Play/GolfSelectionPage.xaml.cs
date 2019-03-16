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

namespace GreenSa.ViewController.Play
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GolfSelectionPage : ContentPage
    {
        Partie p;
        public GolfSelectionPage(Partie partie)
        {
            InitializeComponent();
            p = partie;

        }

        /**
         * Méthode qui s'execute automatiquement au chargement de la page
         * Demande à la classe GestionGolf
         * */
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            //Définition du filtre
            Func<GolfCourse, bool> f = (c => true);
            //Recupere la liste des Golfs filtré par la classe GestionGolf
            List<GolfCourse> res = await GestionGolfs.getListGolfsAsync(f);
            ListGolfCourse.ItemsSource = res; 
        }


        /*
         * Appelée à la sélection d'un golf
         * doit mettre à jour la partie, et ouvrir la page parametre suivant (ClubSelection)
         * */
         private async void onGolfSelection(object sender, EventArgs e)
         {
            var g = ListGolfCourse.SelectedItem as GolfCourse;
            p.GolfCourse = g;
            await Navigation.PushAsync(new ViewController.Profile.MyClubs.ClubSelectionPage(p),false);
         }



    }
}