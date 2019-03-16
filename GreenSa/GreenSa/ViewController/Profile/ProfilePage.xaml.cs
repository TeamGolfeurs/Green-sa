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
using GreenSa.Models.ViewElements;

namespace GreenSa.ViewController.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();

            cielhaut.BackgroundColor = Color.FromHex("52D0DD");
            cielbas.BackgroundColor = Color.FromHex("52D0DD");

            nuage.HeightRequest = haut.Height.Value * 120;

            var flecheGestureRecognizer = new TapGestureRecognizer();
            flecheGestureRecognizer.Tapped += (s, e) =>
            {
                OnArrowClicked(s, e);
            };
            fleche.GestureRecognizers.Add(flecheGestureRecognizer);

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                OnOptionsClicked(s, e);
            };
            engrenage.GestureRecognizers.Add(tapGestureRecognizer);

            //buttons
            clubs.BackgroundColor = Color.FromRgba(0, 0, 0, 0.2);
            clubs.BorderWidth = 0;

            parties.BackgroundColor = Color.FromRgba(0, 0, 0, 0.2);
            parties.BorderWidth = 0;

            stats.BackgroundColor = Color.FromRgba(0, 0, 0, 0.2);
            stats.BorderWidth = 0;
        }

        /**
         * Méthode déclenchée au click sur le bouton "Jouer"
         * Redirige vers la page "GolfSelection"
         * */
        async private void OnClubsClicked(object sender, EventArgs e)
        {
            Partie p = new Partie();
            await Navigation.PushAsync(new MyClubs.ClubSelectionPage(p));
        }
        /**
         * Méthode déclenchée au click sur le bouton "Profil"
         * Redirige vers la page "profil"
         * */
        async private void OnPartiesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyGames.ViewPartieListPage());
        }
        /**
         * Méthode déclenchée au click sur le bouton "MesGolfs"
         * Redirige vers la page "GolfSelection"
         * */
        async private void OnStatsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Statistiques.SpecificStatistiques.DistanceClubPage());
        }
        /**
         * Méthode déclenchée au click sur le bouton "Option"
         * Redirige vers la page "OptionTabbedPage"
         * */
        async private void OnOptionsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Options.ProfileOptions());
        }

        /**
         * Méthode déclenchée au click sur le bouton "Back"
         * Redirige vers la page "MainMenu"
         * */
        async private void OnArrowClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}