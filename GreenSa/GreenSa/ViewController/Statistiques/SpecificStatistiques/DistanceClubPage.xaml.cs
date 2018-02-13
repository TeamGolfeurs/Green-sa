using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenSa.ViewController.Statistiques.SpecificStatistiques
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DistanceClubPage : ContentPage
    {
        public DistanceClubPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {

            base.OnAppearing();
        }
        /**
        * Méthode déclenchée à l'application du filtre
        * Appel a la classe StatstiquesGolf avec un filtre
        * */
        async private void onFilterApplied(object sender, EventArgs e)
        {
        }
    }
}