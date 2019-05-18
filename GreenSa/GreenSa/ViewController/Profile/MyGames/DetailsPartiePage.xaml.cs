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
            int totScore = 0;
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

            score.Margin = new Thickness(responsiveDesign(39), responsiveDesign(27), 0, 0);
            title.Margin = new Thickness(responsiveDesign(65), responsiveDesign(20), 0, 0);
            totalPar.Text = totPar + "";
            totalPutt.Text = totPutt + "";
            totalPen.Text = totPen + "";
            totalScore.Text = totScore + "";
        }

        private int responsiveDesign(int pix)
        {
            return (int)((pix * 4.1 / 1440.0) * Application.Current.MainPage.Width);
        }

    }
}
