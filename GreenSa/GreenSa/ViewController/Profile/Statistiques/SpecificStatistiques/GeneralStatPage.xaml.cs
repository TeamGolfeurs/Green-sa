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
using GreenSa.Persistence;

namespace GreenSa.ViewController.Profile.Statistiques.SpecificStatistiques
{
    public partial class GeneralStatPage : ContentPage
    {
        public GeneralStatPage()
        {
            InitializeComponent();
            updateMaxDistClubStat();
            updateLast4Scores();
        }


        private void updateLast4Scores()
        {
            TestClassFactory.CreateScorePartie();
            TestClassFactory.CreateScorePartie();
            TestClassFactory.CreateScorePartie();
            int index = (int)StatistiquesGolf.getPlayerIndex();
            int rowCount = last4ScoresGrid.Children.Count / 3;
            var scores = StatistiquesGolf.getScores();
            System.Diagnostics.Debug.WriteLine(scores[0].scoreHoles.ToString());
            int col = 0;
            int row = 0;
            foreach (View label in last4ScoresGrid.Children)
            {
                if (row > 0)
                {
                    if (row <= scores.Count)
                    {
                        switch (col)
                        {
                            case 0://column golf name
                                ((Label)label).Text = "Nom du golf";
                                break;

                            case 1://column date
                                ((Label)label).Text = "" + scores[row - 1].DateString;
                                break;

                            case 2://column score
                                Tuple<int, int> score = scores[row - 1].GetScore();
                                int perf = (int)((double)(score.Item1)/ (double)score.Item2)*18 - index;
                                ((Label)label).Text = ((score.Item1 >= 0) ? "+" : "") + score.Item1 + " / " + score.Item2 + " trous";
                                if (perf == 0)//you played your index
                                {
                                    ((Label)label).TextColor = Color.Gray;
                                } else if (perf < 0)//you played better than your index
                                {
                                    ((Label)label).TextColor = Color.Green;
                                } else if (perf < 10)//you played worse than your index
                                {
                                    ((Label)label).TextColor = Color.Orange;
                                } else//you played than your index
                                {
                                    ((Label)label).TextColor = Color.Red;
                                }
                                break;

                            default:
                                break;
                        }
                    }
                    row++;
                    if (row == rowCount)
                    {
                        row = 0;
                        col++;
                    }
                } else
                {
                    row++;
                }
            }
        }


        private void updateMaxDistClubStat()
        {
            Tuple<string, int> maxDist = StatistiquesGolf.getMaxDistClub();
            if (maxDist.Item2 == 0.0)
            {
                this.maxDistClubLabel.Text = "Votre coup le plus long : ";
                this.maxDistClub.Text = "X";
            }
            else
            {
                this.maxDistClubLabel.Text = "Votre coup de " + maxDist.Item1 + " le plus long : ";
                this.maxDistClub.Text = maxDist.Item2 + " m";
            }
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