using System;
using TestAPp.Services;
using TestAPp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestAPp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();


            DependencyService.Register<BossDataStore>();

            
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
