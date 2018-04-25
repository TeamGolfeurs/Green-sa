using GreenSa.Models.GolfModel;
using GreenSa.Models.Tools;
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
            getScores(c => true);
        }
        /**
        * Méthode déclenchée à l'application du filtre
        * Appel a la classe StatstiquesGolf avec un filtre
        * */
        async private void onFilterApplied(object sender, EventArgs e)
        {
            getScores(c=>true);
        }

        private void getScores(Filter<Club>.Filtre f)
        {
            IEnumerable<Tuple<Club, double>> res = StatistiquesGolf.getAverageDistanceForClubs(f);
            lab.Text = "";
            foreach (Tuple<Club, double> couple in res)
            {
                lab.Text += couple.Item1.Name + " : " + couple.Item2 + " \n";
            }
        }
    }
}