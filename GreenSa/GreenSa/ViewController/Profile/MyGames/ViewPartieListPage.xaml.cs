using GreenSa.Models.GolfModel;
using GreenSa.ViewController.Profile.Statistiques.StatistiquesGolfCourse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenSa.ViewController.Profile.MyGames
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewPartieListPage : ContentPage
    {
        private bool isInStat;

        public ViewPartieListPage()
        {
            InitializeComponent();
            this.isInStat = false;
        }

        public ViewPartieListPage(bool isInStat)
        {
            InitializeComponent();
            this.isInStat = isInStat;
        }

        async protected override void OnAppearing()
        {
            IEnumerable<ScorePartie> list = await StatistiquesGolf.getListOfScorePartie();
            listPartie.ItemsSource = list;
        }

        private async void listPartie_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (!isInStat)
            {
                await Navigation.PushModalAsync(new DetailsPartiePage((ScorePartie)listPartie.SelectedItem));
            } else
            {
                await Navigation.PushModalAsync(new PartieStatPage((ScorePartie)listPartie.SelectedItem));
            }
        }
    }
}