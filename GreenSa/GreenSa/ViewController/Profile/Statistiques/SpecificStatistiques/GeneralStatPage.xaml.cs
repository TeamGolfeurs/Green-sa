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
        }

        private void updateMaxDistClubStat()
        {
            Tuple<string, int> maxDist = this.getMaxDistClub();
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

        private Tuple<string, int> getMaxDistClub()
        {
            SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTable<Shot>();
            List<Shot> shots = SQLiteNetExtensions.Extensions.ReadOperations.GetAllWithChildren<Shot>(connection);
            double maxDist = 0.0;
            string clubName = "";
            foreach (Shot shot in shots)
            {
                double dist = shot.Distance;
                if (dist > maxDist)
                {
                    maxDist = dist;
                    clubName = shot.Club.Name;
                }
            }
            return new Tuple<string, int>(clubName, (int)maxDist);
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