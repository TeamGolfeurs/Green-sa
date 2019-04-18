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
    public partial class ChooseAvatar : ContentPage
    {
        private SQLiteConnection DBconnection;
        private Profil LocalUser;
        private ImageButton last;
        private Color col;

        public ChooseAvatar()
        {
            InitializeComponent();
            col = Color.Gray;
            title.FontSize = 30;
            ok.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
            ok.Margin = responsiveDesign(-15);
            ok.HeightRequest = responsiveDesign(80);
            ok.WidthRequest = responsiveDesign(80);

            //Initialisation de la BDD
            this.InitBDD();
            LocalUser = GetProfile("localUser");

            title.Margin = new Thickness(80, 5, 80, 10);
            if (LocalUser.Photo == 1)
            {
                last = photo1;
                photo1.BorderWidth = 3;
                photo1.BorderColor = col;
            }
            else if (LocalUser.Photo == 2) {
                last = photo2;
                photo2.BorderWidth = 3;
                photo2.BorderColor = col;
            }
            else if (LocalUser.Photo == 3)
            {
                last = photo3;
                photo3.BorderWidth = 3;
                photo3.BorderColor = col;
            }
            else if (LocalUser.Photo == 4)
            {
                last = photo4;
                photo4.BorderWidth = 3;
                photo4.BorderColor = col;
            }
            else if (LocalUser.Photo == 5)
            {
                last = photo5;
                photo5.BorderWidth = 3;
                photo5.BorderColor = col;
            }
            else if (LocalUser.Photo == 6)
            {
                last = photo6;
                photo6.BorderWidth = 3;
                photo6.BorderColor = col;
            }
            else
            {
                last = photo1;
                photo1.BorderWidth = 3;
                photo1.BorderColor = col;
            }
        }

        private int responsiveDesign(int pix)
        {
            return (int)((pix * 4.1 / 1440.0) * Application.Current.MainPage.Width);
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

        private void OnPhoto1Selected(object sender, EventArgs e)
        {
            last.BorderWidth = 0;
            last.BorderColor = Color.Transparent;
            LocalUser.Photo = 1;
            DBconnection.Update(LocalUser);
            photo1.BorderWidth = 3;
            photo1.BorderColor = col;
            last = photo1;
        }
        private void OnPhoto2Selected(object sender, EventArgs e)
        {
            last.BorderWidth = 0;
            last.BorderColor = Color.Transparent;
            LocalUser.Photo = 2;
            DBconnection.Update(LocalUser);
            photo2.BorderWidth = 3;
            photo2.BorderColor = col;
            last = photo2;
        }
        private void OnPhoto3Selected(object sender, EventArgs e)
        {
            last.BorderWidth = 0;
            last.BorderColor = Color.Transparent;
            LocalUser.Photo = 3;
            DBconnection.Update(LocalUser);
            photo3.BorderWidth = 3;
            photo3.BorderColor = col;
            last = photo3;
        }
        private void OnPhoto4Selected(object sender, EventArgs e)
        {
            last.BorderWidth = 0;
            last.BorderColor = Color.Transparent;
            LocalUser.Photo = 4;
            DBconnection.Update(LocalUser);
            photo4.BorderWidth = 3;
            photo4.BorderColor = col;
            last = photo4;
        }
        private void OnPhoto5Selected(object sender, EventArgs e)
        {
            last.BorderWidth = 0;
            last.BorderColor = Color.Transparent;
            LocalUser.Photo = 5;
            DBconnection.Update(LocalUser);
            photo5.BorderWidth = 3;
            photo5.BorderColor = col;
            last = photo5;
        }
        private void OnPhoto6Selected(object sender, EventArgs e)
        {
            last.BorderWidth = 0;
            last.BorderColor = Color.Transparent;
            LocalUser.Photo = 6;
            DBconnection.Update(LocalUser);
            photo6.BorderWidth = 3;
            photo6.BorderColor = col;
            last = photo6;
        }
    }
}