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

namespace GreenSa.ViewController.Profile.Statistiques.SpecificStatistiques
{
    public partial class GeneralStatPage : ContentPage
    {
        public GeneralStatPage()
        {
            InitializeComponent();
        }

        /**
         * Méthode déclenchée au click sur le bouton "Distances moyennes de vos clubs"
         * Redirige vers la page "DistanceClubPage"
         * */
        async private void OnDistClubClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DistanceClubPage());
        }

    }
}