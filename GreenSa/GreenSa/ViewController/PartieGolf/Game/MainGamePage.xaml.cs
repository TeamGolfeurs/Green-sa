using GreenSa.Models;
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
    /**
     * Page principale du jeu
     * Affiche la carte, boutons etc
     * Voir diagramme "onMainGamePageAppearing"
     * */
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainGamePage : ContentPage
    {


        public MainGamePage(Partie partie)
        {
            InitializeComponent();
        }
        /**
        * Méthode qui s'execute automatiquement au chargement de la page
        * Affiche le résumé de la partie avec possibilité de correction et de ne pas enregistrer cette partie dans les stats
        * */
        protected override void OnAppearing()
        {

            base.OnAppearing();
        }
        /**
        * Méthode qui met à jour l'état du jeu 
        */
        private void setNextState()
        {
            //if... (voir diagramme)
        }

        /* Méthode qui s'execute au click sur le bouton principal.
        * **/
        private async void onMainButtonClicked(object sender, SelectedItemChangedEventArgs e)
        {

        }

        /* Méthode qui s'execute au click sur le bouton de la selection du club.
        * **/
        private async void onClubSelectionClicked(object sender, SelectedItemChangedEventArgs e)
        {

        }

          /* Méthode qui s'execute au click sur le bouton principal.
           * **/
        private async void onHoleFinishedButtonClicked(object sender, SelectedItemChangedEventArgs e)
        {
               
        }

    }
}