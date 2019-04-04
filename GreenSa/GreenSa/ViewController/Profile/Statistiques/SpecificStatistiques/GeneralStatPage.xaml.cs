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

        public static string NO_DATA_LIST = "-";
        public static string NO_DATA = "NoData";

        public GeneralStatPage()
        {
            InitializeComponent();
            this.updateLast4Scores();
            this.updateMaxDistClubStat();
            this.updateAveragePutts();
            this.updateAveragePar();
        }

        private void updateLast4Scores()
        {
            int index = (int)StatistiquesGolf.getPlayerIndex();
            int rowCount = last4ScoresGrid.Children.Count / 3;
            var allGolfCourses = StatistiquesGolf.getAllGolfCourses();
            var notSortedScores = StatistiquesGolf.getScores();
            var scores = notSortedScores.OrderBy(d => d.DateDebut).ToList();
            //System.Diagnostics.Debug.WriteLine(scores[0].scoreHoles.ToString());
            int col = 0;
            int row = 0;
            foreach (View label in last4ScoresGrid.Children)
            {
                if (row > 0)
                {
                    switch (col)
                    {
                        case 0://column golf name
                            if (row <= scores.Count)
                            {
                                string courseName = "";
                                string id = scores[row - 1].scoreHoles[0].IdHole;
                                foreach (GolfCourse gc in allGolfCourses)
                                {
                                    foreach (Hole h in gc.Holes)
                                    {
                                        if (h.Id.Equals(id))
                                        {
                                            courseName = gc.Name;
                                            break;
                                        }
                                    }
                                }
                                ((Label)label).Text = courseName;
                            }
                            else
                            {
                                ((Label)label).Text = NO_DATA_LIST;
                            }
                            break;

                        case 1://column date
                            if (row <= scores.Count)
                            {
                                ((Label)label).Text = "" + scores[row - 1].DateString;
                            }
                            else
                            {
                                ((Label)label).Text = NO_DATA_LIST;
                            }
                            break;

                        case 2://column score
                            if (row <= scores.Count)
                            {
                                Tuple<int, int> score = scores[row - 1].GetScore();
                                int perf = (int)(score.Item1 / score.Item2 * 18.0) - index;
                                ((Label)label).Text = ((score.Item1 >= 0) ? "+" : "") + score.Item1 + " / " + score.Item2 + " trous";
                                if (perf == 0)//you played your index
                                {
                                    ((Label)label).TextColor = Color.Gray;
                                }
                                else if (perf < 0)//you played better than your index
                                {
                                    ((Label)label).TextColor = Color.Green;
                                }
                                else if (perf < 10)//you played worse than your index
                                {
                                    ((Label)label).TextColor = Color.Orange;
                                }
                                else//you played than your index
                                {
                                    ((Label)label).TextColor = Color.Red;
                                }
                            }
                            else
                            {
                                ((Label)label).Text = NO_DATA_LIST;
                            }
                            break;

                        default:
                            break;
                    }
                    row++;
                    if (row == rowCount)
                    {
                        row = 0;
                        col++;
                    }
                }
                else
                {
                    row++;
                }
            }
        }


        private void updateAveragePar()
        {
            double avPars = StatistiquesGolf.getAveragePars();
            if (avPars == -1.0)
            {
                this.averagePars.Text = NO_DATA;
                this.averagePars.TextColor = Color.Gray;
                this.averagePars.FontSize = 15;
            }
            else
            {
                this.averagePars.Text = "" + avPars.ToString("0.00");
                this.averagePars.TextColor = Color.FromHex("#39B54A");
                this.averagePars.FontSize = 30;
            }
        }

        private void updateAveragePutts()
        {
            double avPutts = StatistiquesGolf.getAveragePutts();
            if (avPutts == -1.0)
            {
                this.averagePutts.Text = NO_DATA;
                this.averagePutts.TextColor = Color.Gray;
                this.averagePutts.FontSize = 15;
            }
            else
            {
                this.averagePutts.Text = "" + avPutts.ToString("0.00");
                this.averagePutts.TextColor = Color.FromHex("#39B54A");
                this.averagePutts.FontSize = 30;
            }
        }



        private void updateMaxDistClubStat()
        {
            Tuple<string, int> maxDist = StatistiquesGolf.getMaxDistClub();
            if (maxDist.Item2 == 0.0)
            {
                this.maxDistClubLabel.Text = "Coup le plus long";
                this.maxDistClub.Text = NO_DATA;
                this.maxDistClub.TextColor = Color.Gray;
                this.maxDistClub.FontSize = 15;
            }
            else
            {
                this.maxDistClubLabel.Text = maxDist.Item1 + " le plus long";
                this.maxDistClub.Text = maxDist.Item2 + " m";
                this.maxDistClub.TextColor = Color.FromHex("#39B54A");
                this.maxDistClub.FontSize = 30;
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