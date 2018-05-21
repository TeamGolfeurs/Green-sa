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

namespace GreenSa.ViewController.Statistiques
{
    public partial class GIRPage : ContentPage
    {
        public GIRPage()
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

        private void getScores(Func<Club, bool> f)
        {
            
            int hit = (int)StatistiquesGolf.getProportionHit();
            int missed = 100-hit;

            var entries = new[]
            {
                new Entry(hit)
                {
                    Label = "Hit",
                    ValueLabel = hit.ToString()+"%",
                    Color = SKColor.Parse("#16F50B")
                },
                new Entry(missed)
                 {
                    Label = "Missed",
                    ValueLabel = missed.ToString()+"%",
                    Color = SKColor.Parse("#F54B0B")
                 }
            };

            this.chartView.Chart = new DonutChart() { Entries = entries   };



        }

    }
}
