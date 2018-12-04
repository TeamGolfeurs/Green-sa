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
         private void onFilterApplied(object sender, EventArgs e)
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
                    albatros =  k.Value ;
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

                new Entry(albatros)
                 {
                     Label = "Albatros",
                     ValueLabel =float.IsNaN(albatros)?"N/A":(albatros.ToString("n2")   +"%"),
                    Color = SKColor.Parse("#0BF5A3")
                 },    
                new Entry(eagle)
                 {
                     Label = "Eagle",
                    ValueLabel =float.IsNaN(eagle)?"N/A": (eagle.ToString("n2")+"%"),
                    Color = SKColor.Parse("#0BF5A3")
                 },
                new Entry(birdie)
                 {
                     Label = "Birdie",
                    ValueLabel = float.IsNaN(birdie)?"N/A":(birdie.ToString("n2")+"%"),
                    Color = SKColor.Parse("#0BF54E")
                 },
                new Entry(par)
                 {
                     Label = "Par",
                    ValueLabel = float.IsNaN(par)?"N/A":(par.ToString("n2")+"%"),
                    Color = SKColor.Parse("#44F50B")
                 },
                new Entry(bogey)
                 {
                     Label = "Bogey",
                    ValueLabel = float.IsNaN(bogey)?"N/A":(bogey.ToString("n2")+"%"),
                    Color = SKColor.Parse("#C0F50B")
                 },
                new Entry(dbogey)
                 {
                     Label = "DoubleBogey",
                    ValueLabel = float.IsNaN(dbogey)?"N/A":(dbogey.ToString("n2")+"%"),
                    Color = SKColor.Parse("#F5A00B")
                 },
                new Entry(more)
                 {
                     Label = "More",
                    ValueLabel = float.IsNaN(more)?"N/A":(more.ToString("n2")+"%"),
                    Color = SKColor.Parse("#F5340B")
                 },
            };

            this.chartView.Chart = new BarChart() { Entries = entries, LabelTextSize=22, MaxValue=100 };

        }
    }
}
