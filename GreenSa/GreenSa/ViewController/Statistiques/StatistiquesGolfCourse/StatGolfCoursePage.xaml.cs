using System;
using System.Collections.Generic;
using GreenSa.Models.GolfModel;
using GreenSa.Models.Tools;

using Xamarin.Forms;

namespace GreenSa.ViewController.Statistiques.StatistiquesGolfCourse
{
    public partial class StatGolfCoursePage : ContentPage
    {
        
        public StatGolfCoursePage(GolfCourse g)
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

            //Création des noms des colonnes 
            var trou = new Label { Text = "Trou", BackgroundColor=Color.White };
            var par = new Label { Text = "Par", BackgroundColor = Color.White };
            var moy = new Label { Text = "Moy", BackgroundColor = Color.White };
            var max = new Label { Text = "Max", BackgroundColor = Color.White };
            var min = new Label { Text = "Min", BackgroundColor = Color.White };
            var nbPutt = new Label { Text = "Nb putt", BackgroundColor = Color.White };
            var nomb = new Label { Text = "Nb fois", BackgroundColor = Color.White };
            grid.Children.Add(trou, 0, 0);
            grid.Children.Add(par, 1, 0);
            grid.Children.Add(moy, 2, 0);
            grid.Children.Add(max, 3, 0);
            grid.Children.Add(min, 4, 0);
            grid.Children.Add(nbPutt, 5, 0);
            grid.Children.Add(nomb, 6, 0);

            //On récupère les Stats du Modele
            Func<GolfCourse, bool> f = (gf => gf.Equals(g));
            Dictionary<GolfCourse, List<Tuple<Hole, float, int, int,float, int>>> d = StatistiquesGolf.getScoreForGolfCourses(f);

            //On parcout tous le dictionnaire
            foreach (KeyValuePair<GolfCourse, List<Tuple<Hole, float, int, int,float,int>>> k in d)
            {   
                // Label du nom du golf que l'on ajoute dans le stacklayout au dessus du grid.
                var la = new Label { Text = k.Key.Name, BackgroundColor = Color.White, FontAttributes = FontAttributes.Bold, HorizontalTextAlignment= TextAlignment.Center,  };
                gridContent.Children.Add(la);

                //On récupère la liste des trous du golf
                List<Tuple<Hole, float, int, int,float,int>> list = k.Value;
                int nb = 1;

                //Pour chaque trou, on extrait les stats que l'on met dans un label et qu'on ajoute au gridlayout 
                foreach (Tuple<Hole, float, int, int,float,int> t2 in list){
                    var parval = t2.Item1.Par.ToString();
                    var moyvaleur = t2.Item2.ToString("n2");
                    var maxvaleur = t2.Item3.ToString();
                    var minvaleur = t2.Item4.ToString();
                    var moyPutt = t2.Item5.ToString("n2");
                    var nbJoue = t2.Item6.ToString();

                    if (moyvaleur.Equals("NaN")){
                        moyvaleur = "N/A";
                    }
                    if (moyPutt.Equals("NaN"))
                    {
                        moyPutt = "N/A";
                    }
                    if (maxvaleur.Equals("99"))
                    {
                        maxvaleur = "N/A";
                    }
                    if (minvaleur.Equals("-99"))
                    {
                        minvaleur = "N/A";
                    }
                    var parLab = new Label { Text = parval, BackgroundColor = Color.White };
                    var tr = new Label { Text = (nb).ToString(), BackgroundColor = Color.White };
                    var mo = new Label { Text = moyvaleur, BackgroundColor = Color.White };
                    var ma = new Label { Text = maxvaleur, BackgroundColor = Color.White };
                    var mi = new Label { Text = minvaleur, BackgroundColor = Color.White };
                    var nbP = new Label { Text = moyPutt, BackgroundColor = Color.White };

                    var nbJ = new Label { Text = nbJoue, BackgroundColor = Color.White };

                    grid.Children.Add(tr, 0,nb);
                    grid.Children.Add(parLab,1, nb);
                    grid.Children.Add(mo,2,nb);
                    grid.Children.Add(ma,3,nb);
                    grid.Children.Add(mi,4,nb);
                    grid.Children.Add(nbP,5, nb);
                    grid.Children.Add(nbJ, 6, nb);

                    nb++;
                    }
                
            }

            //On rajoute le grid dans le stacklayout en dessous du label du nom du golf
            gridContent.Children.Add(grid);
        }
    }
}
