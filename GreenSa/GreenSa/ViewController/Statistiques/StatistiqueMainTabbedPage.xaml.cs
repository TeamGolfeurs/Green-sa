using GreenSa.ViewController.Statistiques.SpecificStatistiques;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenSa.ViewController.Statistiques
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatistiqueMainTabbedPage : ContentPage
    {
        public StatistiqueMainTabbedPage()
        {
            InitializeComponent();

        }

        async protected override void OnAppearing()
        {
            //lire dans un fichier XML les noms des Classes statistiques
            string assemblyName = "GreenSa.ViewController.Statistiques.SpecificStatistiques.DistanceClubPage";
            Type t = Type.GetType(assemblyName);
            ContentPage p = (ContentPage)Activator.CreateInstance(t);
            await Navigation.PushAsync(p);


        }
    



    }
}