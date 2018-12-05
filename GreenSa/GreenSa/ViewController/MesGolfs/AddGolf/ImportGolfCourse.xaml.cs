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
using Xamarin.Forms.Maps;

namespace GreenSa.ViewController.Option
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImportGolfCourse : ContentPage
    {
        public ImportGolfCourse()
        {
            InitializeComponent();
            map.update();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            
            /*SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();

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
            }*/


        }
    }
}