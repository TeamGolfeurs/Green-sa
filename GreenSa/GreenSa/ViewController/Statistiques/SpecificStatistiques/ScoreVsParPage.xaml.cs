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
            float p3 = 4.6f;
            float p4 = 5.3f;
            float p5 = 6.9f;
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
