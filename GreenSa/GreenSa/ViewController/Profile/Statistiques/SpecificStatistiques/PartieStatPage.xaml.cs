using System;
using System.Collections.Generic;
using GreenSa.Models.GolfModel;
using GreenSa.Models.Tools;
using Entry = Microcharts.Entry;
using Xamarin.Forms;
using SkiaSharp;
using Microcharts;
using GreenSa.ViewController.Profile.Statistiques.SpecificStatistiques;

namespace GreenSa.ViewController.Profile.Statistiques.StatistiquesGolfCourse
{
    public partial class PartieStatPage : ContentPage
    {

        private ScorePartie scorePartie;

        public PartieStatPage(ScorePartie sp)
        {
            InitializeComponent();
            this.scorePartie = sp;
            this.legendButton.BorderColor = Color.FromHex("0C5E11");
            this.legendButton.BorderWidth = 2;
            //this.golfCourseName.Text = this.scorePartie.Name;
        }

        async protected override void OnAppearing()
        {
            
        }

        /**
         * Méthode déclenchée au click sur le bouton légende
         * */
        private void onLegendClick(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Text.Equals("?"))
            {
                b.Text = "X";
                this.legend.IsVisible = true;
                this.bottomStats.IsVisible = false;
            }
            else
            {
                b.Text = "?";
                this.legend.IsVisible = false;
                this.bottomStats.IsVisible = true;
            }
        }

        private void updateWorstHole(List<ScoreHole> allScoreHoles)
        {
            /*int worstHoleNumber = StatistiquesGolf.getWorstHole(allScoreHoles, this.golfCourse);
            if (worstHoleNumber == 0)
            {
                this.worstHole.Text = GeneralStatPage.NO_DATA;
                this.worstHole.TextColor = Color.Gray;
                this.worstHole.FontSize = 15;
            }
            else
            {
                this.worstHole.Text = "" + worstHoleNumber;
                this.worstHole.TextColor = Color.FromHex("#39B54A");
                this.worstHole.FontSize = 30;
            }*/
        }

        private void updateChart(List<ScoreHole> allScoreHoles)
        {
            /*float eagle = 0f;
            float birdie = 0f;
            float par = 0f;
            float bogey = 0f;
            float dbogey = 0f;
            float more = 0f;

            Dictionary<Hole.ScorePossible, float> d = StatistiquesGolf.getProportionScore(allScoreHoles, this.golfCourse);

            foreach (KeyValuePair<Hole.ScorePossible, float> k in d)
            {
                if (k.Key.Equals(Hole.ScorePossible.BIRDIE))
                {
                    birdie = k.Value;
                }
                if (k.Key.Equals(Hole.ScorePossible.BOGEY))
                {
                    bogey = k.Value;
                }
                if (k.Key.Equals(Hole.ScorePossible.DOUBLE_BOUGEY))
                {
                    dbogey = k.Value;
                }
                if (k.Key.Equals(Hole.ScorePossible.EAGLE))
                {
                    eagle = k.Value;
                }
                if (k.Key.Equals(Hole.ScorePossible.MORE))
                {
                    more = k.Value;
                }
                if (k.Key.Equals(Hole.ScorePossible.PAR))
                {
                    par = k.Value;
                }
            }


            var entries = new[]
             {
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
                     Label = "Dbl-Bogey",
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

            this.chartView.Chart = new BarChart() { Entries = entries, LabelTextSize = 26, MaxValue = 100, ValueLabelOrientation = Orientation.Horizontal, LabelOrientation = Orientation.Horizontal};
            */
        }

    }
}
