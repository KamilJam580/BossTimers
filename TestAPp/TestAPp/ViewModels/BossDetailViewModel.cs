using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TestAPp.Models;
using TestAPp.Services;
using Xamarin.Forms;

namespace TestAPp.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class BossDetailViewModel : BaseViewModel
    {
        public BossDetailViewModel()
        {
            DefeatCommand = new Command<string>(async (idA) => await OnDefeatCommand(idA));
            UnDefeatCommand = new Command<string>(async (idA) => await OnUnDefeatCommand(idA));


        }
        public Command DefeatCommand { get; }
        async Task OnDefeatCommand(string id)
        {
            Boss boss = await UserBossDataStore.GetBossAsync(id);
            await UserBossDataStore.Defeat(boss);
        }

        public Command UnDefeatCommand { get; }
        async Task OnUnDefeatCommand(string id)
        {
            Boss boss = await UserBossDataStore.GetBossAsync(id);
            await UserBossDataStore.SetDefeatTime(boss, DateTimeOffset.Now.AddDays(-1000));
        }

        private string id;
        public string Id
        {
            get => id;
            set 
            { 
                LoadBoss(value);
                SetProperty(ref id, value);
            }
        }
        
        private Boss boss;
        public Boss BossCopy
        {
            get => boss;
            set => SetProperty(ref boss, value);
        }
        public void OnAppearing()
        {
            UserBossDataStore.SetPath(dbPath);
            //IsBusy = true;
            //SelectedCD = null;
        }


        private string dbPath;
        public async void LoadBoss(string bossId)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Load Boss ID !!!!!!!!!!!!");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            BossPageSelected bossPageSelected = BossPageSelected.GetInstance();
            dbPath = bossPageSelected.dbPath;
            UserBossDataStore.SetPath(dbPath);
            try
            {
                var boss = await UserBossDataStore.GetBossAsync(bossId);
                BossCopy = boss;    
                
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
