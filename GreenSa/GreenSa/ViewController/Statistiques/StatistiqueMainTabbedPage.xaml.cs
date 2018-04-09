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
            string path = "GreenSa.ViewController.Statistiques.SpecificStatistiques";
            List < String > l = new List<String>();
            l.Add("DistanceClubPage");
            l.Add("ScoreEvolutionDetailsPage");
            l.Add("ScoreEvolutionPage");


            foreach(String file in l){
                
                string assemblyName = path+"."+file;
                Type t = Type.GetType(assemblyName);
                ContentPage p = (ContentPage)Activator.CreateInstance(t);
                p.Title = file;
                this.Children.Add(p);
            }
           
            base.OnAppearing();


        }
    



    }
}