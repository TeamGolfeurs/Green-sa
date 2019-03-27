using GreenSa.ViewController.Profile.Statistiques.SpecificStatistiques;
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
using GreenSa.ViewController.Play;

namespace GreenSa.ViewController.Profile.Statistiques
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatistiqueMainTabbedPage : TabbedPage
    {
        public StatistiqueMainTabbedPage()
        {
            InitializeComponent();
            this.BarBackgroundColor = Color.FromHex("39B54A");
            this.BarTextColor = Color.FromHex("0A7210");

            var Page1 = new DistanceClubPage();
            Page1.Title = "Général";
            this.Children.Add(Page1);

            var Page2 = new GolfSelectionPage();
            Page2.Title = "Par parcours";
            this.Children.Add(Page2);

            var Page3 = new ScoreVsParPage();
            Page3.Title = "Par partie";
            this.Children.Add(Page3);

            /*var Page4 = new GIRPage();
            Page4.Title = "GIR";
            this.Children.Add(Page4);*/
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    
    }
}