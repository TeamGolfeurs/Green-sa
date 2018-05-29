using GreenSa.Models.GolfModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenSa.ViewController.Statistiques.Partie
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewPartieListPage : ContentPage
    {
        public ViewPartieListPage()
        {
            InitializeComponent();
        }

        async protected override void OnAppearing()
        {
            IEnumerable<ScorePartie> list = await StatistiquesGolf.getListOfScorePartie();
            listPartie.ItemsSource = list;
        }

        private async void listPartie_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushModalAsync(new DetailsPartiePage((ScorePartie)listPartie.SelectedItem));
        }
    }
}