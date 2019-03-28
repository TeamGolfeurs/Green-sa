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

            //Initialisation de la BDD
            LocalUser = GetProfile("localUser");
            
            //elements
            username.Text = LocalUser.Username;

            index.Text = LocalUser.Index.ToString();

            golfref.Text = LocalUser.GolfRef;

            photo.Source = "user" + LocalUser.Photo + ".png";

            boutons.Margin = new Thickness(60, 15, 60, 60);
        }

        protected override void OnAppearing()
        {
            LocalUser = GetProfile("localUser");

            username.Text = LocalUser.Username;

            index.Text = LocalUser.Index.ToString();

            golfref.Text = LocalUser.GolfRef;

            photo.Source = "user" + LocalUser.Photo + ".png";
        }

        public Profil GetProfile(string id)
        {
            DBconnection = DependencyService.Get<ISQLiteDb>().GetConnection();
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
            * Méthode déclenchée au click sur le bouton "Back"
            * Redirige vers la page "MainMenu"
            * */
        async private void OnArrowClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        async private void OnPhotoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Profile.Options.ChooseAvatar());
        }
    }
}