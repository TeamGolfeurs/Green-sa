using GreenSa.Models.GolfModel;
using GreenSa.Models.Tools;
using GreenSa.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenSa.ViewController.Option
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatabaseDeletionPage : ContentPage
    {
        public DatabaseDeletionPage()
        {
            InitializeComponent();
            lab.Text = "Attention, manoeuvre irréversible.";
        }

        private async void dropAllTable(object sender, EventArgs e)
        {
           bool delete= await DisplayAlert("Attention", "Attention, manoeuvre irréversible.", "OK", "Annuler");
            if (!delete)
                return;
            try
            {
                SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();
                connection.CreateTable<MyPosition>();
                connection.CreateTable<GolfCourse>();
                //connection.CreateTable<Club>();
                connection.CreateTable<Shot>();
                connection.CreateTable<Hole>();
                connection.CreateTable<ScoreHole>();
                connection.CreateTable<ScorePartie>();

                connection.DropTable<MyPosition>();
                connection.DropTable<GolfCourse>();
                //connection.DropTable<Club>();
                connection.DropTable<Shot>();
                connection.DropTable<Hole>();
                connection.DropTable<ScoreHole>();
                connection.DropTable<ScorePartie>();

                lab.Text = "Base de donnée bien supprimée";

            }
            catch (Exception )
            {
                lab.Text = "Erreur avec le suppression de la base de données";
            }

         
        }
    }
}