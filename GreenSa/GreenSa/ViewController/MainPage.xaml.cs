using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GreenSa.ViewController.Play;
using GreenSa.ViewController.Option;
using GreenSa.ViewController.MesGolfs;
using GreenSa.ViewController.Profile;
using GreenSa.Models.GolfModel;

namespace GreenSa.ViewController
{
    /**
     *  Page d'accueil 
     *  Contient  :
     *          -Bouton option
     *          -Titre
     *          -Bouton Jouer
     *          -Bouton Profil
     *          -Bouton Mes Golfs
     */
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            cielhaut.BackgroundColor = Color.FromHex("52D0DD");
            cielbas.BackgroundColor = Color.FromHex("52D0DD");

            nuage.HeightRequest = haut.Height.Value * 100;

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                OnOptionsClicked(s, e);
            };
            engrenage.GestureRecognizers.Add(tapGestureRecognizer);

        }

        /**
         * Méthode déclenchée au click sur le bouton "Jouer"
         * Redirige vers la page "GolfSelection"
         * */
        async private void OnPlayClicked(object sender, EventArgs e)
        {
            Partie partie = new Partie();
            await Navigation.PushAsync(new Play.GolfSelectionPage(partie));
        }
        /**
         * Méthode déclenchée au click sur le bouton "Profil"
         * Redirige vers la page "profil"
         * */
        async private void OnProfilClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage());
        }
        /**
         * Méthode déclenchée au click sur le bouton "MesGolfs"
         * Redirige vers la page "GolfSelection"
         * */
        async private void OnGolfClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GolfsManager());
        }
        /**
         * Méthode déclenchée au click sur le bouton "Option"
         * Redirige vers la page "OptionTabbedPage"
         * */
        async private void OnOptionsClicked(object sender, EventArgs e){ 
            await Navigation.PushAsync(new DatabaseDeletionPage());
        }
    }
}
