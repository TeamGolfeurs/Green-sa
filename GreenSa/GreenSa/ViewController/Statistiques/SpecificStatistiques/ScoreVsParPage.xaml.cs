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

using Xamarin.Forms;

namespace GreenSa.ViewController.Statistiques.SpecificStatistiques
{
    public partial class ScoreVsParPage : ContentPage
    {
        public ScoreVsParPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {

            base.OnAppearing();
            getScores(c => true);


        }
        /*
        * Méthode déclenchée à l'application du filtre
        * Appel a la classe StatstiquesGolf avec un filtre
        * */
        async private void onFilterApplied(object sender, EventArgs e)
        {
            getScores(c => true);
        }

        private void getScores(Filter<Club>.Filtre f)
        {

            float p3= 0f;
            float p4= 0f;
            float p5= 0f;

            Dictionary<int, float> d = StatistiquesGolf.getScoreForPar();

            foreach(KeyValuePair<int,float> k in d){
                if (k.Key==3){
                    p3 = k.Value;
                }
                if (k.Key == 4)
                {
                    p4 = k.Value;
                }
                if (k.Key == 5)
                {
                    p5 = k.Value;
                }
            }


            var entries = new[]
             {
                 new Entry(p3)
                 {
                     Label = "PAR 3",
                    ValueLabel = p3.ToString(),
                    Color = SKColor.Parse("#98FB98")
                 },
                 new Entry(p4)
                 {
                     Label = "PAR 4",
                    ValueLabel = p4.ToString(),
                    Color = SKColor.Parse("#98FB98")
                 },
                 new Entry(p5)
                 {
                     Label = "PAR 5",
                    ValueLabel = p5.ToString(),
                    Color = SKColor.Parse("#98FB98")
                 }
            };

            this.chartView.Chart = new BarChart() { Entries = entries };

        }
    }
}
