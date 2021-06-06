using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAPp.Services;
using TestAPp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestAPp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BossesPage2 : ContentPage
    {
        BossesViewModel _viewModel;

        public BossesPage2()
        {
            InitializeComponent();
            BossPageSelected bossPageSelected = BossPageSelected.GetInstance();
            if (bossPageSelected.viewModel != null)
            {
                bossPageSelected.viewModel.OnDisAppearing();
            }
            BindingContext = bossPageSelected.viewModel = new BossesViewModel("default2");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BossPageSelected bossPageSelected = BossPageSelected.GetInstance();
            if (bossPageSelected.viewModel != null)
            {
                bossPageSelected.viewModel.OnDisAppearing();
            }
            BindingContext = bossPageSelected.viewModel = new BossesViewModel("default2");
            bossPageSelected.dbPath = "default2";
            bossPageSelected.viewModel.OnAppearing("default2");
        }


    }
}