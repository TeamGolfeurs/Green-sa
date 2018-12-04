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

namespace GreenSa.ViewController.Profile.Statistiques.SpecificStatistiques
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

        private void getScores(Func<Club, bool> f)
        {

            Dictionary<int, float> d = StatistiquesGolf.getScoreForPar();

            List<Entry> entries = new List<Entry>();

            foreach (KeyValuePair<int, float> k in d){

                String color = "#F7230C";
                if(k.Value<0){
                    color = "#98FB98";

                }
                Entry e = new Entry(k.Value)
                {
                    Label = "PAR" + k.Key.ToString(),
                    ValueLabel = k.Value.ToString("n2"),
                    Color = SKColor.Parse(color)
                };
                entries.Add(e);
            }

            this.chartView.Chart = new BarChart() { Entries = entries, LabelTextSize=25 };

        }
    }
}
