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
    public partial class ScoreEvolutionDetailsPage : ContentPage
    {
        public ScoreEvolutionDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();
        }

        /**
        * Méthode déclenchée à l'application du filtre
        * Appel a la classe StatstiquesGolf avec un filtre
        * */
        async private void onFilterApplied(object sender, EventArgs e)
        {
        }

        /**
        * Méthode déclenchée au clic sur un golfCourse
        * Redirigie vers ScoreEvolutionPage
        * */
        async private void onGolfCourseClicked(object sender, EventArgs e)
        {
        }


    }
}