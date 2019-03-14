using GreenSa.ViewController.Option;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenSa.ViewController.MesGolfs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GolfsManager : ContentPage
    {
        public GolfsManager()
        {
            InitializeComponent();
        }

        /**
         * Méthode déclenchée au click sur le bouton "Ajouter un golf"
         * Redirige vers la page "ImportGolfCourse"
         * */
        async private void OnAddGolfClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new ImportGolfCourse());
            } catch (TargetInvocationException exception)
            {
                Debug.WriteLine(exception.StackTrace);
            }
        }
    }
}