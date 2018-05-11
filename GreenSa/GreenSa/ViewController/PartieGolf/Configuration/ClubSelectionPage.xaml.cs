using GreenSa.Models.GolfModel;
using GreenSa.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

namespace GreenSa.ViewController.PartieGolf.Configuration
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClubSelectionPage : ContentPage
    {
        
        Partie p;

        public ClubSelectionPage(Partie partie)
        {

            InitializeComponent();
            p = partie;
            
        }

        /**
       * Méthode qui s'execute automatiquement au chargement de la page
       * Demande à la classe GestionGolf la liste des clubs
       * et met à jour la listView
       * */
        protected override void OnAppearing()
        {
            Func<Club, bool> f = (c => true);
            //get the list from gestionGolf
            /*if (listviewclub.ItemsSource != null)
                Navigation.PopToRootAsync();*/
           

            listviewclub.ItemsSource = GestionGolfs.getListClubs(f);
            base.OnAppearing();
        }

        /*
         * Appelée à la validation de la selection
         * doit mettre à jour la partie, et ouvrir la page du jeu (MainGamePage)
         * */
        private async void onValidClubSelection(object sender, SelectedItemChangedEventArgs e)
        {
            List<Club> clubselected = new List<Club>();
            foreach (Club c in listviewclub.ItemsSource){
                if(c.selected){
                    clubselected.Add(c);
                }
            }
            if (clubselected.Count == 0)
            {
                await DisplayAlert("Aucun club selectionné", "Vous devez selectionner au moins un club", "ok");
                return;
            }
            p.Clubs = clubselected;
            await Navigation.PushAsync(new Game.MainGamePage(p));
        }
    }
}