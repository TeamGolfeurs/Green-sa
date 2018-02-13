using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenSa.ViewController.Statistiques.SpecificStatistiques
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScoreEvolutionPage : ContentPage
    {
        public ScoreEvolutionPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();
        }
    }
}