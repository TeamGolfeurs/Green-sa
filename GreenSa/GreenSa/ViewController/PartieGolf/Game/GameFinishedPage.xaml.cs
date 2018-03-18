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
        public GameFinishedPage(Partie partie)
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

        /* Méthode qui s'execute au click sur le bouton valider.
         * met à jour la partie (ou non si c'est bind)
         * Méthode qui appelle la méthode holeFinished de partie qui se chargera de l'enregistrer pour les stats
         * **/
        private async void onValidClicked(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}