using GreenSa.Models.GolfModel;
using GreenSa.ViewController.Profile.Statistiques.StatistiquesGolfCourse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenSa.ViewController.Profile.MyGames
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewPartieListPage : ContentPage
    {
        private bool isInStat;
        private PartieStatPage partieStatPage;

        public ViewPartieListPage()
        {
            InitializeComponent();
            this.isInStat = false;
            this.partieStatPage = null;
        }

        public ViewPartieListPage(bool isInStat)
        {
            InitializeComponent();
            this.isInStat = isInStat;
            this.partieStatPage = null;
        }

        async protected override void OnAppearing()
        {
            try {
                IEnumerable<ScorePartie> list = (await StatistiquesGolf.getScoreParties()).OrderByDescending(d => d.DateDebut).ToList();
                List<GolfCourse> allGolfCourses = await StatistiquesGolf.getGolfCourses();
                string id = "";
                foreach (ScorePartie sp in list)
                {
                    id = sp.scoreHoles[0].IdHole;
                    foreach (GolfCourse gc in allGolfCourses)
                    {
                        foreach (Hole h in gc.Holes)
                        {
                            if (h.Id.Equals(id))
                            {
                                sp.GolfName = gc.Name;
                                break;
                            }
                        }
                    }
                }
                listPartie.ItemsSource = list;
            } catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error : " + e.StackTrace);
            }
            
        }

        private async void listPartie_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (!isInStat)
            {
                await Navigation.PushModalAsync(new DetailsPartiePage((ScorePartie)listPartie.SelectedItem));
            } else
            {
                ScorePartie sp = (ScorePartie)listPartie.SelectedItem;
                if (this.partieStatPage == null)
                {
                    this.partieStatPage = new PartieStatPage(sp, sp.GolfName);
                }
                else
                {
                    this.partieStatPage.changePartie(sp, sp.GolfName);
                }
                await Navigation.PushModalAsync(this.partieStatPage);
            }
        }
    }
}