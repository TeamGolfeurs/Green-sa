using GreenSa.Models;
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
    /**
     * Page principale du jeu
     * Affiche la carte, boutons etc
     * 
     * */
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainGamePage : ContentPage
    {
        public MainGamePage(Partie partie)
        {
            InitializeComponent();
        }
    }
}