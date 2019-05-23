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
            List<DisplayScoreCard> list = new List<DisplayScoreCard>();
            int i = 1;
            int totPar = 0;
            int totPutt = 0;
            int totPen = 0;
            var totScore = 0;
            foreach (ScoreHole sh in sp.scoreHoles)
            {
                list.Add(new DisplayScoreCard(i, sh));
                i++;
                totPar += sh.Hole.Par;
                totPutt += sh.NombrePutt;
                totPen += sh.Penality;
                totScore += (sh.Score + sh.Hole.Par);
            }
            listScore.ItemsSource = list;

            var scoreDelta = totScore - totPar;
            var sdAbs = Math.Abs(scoreDelta);
            if (scoreDelta < 0)
            {
                score.Text = "-" + sdAbs;
            }
            else
            {
                score.Text = "+" + scoreDelta;
            }
            if (sdAbs >= 100) {
                score.FontSize = 25;
                score.Margin = new Thickness(responsiveDesign(32), responsiveDesign(33), 0, 0);
            }
            else if (sdAbs >= 10)
            {
                score.FontSize = 30;
                score.Margin = new Thickness(responsiveDesign(35), responsiveDesign(29), 0, 0);
            }
            else
            {
                score.Margin = new Thickness(responsiveDesign(39), responsiveDesign(27), 0, 0);
            }
            title.Margin = new Thickness(responsiveDesign(65), responsiveDesign(20), 0, 0);
            totalPar.Text = totPar + "";
            totalPutt.Text = totPutt + "";
            totalPen.Text = totPen + "";
            totalScore.Text = totScore + "";

            partie.Text = sp.GolfName + " :";
            date.Text = sp.DateString;
        }

        private int responsiveDesign(int pix)
        {
            return (int)((pix * 4.1 / 1440.0) * Application.Current.MainPage.Width);
        }

    }
}
