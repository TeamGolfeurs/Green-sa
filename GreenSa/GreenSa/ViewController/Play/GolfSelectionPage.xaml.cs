using GreenSa.Models;
using GreenSa.Models.GolfModel;
using GreenSa.Models.Tools;
using GreenSa.ViewController.Profile.Statistiques.StatistiquesGolfCourse;
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
        private GolfCourseStatPage golfCourseStatPage;

        public GolfSelectionPage(Partie partie)
        {
            InitializeComponent();
            p = partie;
            golfCourseStatPage = null;
        }

        public GolfSelectionPage()
        {
            InitializeComponent();
            p = null;
            golfCourseStatPage = null;
        }

        /**
         * Méthode qui s'execute automatiquement au chargement de la page
         * Demande à la classe GestionGolf
         * */
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            titre.FontSize = 30;
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
            if (p == null)
            {
                if (this.golfCourseStatPage == null)
                {
                    this.golfCourseStatPage = new GolfCourseStatPage(g);
                } else
                {
                    this.golfCourseStatPage.changeGolfCourse(g);
                }
                await Navigation.PushModalAsync(this.golfCourseStatPage, true);
            } else
            {
                p.GolfCourse = g;
                Func<Club, bool> f = (c => true);
                List<Club> clubselected = await GestionGolfs.getListClubsAsync(f);
                clubselected.RemoveAll(c => c.selected == false);
                if (clubselected.Count == 0)
                {
                    await this.DisplayAlert("Erreur", "Vous n'avez aucun club dans votre sac. Veuillez en choisir au moins un dans le page 'Profil'", "ok");
                } else
                {
                    p.Clubs = clubselected;
                    await Navigation.PushAsync(new ViewController.Play.Game.MainGamePage(p), false);
                } 
            }
         }



    }
}