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

namespace GreenSa.ViewController.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private SQLiteConnection DBconnection;
        private Profil LocalUser;

        public ProfilePage()
        {
            InitializeComponent();

            boutons.Margin = new Thickness(10, 0, 10, 15);

            golfref.Margin = new Thickness(0, 15, 0, 0);
            index.Margin = new Thickness(0, 15, 0, 0);
            niv.Margin = new Thickness(0, 15, 0, 0);

            this.InitBDD();
            LocalUser = GetProfile("localUser");

            user.Text = LocalUser.Username;
            index.Text = LocalUser.Index.ToString();
            golfref.Text = LocalUser.GolfRef;

            if (LocalUser.Index > 30) { niv.Text = "Debutant"; }
            else if (LocalUser.Index > 18) { niv.Text = "Moyen"; }
            else if (LocalUser.Index > 11) { niv.Text = "Confirmé"; }
            else if (LocalUser.Index >5 ) { niv.Text = "Très bon joueur"; }
            else { niv.Text = "Compétitif"; }

        }

        public void InitBDD()
        {
            DBconnection = DependencyService.Get<ISQLiteDb>().GetConnection();
            System.Diagnostics.Debug.WriteLine("connection ok");
            DBconnection.CreateTable<Profil>();
            System.Diagnostics.Debug.WriteLine("create ok");
            if (!DBconnection.Table<Profil>().Any())
            {
                AddLocalUser();
            }
        }

        public void AddLocalUser()
        {
            DBconnection.Insert(new Profil());
            System.Diagnostics.Debug.WriteLine("user added");
        }

        public Profil GetProfile(string id)
        {
            System.Diagnostics.Debug.WriteLine("get ok");
            return DBconnection.Table<Profil>().FirstOrDefault(pro => pro.Id == id);
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