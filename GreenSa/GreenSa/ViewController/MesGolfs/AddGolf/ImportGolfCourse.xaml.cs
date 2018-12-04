using GreenSa.Models.GolfModel;
using GreenSa.Models.Tools;
using GreenSa.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenSa.ViewController.MesGolfs.AddGolf
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImportGolfCourse : ContentPage
    {
        public ImportGolfCourse()
        {
            InitializeComponent();
            format.Text = " < GolfCourse > \n<Name> Saint Jacques Parcours 9 trous </ Name >\n < NbTrous > 9 </ NbTrous >\n<NomGolf> Saint Jacques </ NomGolf >\n < Coordinates >\n < Trou >\n < par > 4 </ par >\n < lat > 48,070325 </ lat >\n < lng > -1,746956 </ lng >\n </ Trou >\n < Trou >\n < par > 4 </ par >\n < lat > 48,06957 </ lat >\n < lng > -1,744317 </ lng >\n </ Trou >\n....\n </ Coordinates >\n </ GolfCourse > ";
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            String url = isPastebin.IsToggled?"https://pastebin.com/raw/"+urlText.Text:urlText.Text;
            String res = "";
            try
            {
                status.Text = "Récuperation en cours";

                HttpClient client = new HttpClient();
                 res = await client.GetStringAsync(url);
                status.Text = "Texte récupéré, transformation en parcours de golf";
            }
            catch (Exception ex)
            {
                status.Text = "URL invalide";
                return;
            }
            GolfCourse gc;
            try
            {
                 gc = GolfXMLReader.getSingleGolfCourseFromText(res);
                status.Text = "Transformé en parcours de golf, insertion dans la base de données";
            }
            catch (Exception ex)
            {
                status.Text = "Format de texte non valide";
                return;
            }
            SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();

            try
            {
                connection.CreateTable<Hole>();
                connection.CreateTable<MyPosition>();
                connection.CreateTable<GolfCourse>();
                connection.BeginTransaction();
                SQLiteNetExtensions.Extensions.WriteOperations.InsertWithChildren(connection, gc, true);
                connection.Commit();
                status.Text = "Parcours bien inséré dans la base de données !";
            }
            catch(Exception ex)
            {
                status.Text = "Problème dans l'insertion dans la base de données (duplication,...)";
                connection.Rollback();
            }


        }
    }
}