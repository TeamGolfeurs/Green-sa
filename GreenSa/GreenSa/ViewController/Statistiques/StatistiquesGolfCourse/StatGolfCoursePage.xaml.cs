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
            //Création GridLayout en c#
            var grid = new Grid();
            grid.BackgroundColor = Color.Black;
            grid.ColumnSpacing = 5;
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(180) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(40) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(40) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(40) });

            var trou = new Label { Text = "Trou", BackgroundColor=Color.White };
            var moy = new Label { Text = "Moy", BackgroundColor = Color.White };
            var max = new Label { Text = "Max", BackgroundColor = Color.White };
            var min = new Label { Text = "Min", BackgroundColor = Color.White };
            var nomb = new Label { Text = "Nb Fois", BackgroundColor = Color.White };


            grid.Children.Add(trou, 0, 0);
            grid.Children.Add(moy, 1, 0);
            grid.Children.Add(max, 2, 0);
            grid.Children.Add(min, 3, 0);
            grid.Children.Add(nomb, 4, 0);

            //On récupère les Stats du Modele
            Func<GolfCourse, bool> f = (gf => gf.Equals(g));
            Dictionary<GolfCourse, Tuple<List<Tuple<Hole, float, int, int>>, int>> d = StatistiquesGolf.getScoreForGolfCourses(f);

            foreach (KeyValuePair<GolfCourse, Tuple<List<Tuple<Hole, float, int, int>>, int>> k in d)
            {
                //on prend que les couples avec le bon golf selectionné
                //On récupère la liste des trous du golf
                    Tuple<List<Tuple<Hole, float, int, int>>, int> t = k.Value;
                    List<Tuple<Hole, float, int, int>> list = t.Item1;
                    int nb = 1;

                    //Pour chaque trou, on extrait les stats que l'on met ds un label et qu'on ajoute au gridlayout 
                    foreach (Tuple<Hole, float, int, int> t2 in list){
                    
                        var tr = new Label { Text = nb.ToString(), BackgroundColor = Color.White };
                        var mo = new Label { Text = t2.Item2.ToString(), BackgroundColor = Color.White };
                        var ma = new Label { Text = t2.Item3.ToString(), BackgroundColor = Color.White };
                        var mi = new Label { Text = t2.Item4.ToString(), BackgroundColor = Color.White };
                        grid.Children.Add(tr, 0,nb);
                        grid.Children.Add(mo, 1,nb);
                        grid.Children.Add(ma, 2,nb);
                        grid.Children.Add(mi, 3,nb);
                        grid.Children.Add(new Label { Text = t.Item2.ToString(), BackgroundColor = Color.White }, 4, nb);

                    nb++;
                    }
                
            }

            Content = grid;
        }
    }
}
