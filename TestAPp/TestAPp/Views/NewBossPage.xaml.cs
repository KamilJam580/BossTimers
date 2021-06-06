using System;
using System.Collections.Generic;
using System.ComponentModel;
using TestAPp.Models;
using TestAPp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestAPp.Views
{
    public partial class NewBossPage : ContentPage
    {
        public Boss Boss { get; set; }
        NewBossViewModel _viewModel;
        public NewBossPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new NewBossViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //_viewModel.OnAppearing();
        }
    }
}