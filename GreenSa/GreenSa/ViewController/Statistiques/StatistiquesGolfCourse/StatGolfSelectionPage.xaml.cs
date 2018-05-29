using GreenSa.Models;
using GreenSa.Models.GolfModel;
using GreenSa.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace GreenSa.ViewController.Statistiques.StatistiquesGolfCourse
{
    public partial class StatGolfSelectionPage : ContentPage
    {
        public StatGolfSelectionPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //Définition du filtre
            Func<GolfCourse, bool> f = (c => true);

            //Recupere la liste des Golfs filtré par la classe GestionGolf
            ListGolfCourse.ItemsSource = await GestionGolfs.getListGolfsAsync(f);


        }

        async void Handle_TappedAsync(object sender, System.EventArgs e)
        {
            var g = ListGolfCourse.SelectedItem as GolfCourse;

            await Navigation.PushAsync(new StatGolfCoursePage(g));
        }


    }
}
