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
            this.BarBackgroundColor = Color.FromHex("0A7210");
            this.BarTextColor = Color.White;

            var Page1 = new GeneralStatPage();
            Page1.Title = "Général";
            this.Children.Add(Page1);

            var Page2 = new GolfSelectionPage();
            Page2.Title = "Par parcours";
            this.Children.Add(Page2);

            var Page3 = new ScoreVsParPage();
            Page3.Title = "Par partie";
            this.Children.Add(Page3);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    
    }
}