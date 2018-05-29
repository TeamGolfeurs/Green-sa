﻿using GreenSa.Models.GolfModel;
using GreenSa.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenSa.ViewController.PartieGolf.Configuration
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClubSelection : ContentPage
    {
        public ClubSelection(Partie partie)
        {
            InitializeComponent();
        }

        /**
       * Méthode qui s'execute automatiquement au chargement de la page
       * Demande à la classe GestionGolf la liste des clubs
       * et met à jour la listView
       * */
        protected override void OnAppearing()
        {
            Filter<Club>.Filtre f = (c => true);
            //get the list from gestionGolf

            base.OnAppearing();
        }

        /*
         * Appelée à la validation de la selection
         * doit mettre à jour la partie, et ouvrir la page du jeu (MainGamePage)
         * */
        private async void onValidClubSelection(object sender, SelectedItemChangedEventArgs e)
        {
        }
    }
}