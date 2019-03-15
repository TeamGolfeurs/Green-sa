using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GreenSa.Persistence;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GreenSa.Models;
using GreenSa.ViewController.Play;
using GreenSa.ViewController.Option;
using GreenSa.ViewController.MesGolfs;
using GreenSa.Models.GolfModel;
using GreenSa.Models.ViewElements;
using GreenSa.Models.Profiles;

using SQLite;
using System.Collections.ObjectModel;


namespace GreenSa.ViewController.Profile.Options
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileOptions : ContentPage
    {
        private SQLiteConnection DBconnection;
        private Profil LocalUser;

        public ProfileOptions()
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

            //Initialisation de la BDD
            this.InitBDD();
            LocalUser = GetProfile("localUser");
            
            //elements
            username.BackgroundColor = Color.FromRgba(0, 0, 0, 0.2);
            username.Text = LocalUser.Username;

            index.BackgroundColor = Color.FromRgba(0, 0, 0, 0.2);
            index.Text = LocalUser.Index.ToString();

            golfref.BackgroundColor = Color.FromRgba(0, 0, 0, 0.2);
            golfref.Text = LocalUser.GolfRef;

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

        private void OnNameCompleted(object sender, EventArgs e)
        {
            LocalUser.Username = ((Entry)sender).Text;
            DBconnection.Update(LocalUser);
        }

        private void OnGolfRefCompleted(object sender, EventArgs e)
        {
            LocalUser.GolfRef = ((Entry)sender).Text;
            DBconnection.Update(LocalUser);
        }

        private void OnIndexCompleted(object sender, EventArgs e)
        {
            LocalUser.Index = double.Parse(((Entry)sender).Text);
            DBconnection.Update(LocalUser);
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
            * Méthode déclenchée au click sur le bouton "Back"
            * Redirige vers la page "MainMenu"
            * */
        async private void OnArrowClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}