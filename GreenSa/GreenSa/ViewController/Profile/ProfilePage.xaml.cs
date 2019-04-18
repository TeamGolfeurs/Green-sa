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
using GreenSa.Models.Profiles;
using GreenSa.Models.ViewElements;
using SQLite;
using System.Collections.ObjectModel;
using GreenSa.Persistence;
using GreenSa.ViewController.Profile.Statistiques;

namespace GreenSa.ViewController.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private SQLiteConnection DBconnection;
        private Profil LocalUser;
        private StatistiqueMainTabbedPage statPage;

        public ProfilePage()
        {
            InitializeComponent();
            this.statPage = null;

            photo.Margin = responsiveDesign(30);
            photo.HeightRequest = responsiveDesign(150);
            user.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
            engrenage.Margin = responsiveDesign(10);
            engrenage.HeightRequest = responsiveDesign(30);
            arrow.Margin = responsiveDesign(10);
            arrow.HeightRequest = responsiveDesign(25);
            golfref.Margin = new Thickness(0, responsiveDesign(20), 0, 0);
            index.Margin = new Thickness(0, responsiveDesign(20), 0, 0);
            niv.Margin = new Thickness(0, responsiveDesign(20), 0, 0);
            clubs.Margin = new Thickness(responsiveDesign(5), 0, responsiveDesign(5), responsiveDesign(10));
            parties.Margin = new Thickness(responsiveDesign(5), 0, responsiveDesign(5), responsiveDesign(10));
            stats.Margin = new Thickness(responsiveDesign(5), 0, responsiveDesign(5), responsiveDesign(10));
            golfref.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
            index.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
            niv.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
            golfreftitle.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            indextitle.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            nivtitle.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            clubstitle.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
            partiestitle.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
            statstitle.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));

            boutons.Margin = new Thickness(10, 0, 10, 15);

            LocalUser = GetProfile("localUser");

            user.Text = LocalUser.Username;
            index.Text = LocalUser.Index.ToString();
            golfref.Text = LocalUser.GolfRef;
            System.Diagnostics.Debug.WriteLine("user" + LocalUser.Photo.ToString() + ".png");
            photo.Source = "user" + LocalUser.Photo.ToString() + ".png";

            if (LocalUser.Index > 30) { niv.Text = "Debutant"; }
            else if (LocalUser.Index > 18) { niv.Text = "Moyen"; }
            else if (LocalUser.Index > 11) { niv.Text = "Confirmé"; }
            else if (LocalUser.Index >5 ) { niv.Text = "Très bon joueur"; }
            else { niv.Text = "Compétitif"; }

        }

        protected override void OnAppearing()
        {
            LocalUser = GetProfile("localUser");

            user.Text = LocalUser.Username;

            index.Text = LocalUser.Index.ToString();

            golfref.Text = LocalUser.GolfRef;

            photo.Source = "user" + LocalUser.Photo + ".png";

            if (LocalUser.Index > 30) { niv.Text = "Debutant"; }
            else if (LocalUser.Index > 18) { niv.Text = "Moyen"; }
            else if (LocalUser.Index > 11) { niv.Text = "Confirmé"; }
            else if (LocalUser.Index > 5) { niv.Text = "Très bon joueur"; }
            else { niv.Text = "Compétitif"; }
        }

        private int responsiveDesign(int pix)
        {
            return (int)((pix * 4.1 / 1440.0) * Application.Current.MainPage.Width);
        }

        public Profil GetProfile(string id)
        {
            DBconnection = DependencyService.Get<ISQLiteDb>().GetConnection();
            return DBconnection.Table<Profil>().FirstOrDefault(pro => pro.Id == id);
        }

        /**
         * Méthode déclenchée au click sur le bouton "Jouer"
         * Redirige vers la page "GolfSelection"
         * */
        async private void OnClubsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyClubs.ClubSelectionPage());
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
            if (this.statPage == null)
            {
                this.statPage = new Statistiques.StatistiqueMainTabbedPage();
            }
            await Navigation.PushAsync(this.statPage);
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