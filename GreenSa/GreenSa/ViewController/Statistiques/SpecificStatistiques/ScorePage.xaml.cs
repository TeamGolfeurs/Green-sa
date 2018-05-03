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

        private void getScores(Filter<Club>.Filtre f)
        {

            float eagle = 2f;
            float birdie = 10f;
            float par = 40f;
            float bogey = 30f;
            float dbogey = 10f;
            float more = 8f;

            var entries = new[]
             {
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
