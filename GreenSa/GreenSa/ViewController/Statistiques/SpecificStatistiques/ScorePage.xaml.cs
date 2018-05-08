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
    public partial class ScorePage : ContentPage
    {
        public ScorePage()
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
            float albatros = 0f;
            float eagle = 0f;
            float birdie = 0f;
            float par = 0f;
            float bogey = 0f;
            float dbogey = 0f;
            float more = 0f;

            Dictionary<Hole.ScorePossible, float> d = StatistiquesGolf.getProportionScore();

            foreach(KeyValuePair<Hole.ScorePossible, float> k in d){
                if (k.Key.Equals(Hole.ScorePossible.ALBATROS)){
                    albatros = k.Value;
                }
                if (k.Key.Equals(Hole.ScorePossible.BIRDIE)){
                    birdie = k.Value;
                }
                if (k.Key.Equals(Hole.ScorePossible.BOGEY)){
                    bogey = k.Value;
                }
                if (k.Key.Equals(Hole.ScorePossible.DOUBLE_BOUGEY)){
                    dbogey = k.Value;
                }
                if (k.Key.Equals(Hole.ScorePossible.EAGLE)){
                    eagle = k.Value;
                }
                if (k.Key.Equals(Hole.ScorePossible.MORE)){
                    more = k.Value;
                }
                if (k.Key.Equals(Hole.ScorePossible.PAR)){
                    par = k.Value;
                }
            }

            
            var entries = new[]
             {

                new Entry(eagle)
                 {
                     Label = "Albatros",
                     ValueLabel = albatros.ToString()+"%",
                    Color = SKColor.Parse("#0BF5A3")
                 },    
                new Entry(eagle)
                 {
                     Label = "Eagle",
                    ValueLabel = eagle.ToString()+"%",
                    Color = SKColor.Parse("#0BF5A3")
                 },
                new Entry(birdie)
                 {
                     Label = "Birdie",
                    ValueLabel = birdie.ToString()+"%",
                    Color = SKColor.Parse("#0BF54E")
                 },
                new Entry(par)
                 {
                     Label = "Par",
                    ValueLabel = par.ToString()+"%",
                    Color = SKColor.Parse("#44F50B")
                 },
                new Entry(bogey)
                 {
                     Label = "Bogey",
                    ValueLabel = bogey.ToString()+"%",
                    Color = SKColor.Parse("#C0F50B")
                 },
                new Entry(dbogey)
                 {
                     Label = "DoubleBogey",
                    ValueLabel = dbogey.ToString()+"%",
                    Color = SKColor.Parse("#F5A00B")
                 },
                new Entry(more)
                 {
                     Label = "More",
                    ValueLabel = more.ToString()+"%",
                    Color = SKColor.Parse("#F5340B")
                 },
            };

            this.chartView.Chart = new BarChart() { Entries = entries };

        }
    }
}
