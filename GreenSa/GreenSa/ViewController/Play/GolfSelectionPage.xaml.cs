using GreenSa.Models;
using GreenSa.Models.GolfModel;
using GreenSa.Models.Tools;
using GreenSa.ViewController.Profile.MyGames;
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
        private bool BackButtonPressed;

        public GolfSelectionPage(Partie partie)
        {
            InitializeComponent();
            p = partie;
            golfCourseStatPage = null;
            this.BackButtonPressed = false;
        }

        public GolfSelectionPage()
        {
            InitializeComponent();
            p = null;
            golfCourseStatPage = null;
            this.BackButtonPressed = false;
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
                    List<ScorePartie> scoreParties = await StatistiquesGolf.getNotFinishedGames(g);
                    if (scoreParties.Count > 0)
                    {
                        var newGame = await this.DisplayAlert("Lancement d'une partie", "Voulez vous lancer une nouvelle partie ou charger une existante ?", "Nouvelle partie", "Charger une existante");
                        if (newGame)
                        {
                            await Navigation.PushAsync(new Game.MainGamePage(p), false);
                        }
                        else
                        {
                            if (!this.BackButtonPressed)
                            {
                                await Navigation.PushAsync(new ViewPartieListPage(2, scoreParties, p), false);
                                this.BackButtonPressed = false;
                            }
                        }
                    } else
                    {
                        await Navigation.PushAsync(new Game.MainGamePage(p), false);
                    }
                } 
            }
         }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            System.Diagnostics.Debug.WriteLine("OnBackButtonPressed");
            this.BackButtonPressed = true;
            return true;
        }

    }
}