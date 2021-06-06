using System;
using System.ComponentModel;
using TestAPp.Models;
using TestAPp.Services;
using TestAPp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestAPp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BossDetailPage : ContentPage
    {
        BossDetailViewModel _viewModel;
        public BossDetailPage()
        {

            InitializeComponent();

            BindingContext = _viewModel = new BossDetailViewModel();
            MessagingCenter.Subscribe<BossesViewModel, Boss>(this, "timerchanged", (sender, arg) =>
            {
                if (arg.Id == _viewModel.BossCopy.Id)
                {
                    Timer.Text = arg.TimeToDefeat;
                }
            });
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();


            /*if (_viewModel.BossCopy != null)
            {
                _viewModel.BossCopy = null;
                BossPageSelected bossPageSelected = BossPageSelected.GetInstance();
                Shell.Current.GoToAsync($"{nameof(bossPageSelected.viewModel)}");
            }*/
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

        }
    }
}