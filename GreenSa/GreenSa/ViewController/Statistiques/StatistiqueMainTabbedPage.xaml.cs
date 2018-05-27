using GreenSa.ViewController.Statistiques.SpecificStatistiques;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;


namespace GreenSa.ViewController.Statistiques
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatistiqueMainTabbedPage : TabbedPage
    {
        public StatistiqueMainTabbedPage()
        {
            InitializeComponent();
            var Page1 = new DistanceClubPage();
            Page1.Title = "Distance Clubs";
            this.Children.Add(Page1);

            var Page2 = new ScorePage();
            Page2.Title = "Score";
            this.Children.Add(Page2);

            var Page3 = new ScoreVsParPage();
            Page3.Title = "Score par Par";
            this.Children.Add(Page3);

            var Page4 = new GIRPage();
            Page4.Title = "GIR";
            this.Children.Add(Page4);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    
    }
}