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
            getScores(c=>true);
        }

        private void getScores(Filter<Club>.Filtre f)
        {
            float b3 = 0f;
            float b5 = 0f;
            float d = 0f;
            float f3 = 0f;
            float f4 = 0f;
            float f5 = 0f;
            float f6 = 0f;
            float f7 = 0f;
            float f8 = 0f;
            float f9 = 0f;
            float h = 0f;
            float s = 0f;
            float p = 0f;


            IEnumerable<Tuple<Club, double>> res = StatistiquesGolf.getAverageDistanceForClubs(f);


            foreach (Tuple<Club, double> couple in res)
            {
                if (couple.Item1.Name.Equals("Bois3")){
                    b3 = (float)couple.Item2;
                }
                if (couple.Item1.Name.Equals("Bois5")){
                    b5 = (float)couple.Item2;
                }
                if (couple.Item1.Name.Equals("Driver")){
                    d = (float)couple.Item2;
                }
                if (couple.Item1.Name.Equals("Fer3")){
                    f3 = (float)couple.Item2;
                }
                if (couple.Item1.Name.Equals("Fer4")){
                    f4 = (float)couple.Item2;
                }
                if (couple.Item1.Name.Equals("Fer5")){
                    f5 = (float)couple.Item2;
                }
                if (couple.Item1.Name.Equals("Fer6")){
                    f6 = (float)couple.Item2;
                }
                if (couple.Item1.Name.Equals("Fer7")){
                    f7 = (float)couple.Item2;
                }
                if (couple.Item1.Name.Equals("Fer8")){
                    f8 = (float)couple.Item2;
                }
                if (couple.Item1.Name.Equals("Fer9")){
                    f9 = (float)couple.Item2;
                }
                if (couple.Item1.Name.Equals("Hybride")){
                    h = (float)couple.Item2;
                }
                if (couple.Item1.Name.Equals("Sandwedge")){
                    s = (float)couple.Item2;
                }
                if (couple.Item1.Name.Equals("Pitching")){
                    p = (float)couple.Item2;
                }
            }

            var entries = new[]
             {

                new Entry(b3)
                 {
                     Label = "Bois3",
                     ValueLabel = b3.ToString()+"m",
                    Color = SKColor.Parse("#16F50B")
                 },
                new Entry(b5)
                 {
                     Label = "Bois5",
                     ValueLabel = b5.ToString()+"m",
                    Color = SKColor.Parse("#16F50B")
                 },
                new Entry(d)
                 {
                     Label = "Driver",
                     ValueLabel = d.ToString()+"m",
                    Color = SKColor.Parse("#16F50B")
                 },
                new Entry(f4)
                 {
                     Label = "Fer4",
                    ValueLabel = f4.ToString()+"m",
                    Color = SKColor.Parse("#16F50B")
                 },
                new Entry(f5)
                 {
                     Label = "Fer5",
                    ValueLabel = f5.ToString()+"m",
                    Color = SKColor.Parse("#16F50B")
                 },
                new Entry(f6)
                 {
                     Label = "Fer6",
                    ValueLabel = f6.ToString()+"m",
                    Color = SKColor.Parse("#16F50B")
                 },
                new Entry(f7)
                 {
                     Label = "Fer7",
                    ValueLabel = f7.ToString()+"m",
                    Color = SKColor.Parse("#16F50B")
                 },
                new Entry(f8)
                 {
                     Label = "Fer8",
                    ValueLabel = f8.ToString()+"m",
                    Color = SKColor.Parse("#16F50B")
                },new Entry(f9)
                 {
                     Label = "Fer9",
                    ValueLabel = f9.ToString()+"m",
                    Color = SKColor.Parse("#16F50B")
                 },
                new Entry(h)
                 {
                     Label = "Hybride",
                    ValueLabel = h.ToString()+"m",
                    Color = SKColor.Parse("#16F50B")
                 },
                new Entry(s)
                 {
                     Label = "Sundwage",
                    ValueLabel = s.ToString()+"m",
                    Color = SKColor.Parse("#16F50B")
                 },

            };

            this.chartView.Chart = new PointChart() { Entries = entries };
        }
    }
}