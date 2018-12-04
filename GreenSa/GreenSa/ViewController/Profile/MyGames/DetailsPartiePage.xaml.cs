using System;
using System.Collections.Generic;
using GreenSa.Models.GolfModel;
using GreenSa.Models.Tools;

using Xamarin.Forms;

namespace GreenSa.ViewController.Profile.MyGames
{
    public partial class DetailsPartiePage : ContentPage
    {
        
        public DetailsPartiePage(ScorePartie sp)
        {
            
            InitializeComponent();

            //Le Stacklayout contient un label pour le nom du golf et un GridLayout pour faire un tableau de stat par trou

            //Création GridLayout en c#
            var grid = new Grid();
            grid.BackgroundColor = Color.Black;
            grid.ColumnSpacing = 5;
            /* grid.ColumnDefinitions.Add(new ColumnDefinition {  });
             grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
             grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
             grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
             grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
             grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
             grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });*/
            var la = new Label { Text = "Partie du "+ sp.DateString , BackgroundColor = Color.White, FontAttributes = FontAttributes.Bold, HorizontalTextAlignment = TextAlignment.Center, };
            gridContent.Children.Add(la);

            //Création des noms des colonnes 
            var trou = new Label { Text = "Trou", BackgroundColor=Color.White };
            var par = new Label { Text = "Par", BackgroundColor = Color.White };
            var score = new Label { Text = "Score", BackgroundColor = Color.White };
            var hit = new Label { Text = "Hit", BackgroundColor = Color.White };
            var nbPutt = new Label { Text = "Nb putt", BackgroundColor = Color.White };
            grid.Children.Add(trou, 0, 0);
            grid.Children.Add(par, 1, 0);
            grid.Children.Add(score, 2, 0);
            grid.Children.Add(hit, 3, 0);
            grid.Children.Add(nbPutt, 4, 0);
            int nb = 1;
            //On parcout tous le dictionnaire
            foreach (ScoreHole sh in sp.scoreHoles)
            {
                var n = new Label { Text = nb+"", BackgroundColor = Color.White };

                var parLab = new Label { Text = sh.Hole.Par+"", BackgroundColor = Color.White };
                var scor = new Label { Text = sh.Score+"", BackgroundColor = Color.White };
                var hitted = new Label { Text = sh.Hit?"Oui":"Non", BackgroundColor = Color.White };
                var nbPut = new Label { Text = sh.NombrePutt+"", BackgroundColor = Color.White };
                
                grid.Children.Add(n, 0, nb);
                grid.Children.Add(parLab, 1, nb);
                grid.Children.Add(scor, 2, nb);
                grid.Children.Add(hitted, 3, nb);
                grid.Children.Add(nbPut, 4, nb);
                nb++;
            }

            //On rajoute le grid dans le stacklayout en dessous du label du nom du golf
            gridContent.Children.Add(grid);
        }

       
    }
}
