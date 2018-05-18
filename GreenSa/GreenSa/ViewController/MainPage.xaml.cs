using GreenSa.Models.GolfModel;
using GreenSa.ViewController.PartieGolf.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GreenSa.ViewController.Statistiques;
using GreenSa.ViewController.StatistiquesGolfCourse;
using GreenSa.ViewController.Test;

namespace GreenSa.ViewController
{
    /**
     *  Page d'accueil 
     *  Contient  :
     *          -Bouton option
     *          -Titre
     *          -Bouton Jouer
     *          -Bouton Stats
     *      VOIR MAQUETTE 1
     */
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {

            InitializeComponent();
            //optionButton = new FileImageSource { File = "GreenSa.Ressources.Images.tools.png" };
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                onOptionsClicked(s, e);
            };
            optionButton.GestureRecognizers.Add(tapGestureRecognizer);
            //optionButton.Source = ImageSource.FromResource("GreenSa.Ressources.Images.tools.png");
            //optionButton.Image = new FileImageSource { File = "GreenSa.Ressources.Images.tools.png" };

        }

        /**
         * Méthode déclenchée au click sur le bouton "Jouer"
         * Redirige vers la page "GolfSelection"
         * */
        async private void onPlayClicked(object sender, EventArgs e)
        {
            Partie partie = new Partie();
            await Navigation.PushAsync(new GolfSelectionPage(partie));
        }

        /**
          * Méthode déclenchée au click sur le bouton "Stats"
          * Redirige vers la page "StatMenu"
          * */
        async private void onStatsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StatistiqueMainTabbedPage() );
        }

        async private void onStatsGolfsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StatGolfSelectionPage());
        }

        async private void onOptionsClicked(object sender, EventArgs e)
        {
            label.Text= "WIIIIIIIIIIII";

        }

        async private void voirBD(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SeeBDContent());
        }

        async private void voirWind(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WindServicePage());
        }
    }
}
