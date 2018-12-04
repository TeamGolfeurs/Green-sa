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
    public partial class ClubSelectionInGamePage : ContentPage
    {
        Partie p;
        public ClubSelectionInGamePage(Partie partie)
        {
            InitializeComponent();
            p = partie;
        }
       
        /**
       * Méthode qui s'execute automatiquement au chargement de la page
       * Permet la selection du club à utiliser pour le prochain coup
       * Cette méthode utilise la méthode getListClub() de Partie pour afficher la liste
       * */
        protected override void OnAppearing()
        {
           base.OnAppearing();
            //Définition du filtre
            Func<Club, bool> f = (c => true);
            ListClubsPartie.ItemsSource = p.Clubs;
        }

        /**
         * Méthode activée au clic sur un élement de la liste
         * set le currentClub de la Partie à l'élément choisi
         */
        private async void onClubClicked(object sender, SelectedItemChangedEventArgs e)
        {
            var club = ListClubsPartie.SelectedItem as Club;
            p.setCurrentClub(club);
            await Navigation.PopModalAsync();
        }
    }
}
