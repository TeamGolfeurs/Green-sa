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
using GreenSa.Models.Tools;

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
            photo.Margin = new Thickness(0, responsiveDesign(30), 0, responsiveDesign(5));
            photo.HeightRequest = responsiveDesign(140);
            arrow.Margin = responsiveDesign(10);
            arrow.HeightRequest = responsiveDesign(25);
            modifier.Margin = responsiveDesign(15);
            modifier.HeightRequest = responsiveDesign(30);
            golfreftitle.FontSize = 25;
            indextitle.FontSize = 25;
            usernametitle.FontSize = 25;
            golfref.FontSize = 20;
            index.FontSize = 20;
            username.FontSize = 20;

            //Initialisation de la BDD
            LocalUser = GetProfile("localUser");
            
            //elements
            username.Text = LocalUser.Username;

            index.Text = LocalUser.Index.ToString();

            golfref.Text = LocalUser.GolfRef;

            photo.Source = "user" + LocalUser.Photo + ".png";

            boutons.Margin = new Thickness(30, 15, 30, 15);
        }

        protected override void OnAppearing()
        {
            LocalUser = GetProfile("localUser");

            username.Text = LocalUser.Username;

            index.Text = LocalUser.Index.ToString();

            golfref.Text = LocalUser.GolfRef;

            photo.Source = "user" + LocalUser.Photo + ".png";
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

        private async void OnIndexCompleted(object sender, EventArgs e)
        {
            LocalUser.Index = double.Parse(((Entry)sender).Text);
            DBconnection.Update(LocalUser);
            List<Club> clubs = await GestionGolfs.getListClubsAsync(null);
            List<Club> xmlClubs = GolfXMLReader.getListClubFromXMLFiles();
            foreach (Club club in clubs)
            {
                club.DistanceMoyenne = xmlClubs.Find(c => c.Name.Equals(club.Name)).DistanceMoyenne;
            }
            DBconnection.UpdateAll(clubs);
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