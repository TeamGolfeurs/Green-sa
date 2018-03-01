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
    public partial class ClubSelectionInGamePage : ContentPage
    {
        public ClubSelectionInGamePage(Partie partie)
        {

            InitializeComponent();
        }

        /**
       * Méthode qui s'execute automatiquement au chargement de la page
       * Permet la selection du club à utiliser pour le prochain coup
       * Cette méthode utilise la méthode getListClub() de Partie pour afficher la liste
       * */
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        /**
         * Méthode activé au clic sur un élement de la liste
         * set le currentClub de la Partie à l'élément choisi
         */
        private async void onClubClicked(object sender, SelectedItemChangedEventArgs e)
        {
        }
    }
}
 