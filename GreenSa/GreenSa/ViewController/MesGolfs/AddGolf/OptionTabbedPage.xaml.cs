using GreenSa.ViewController.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenSa.ViewController.MesGolfs.AddGolf
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OptionTabbedPage : TabbedPage
    {
        public OptionTabbedPage()
        {
            InitializeComponent();

            var Page1 = new ImportGolfCourse();
            Page1.Title = "Import parcours";
            this.Children.Add(Page1);

            var Page2 = new GreenSa.ViewController.Option.DatabaseDeletionPage();
            Page2.Title = "Supprimer BD";
            this.Children.Add(Page2);
        }

        protected  override void OnAppearing()
        {

            base.OnAppearing();
        }
    }
}