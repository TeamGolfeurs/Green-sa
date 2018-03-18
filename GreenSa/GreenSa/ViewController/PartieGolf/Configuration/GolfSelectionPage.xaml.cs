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
        protected override void OnAppearing()
        {

            //Définition du filtre
            Filter<GolfCourse>.Filtre f = (c => true);

            //Recupere la liste des Golfs filtré par la classe GestionGolf
            ListGolfCourse.ItemsSource = GestionGolfs.getListGolfs(f);


            base.OnAppearing();
        }


        /*
         * Appelée à la sélection d'un golf
         * doit mettre à jour la partie, et ouvrir la page parametre suivant (ClubSelection)
         * */
         private async void onGolfSelection(object sender, SelectedItemChangedEventArgs e)
         {
            var g = e.SelectedItem as GolfCourse;
            p.setGolCourse(g);

            await Navigation.PushAsync(new ClubSelectionPage(p));
         }



    }
}