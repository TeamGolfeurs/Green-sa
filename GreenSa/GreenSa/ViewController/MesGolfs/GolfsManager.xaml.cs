﻿using GreenSa.Models.GolfModel;
using GreenSa.Models.ViewElements;
using GreenSa.ViewController.Option;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public GolfCourseListViewModel gclvm;

        public GolfsManager()
        {
            InitializeComponent();
            
        }

        /**
         * Méthode qui s'execute automatiquement au chargement de la page GolfsManager
         * */
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //Recupere la liste des golfs
            List<GolfCourse> res = await GestionGolfs.getListGolfsAsync(null);
            //Met à jour la liste des golfs dans la vue
            gclvm = new GolfCourseListViewModel(res);
            String str = "";
            /*foreach (GolfCourse gc in gclvm.GolfCourses)
            {
                str += gc.ToString();
            }
            await this.DisplayAlert("Test", str, "test");*/
            BindingContext = gclvm;
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

        private async void DeleteGolfCourse(object sender, EventArgs e)
        {
            var image = sender as Image;
            var tgr = image.GestureRecognizers[0] as TapGestureRecognizer;
            var name = tgr.CommandParameter.ToString();
            var confirmDelete = await this.DisplayAlert("Suppression d'un golf", "Voulez vous vraiment supprimer le golf : "+ name + " ?", "Oui", "Non");
            if (confirmDelete)
            {
                var toDelete = image.BindingContext as GolfCourse;
                var vm = BindingContext as GolfCourseListViewModel;
                vm.RemoveGolfCourse.Execute(toDelete);
                await this.DisplayAlert("Suppression d'un golf", name + " a été supprimé avec succès !", "Ok");
            }
        }
    }
}