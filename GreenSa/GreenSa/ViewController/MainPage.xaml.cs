using GreenSa.Models.GolfModel;
using GreenSa.ViewController.PartieGolf.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

        }

        async private void onOptionsClicked(object sender, EventArgs e)
        {

        }
        


    }
}
