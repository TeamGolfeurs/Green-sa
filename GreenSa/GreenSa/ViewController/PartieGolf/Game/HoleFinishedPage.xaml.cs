using GreenSa.Models.GolfModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenSa.ViewController.PartieGolf.Game
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HoleFinishedPage : ContentPage
    {
        private Partie partie;

        public HoleFinishedPage(Partie partie)
        {
            InitializeComponent();
            this.partie = partie;
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();
        }
    }
}