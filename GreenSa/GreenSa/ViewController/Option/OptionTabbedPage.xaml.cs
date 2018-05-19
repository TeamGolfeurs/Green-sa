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
    public partial class OptionTabbedPage : TabbedPage
    {
        public OptionTabbedPage()
        {
            InitializeComponent();
        }

        protected  override void OnAppearing()
        {
            var Page1 = new ImportGolfCourse();
            Page1.Title = "Import parcours";
            this.Children.Add(Page1);

            var Page2 = new DatabaseDeletionPage();
            Page2.Title = "Supprimer BD";
            this.Children.Add(Page2);

            base.OnAppearing();
        }
    }
}