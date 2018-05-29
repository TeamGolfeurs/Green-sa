using GreenSa.Models.GolfModel;
using GreenSa.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using Microcharts;
using Entry = Microcharts.Entry; 

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;

namespace GreenSa.ViewController.Statistiques.SpecificStatistiques
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DistanceClubPage : ContentPage
    {
        public DistanceClubPage()
        {
            InitializeComponent();
        }
        protected override  void OnAppearing()
        {
            
            base.OnAppearing();
             getScoresAsync(c => true);
           

        }
        /*
        * Méthode déclenchée à l'application du filtre
        * Appel a la classe StatstiquesGolf avec un filtre
        * */
        async private void onFilterApplied(object sender, EventArgs e)
        {
             getScoresAsync(c=>true);
        }

        private void getScoresAsync(Func<Club, bool> f)
        {

            IEnumerable<Tuple<Club, double>> res = StatistiquesGolf.getAverageDistanceForClubsAsync(f);

            List<Entry> entries = new List<Entry>();

            foreach (Tuple<Club, double> couple in res){

                Entry e = new Entry((float)couple.Item2)
                {
                    Label = couple.Item1.Name,
                    ValueLabel = couple.Item2.ToString("n2") + "m",
                    Color = SKColor.Parse("#16F50B")
                };
                entries.Add(e);
            }

            this.chartView.Chart = new PointChart(){ Entries = entries, LabelTextSize=29};
        }
    }
}