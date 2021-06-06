using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using TestAPp.Models;
using TestAPp.Services;
using TestAPp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TestAPp.ViewModels
{

    public class BossesViewModel : BaseViewModel
    {
        private Boss _selectedBoss;
        public ObservableCollection<Boss> Bosses { get; set; }
        public Command LoadBossesCommand { get; }
        public Command AddBossCommand { get; }
        public Command<Boss> BossTapped { get; }
        private bool visable;
        public Thread t;
        public void SetDatabase(string path)
        {
            UserBossDataStore.SetPath(path);
        }
        public BossesViewModel(string path)
        {
            Title = "Boss Timers";
            UserBossDataStore.SetPath(path);
            UserBossDataStore.FirstCreateBosses();
            Bosses = new ObservableCollection<Boss>();

            LoadBossesCommand = new Command(async () => await ExecuteLoadBossesCommand());
            BossTapped = new Command<Boss>(OnBossSelected);
            AddBossCommand = new Command(OnAddBoss);

            t = new Thread(new ThreadStart(UpdateTimer));
            t.Start();
        }
        public void UpdateTimer()
        {
            while (true)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    foreach (var boss in Bosses)
                    {
                        try
                        {
                            
                            UserBossDataStore.RefreshTimer(boss);
                            MessagingCenter.Send<BossesViewModel, Boss>(this, "timerchanged", boss);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                    }
                });
                Thread.Sleep(250);
            }
        }
        async Task ExecuteLoadBossesCommand()
        {
            IsBusy = true;

            try
            {
                Bosses.Clear();
                var bosses = await UserBossDataStore.GetBossesAsync(true);
                foreach (var boss in bosses)
                {

                    Bosses.Add(boss);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

        }



        public void OnAppearing(string path)
        {
            visable = true;
            UserBossDataStore.SetPath(path);
            IsBusy = true;
            SelectedBoss = null;
        }
        public void OnDisAppearing()
        {
            visable = false;

            IsBusy = true;
            SelectedBoss = null;
            t.Abort();
        }
        public Boss SelectedBoss
        {
            get => _selectedBoss;
            set
            {
                SetProperty(ref _selectedBoss, value);
                OnBossSelected(value);
            }
        }

        private async void OnAddBoss(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewBossPage));
        }

        async void OnBossSelected(Boss boss)
        {

            if (boss == null)
                return;
            Console.WriteLine("Boss selected");
            //Console.WriteLine("boss id= " + boss.Id);
            Console.WriteLine("End Bosses View Model");

            await Shell.Current.GoToAsync($"{nameof(BossDetailPage)}?{nameof(BossDetailViewModel.Id)}={boss.Id}");
     }




    }
}