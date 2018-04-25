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


           

        }

        protected async override void OnAppearing()
        {
           
            this.Children.Add(new DistanceClubPage());
            this.Children.Add(new ScoreEvolutionDetailsPage());
            this.Children.Add(new ScoreEvolutionPage());


          
           
            base.OnAppearing();


        }
    



    }
}