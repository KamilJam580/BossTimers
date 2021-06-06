using System;
using System.ComponentModel;
using TestAPp.Services;
using TestAPp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestAPp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BossesPage : ContentPage
    {
        

        public BossesPage()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("BossesPage ctor");
            Console.WriteLine("BossesPage ctor");
            Console.WriteLine("BossesPage ctor");
            Console.WriteLine("BossesPage ctor");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            InitializeComponent();
            BossPageSelected bossPageSelected = BossPageSelected.GetInstance();
            if (bossPageSelected.viewModel != null)
            {
                bossPageSelected.viewModel.OnDisAppearing();
            }
            BindingContext = bossPageSelected.viewModel = new BossesViewModel("default");
        }

        protected override void OnAppearing()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("appering");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            base.OnAppearing();
            BossPageSelected bossPageSelected = BossPageSelected.GetInstance();
            
            if (bossPageSelected.viewModel!=null)
            {
                bossPageSelected.viewModel.OnDisAppearing();
            }
            BindingContext = bossPageSelected.viewModel = new BossesViewModel("default");
            bossPageSelected.dbPath = "default";
            bossPageSelected.viewModel.OnAppearing("default");
        }


    }


}