using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TestAPp.Models;

namespace TestAPp.Services
{
    public class BossDataStore : IBossDataStore<Boss>
    {
        Realm _realm;
        string Path;
        RealmConfiguration realmConfiguration = null;
        public void FirstCreateBosses()
        {
            Console.WriteLine("Ctor Ctor !!!!!!!!!!!");
            Console.WriteLine("Ctor Ctor !!!!!!!!!!!");
            Console.WriteLine("Ctor Ctor !!!!!!!!!!!");
            try
            {
                //Realm.DeleteRealm(RealmConfiguration.DefaultConfiguration);
                Realm _realm = Realm.GetInstance(realmConfiguration);

                Task task = Task.Run(async () =>
                {
                    IEnumerable<Boss> BossesIE = await GetBossesAsync(true);
                    ObservableCollection<Boss> Bosses = new ObservableCollection<Boss>();

                    foreach (var boss in BossesIE)
                    {
                        Bosses.Add(boss);
                    }

                    Boss MiniArena = new Boss() { Name = "MiniArena", CooldownHours = 20, ImagePath = "Alptramun.png", Id = "0" };
                    await AddBossIfMissed(Bosses, MiniArena);

                    Boss Oberon = new Boss() { Name = "Oberon", CooldownHours = 20, ImagePath = "Oberon.png", Id = "2" };
                    await AddBossIfMissed(Bosses, Oberon);

                    Boss FinalArena = new Boss() { Name = "FinalArena", CooldownHours = 20, ImagePath = "Mean_Lost_Soul.png", Id = "1" };
                    await AddBossIfMissed(Bosses, FinalArena);

                    Boss FinalVen = new Boss() { Name = "FinalVen", CooldownHours = 20, ImagePath = "The_Pale_Worm.png", Id = "3" };
                    await AddBossIfMissed(Bosses, FinalVen);

                    Boss MiniVen = new Boss() { Name = "MiniVen", CooldownHours = 20, ImagePath = "Mean_Lost_Soul.png", Id = "4" };
                    await AddBossIfMissed(Bosses, MiniVen);

                    Boss Drume = new Boss() { Name = "Drume", CooldownHours = 20, ImagePath = "Drume.png", Id = "5" };
                    await AddBossIfMissed(Bosses, Drume);

                    Boss Scarlet = new Boss() { Name = "Scarlet", CooldownHours = 20, ImagePath = "Scarlett_Etzel.png", Id = "6" };
                    await AddBossIfMissed(Bosses, Scarlet);
                    Boss Gold = new Boss() { Name = "Gold", CooldownHours = 20, ImagePath = "Anomaly.png", Id = "7" };
                    await AddBossIfMissed(Bosses, Gold);

                    Boss Ratmiral = new Boss() { Name = "Ratmiral", CooldownHours = 20, ImagePath = "Ratmiral_Blackwhiskers.png", Id = "8" };
                    Boss Faceless = new Boss() { Name = "Faceless", CooldownHours = 20, ImagePath = "Faceless_Bane.png", Id = "9" };
                    Boss AB = new Boss() { Name = "AB", CooldownHours = 20, ImagePath = "Ab.png", Id = "10" };
                    Boss Umahlullu = new Boss() { Name = "Umahlullu", CooldownHours = 20, ImagePath = "Urmahlullu.png", Id = "11" };

                    Boss FinalGold = new Boss() { Name = "FinalGold", CooldownHours = 14 * 24, ImagePath = "FinalGold.png", Id = "12" };
                    Boss FinalFeru = new Boss() { Name = "FinalFeru", CooldownHours = 14 * 24, ImagePath = "Ferumbras.png", Id = "13" };
                    Boss FinaThais = new Boss() { Name = "FinaThais", CooldownHours = 14 * 24, ImagePath = "The_Last_Lore_Keeper.png", Id = "14" };

                    
                    
                });



            }

            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Exception:");
                Console.WriteLine(ex);
            }
        }

        private async Task AddBossIfMissed(ObservableCollection<Boss> Bosses, Boss MiniArena)
        {
            bool contains = Bosses.Any(p => p.Id == MiniArena.Id);
            if (!contains)
            {
                await SetDefeatTime(MiniArena, DateTimeOffset.Now.AddDays(-1000));
                await AddBossAsync(MiniArena);
            }
        }

        public BossDataStore()
        {
        }

        public async Task<bool> AddBossAsync(Boss boss)
        {
            //Realm _realm = Realm.GetInstance(Path);
            Realm _realm = await Realm.GetInstanceAsync(realmConfiguration);
            _realm.Write(() =>
            {
                _realm.Add(boss);
            });
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateBossAsync(Boss boss)
        {
            //Realm _realm = Realm.GetInstance(Path);
            Realm _realm = await Realm.GetInstanceAsync(realmConfiguration);
            Boss oldBoss = _realm.Find<Boss>(boss.Id);
            using (var trans = _realm.BeginWrite())
            {
                _realm.Remove(oldBoss);
                trans.Commit();
            }
            _realm.Write(() =>
            {
                _realm.Add(boss);
            });

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteBossAsync(string id)
        {
            //Realm _realm = Realm.GetInstance(Path);
            Realm _realm = await Realm.GetInstanceAsync(realmConfiguration);
            Boss oldBoss = _realm.Find<Boss>(id);
            using (var trans = _realm.BeginWrite())
            {
                _realm.Remove(oldBoss);
                trans.Commit();
            }
            return await Task.FromResult(true);
        }

        public async Task<Boss> GetBossAsync(string id)
        {

            //Realm _realm = Realm.GetInstance(Path);
            Realm _realm = await Realm.GetInstanceAsync(realmConfiguration);
            return _realm.Find<Boss>(id);
        }

        public async Task<IEnumerable<Boss>> GetBossesAsync(bool forceRefresh = false)
        {
            //Realm _realm = Realm.GetInstance(Path);
            Realm _realm = await Realm.GetInstanceAsync(realmConfiguration);
            IEnumerable<Boss> bosiki = _realm.All<Boss>().ToList<Boss>();
            return await Task.FromResult(bosiki);
        }

        public async Task<bool> Defeat(Boss boss)
        {
            //Realm _realm = Realm.GetInstance(Path);
            Realm _realm = await Realm.GetInstanceAsync(realmConfiguration);
            using (var trans = _realm.BeginWrite())
            {
                boss.Defeated = DateTimeOffset.Now;
                trans.Commit();
            }
            await RefreshTimer(boss);
            return await Task.FromResult(true);
        }

        public async Task<bool> SetDefeatTime(Boss boss, DateTimeOffset time)
        {
            //Realm _realm = Realm.GetInstance(Path);
            Realm _realm = await Realm.GetInstanceAsync(realmConfiguration);
            using (var trans = _realm.BeginWrite())
            {
                boss.Defeated = time;
                trans.Commit();
            }
            _realm.Refresh();
            return await Task.FromResult(true);
        }

        public async Task<bool> RefreshTimer(Boss boss)
        {
            //Realm _realm = Realm.GetInstance(realmConfiguration);
            Realm _realm = await Realm.GetInstanceAsync(realmConfiguration);
            TimeSpan bossTimeSpan = new TimeSpan(boss.CooldownHours, 0, 0);
            TimeSpan elapsed = DateTime.Now - boss.Defeated;
            TimeSpan TimeToDefeatTimeSpan = bossTimeSpan - elapsed;
            string timeToDefeat = StringDateTimeConverter.GetTimeText(TimeToDefeatTimeSpan);
            using (var trans = _realm.BeginWrite())
            {
                boss.TimeToDefeat = timeToDefeat;
                trans.Commit();
            }
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAll()
        {
            Realm.DeleteRealm(realmConfiguration);
            return await Task.FromResult(true);
        }

        public string GetPath()
        {
            return Path;
        }

        public void SetPath(string path)
        {
            this.Path = path;
            realmConfiguration = new RealmConfiguration(Path);
        }
    }
}