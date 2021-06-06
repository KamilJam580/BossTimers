using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Realms;
using TestAPp.Models;
using TestAPp.Services;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITestDB
{
    [TestFixture(Platform.Android)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {

            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void WelcomeTextIsDisplayed()
        {
            BossDataStore mockBossDataStore = new BossDataStore();
            Task task = new Task(async () =>
            {
                Boss boss = new Boss();
                boss.Name = "Bosik";
                boss.Id = "0";
                await mockBossDataStore.AddBossAsync(boss);
                Boss boss2 = await mockBossDataStore.GetBossAsync("0");

                Assert.AreEqual(boss.Name, boss2.Name);

            });


        }
        [Test]
        public void DefeatTestAsync()
        {
            BossDataStore mockBossDataStore = new BossDataStore();
            Task task = new Task(async () =>
            {
                Boss boss = new Boss() { Name = "MiniArena", CooldownHours = 20, Id = "0" };
                await mockBossDataStore.AddBossAsync(boss);
                await mockBossDataStore.Defeat(boss);
                //Thread.Sleep(5000);
                await mockBossDataStore.RefreshTimer(boss);
                Boss boss2 = await mockBossDataStore.GetBossAsync("0");
                Trace.WriteLine(boss2.Name);
                Trace.WriteLine(boss2.TimeToDefeat);

                Assert.AreEqual("19:59:59", boss2.TimeToDefeat);
            });

        }
        [Test]
        public void SetDefeatTest()
        {
            BossDataStore mockBossDataStore = new BossDataStore();
            Task task = new Task(async () =>
            {
                Boss boss = new Boss();
                boss.Name = "Bosik";
                boss.Id = "0";
                boss.CooldownHours = 20;
                await mockBossDataStore.AddBossAsync(boss);
                await mockBossDataStore.SetDefeatTime(boss, DateTimeOffset.Now.AddHours(-4));
                await mockBossDataStore.RefreshTimer(boss);
                Boss boss2 = await mockBossDataStore.GetBossAsync("0");
                Trace.WriteLine(boss2.Name);
                Trace.WriteLine(boss2.TimeToDefeat);

                Assert.AreEqual("15:59:59", boss2.TimeToDefeat);
            });


        }
        [Test]
        public void AddBossesAndCheck()
        {
            BossDataStore mockBossDataStore = new BossDataStore();
            Task task = new Task(async () =>
            {
                await mockBossDataStore.DeleteAll();

                Boss MiniArena = new Boss() { Name = "MiniArena", CooldownHours = 20, ImagePath = "Alptramun.png", Id = Guid.NewGuid().ToString(), TimeToDefeat = "22" };
                Boss FinalArena = new Boss() { Name = "FinalArena", CooldownHours = 20, ImagePath = "Mean_Lost_Soul.png", Id = Guid.NewGuid().ToString() };
                Boss Oberon = new Boss() { Name = "Oberon", CooldownHours = 20, ImagePath = "Oberon.png", Id = Guid.NewGuid().ToString() };
                Boss FinalVen = new Boss() { Name = "FinalVen", CooldownHours = 20, ImagePath = "The_Pale_Worm.png", Id = Guid.NewGuid().ToString() };
                await mockBossDataStore.AddBossAsync(MiniArena);
                await mockBossDataStore.AddBossAsync(FinalArena);
                await mockBossDataStore.AddBossAsync(Oberon);
                await mockBossDataStore.AddBossAsync(FinalVen);
                var temp = await mockBossDataStore.GetBossesAsync();
                List<Boss> bosses = temp.ToList();


                Assert.AreEqual(4, bosses.Count);
            });

        }

        [Test]
        public void AddBossesDeleteAndCheck()
        {
            //Realm.DeleteRealm(RealmConfiguration.DefaultConfiguration);
            BossDataStore mockBossDataStore = new BossDataStore();
            Task task = new Task(async () =>
            {
                await mockBossDataStore.DeleteAll();

                Boss MiniArena = new Boss() { Name = "MiniArena", CooldownHours = 20, ImagePath = "Alptramun.png", Id = Guid.NewGuid().ToString(), TimeToDefeat = "22" };
                Boss FinalArena = new Boss() { Name = "FinalArena", CooldownHours = 20, ImagePath = "Mean_Lost_Soul.png", Id = Guid.NewGuid().ToString() };
                Boss Oberon = new Boss() { Name = "Oberon", CooldownHours = 20, ImagePath = "Oberon.png", Id = Guid.NewGuid().ToString() };
                Boss FinalVen = new Boss() { Name = "FinalVen", CooldownHours = 20, ImagePath = "The_Pale_Worm.png", Id = "0" };
                await mockBossDataStore.AddBossAsync(MiniArena);
                await mockBossDataStore.AddBossAsync(FinalArena);
                await mockBossDataStore.AddBossAsync(Oberon);
                await mockBossDataStore.AddBossAsync(FinalVen);
                await mockBossDataStore.DeleteBossAsync("0");
                var temp = await mockBossDataStore.GetBossesAsync();
                List<Boss> bosses = temp.ToList();
                foreach (var item in bosses)
                {
                    Console.WriteLine(item.Name);
                }
                Assert.AreEqual(4, bosses.Count);

            });
        }
        [Test]
        public void TestDifferentDatabasses()
        {

            RealmConfiguration realmConfiguration1 = new RealmConfiguration("1");
            RealmConfiguration realmConfiguration2 = new RealmConfiguration("2");
            Realm.DeleteRealm(realmConfiguration1);
            Realm.DeleteRealm(realmConfiguration2);

            Task task = new Task(async () =>
            {


                Realm realm1 = await Realm.GetInstanceAsync(realmConfiguration1);
                Realm realm2 = await Realm.GetInstanceAsync(realmConfiguration2);
                //Realm realm1 = Realm.GetInstance();
                //Realm realm2 = Realm.GetInstance();

                TestClassObject obj1 = new TestClassObject();
                obj1.name = "Elko1";

                TestClassObject obj2 = new TestClassObject();
                obj2.name = "Elko2";


                realm1.Write(() =>
                {
                    realm1.Add(obj1);
                });

                var objectsdb = realm2.All<TestClassObject>().ToList<TestClassObject>();

                Assert.AreEqual(0, objectsdb.Count);
            });

        }
        [Test]
        public void TestDifferentDatabasses2()
        {

            Task task = new Task(async () =>
            {
                BossDataStore mockBossDataStore = new BossDataStore();
                mockBossDataStore.SetPath("1");
                await mockBossDataStore.DeleteAll();

                BossDataStore mockBossDataStore2 = new BossDataStore();
                mockBossDataStore2.SetPath("2");
                await mockBossDataStore2.DeleteAll();

                Boss Oberon = new Boss() { Name = "Oberon", CooldownHours = 20, ImagePath = "Oberon.png", Id = "2" };
                await mockBossDataStore.AddBossAsync(Oberon);

                var temp = await mockBossDataStore.GetBossesAsync();
                List<Boss> bosses = temp.ToList();

                Assert.AreEqual(1, bosses.Count);
            });

        }
        [Test]
        public void TestDifferentDatabasses3()
        {

            Task task = new Task(async () =>
            {
                BossDataStore mockBossDataStore = new BossDataStore();
                mockBossDataStore.SetPath("1");
                await mockBossDataStore.DeleteAll();

                BossDataStore mockBossDataStore2 = new BossDataStore();
                mockBossDataStore2.SetPath("2");
                await mockBossDataStore2.DeleteAll();

                Boss Oberon = new Boss() { Name = "Oberon", CooldownHours = 20, ImagePath = "Oberon.png", Id = "2" };
                await mockBossDataStore.AddBossAsync(Oberon);


                BossDataStore mockBossDataStore3 = new BossDataStore();
                mockBossDataStore3.SetPath("1");

                var temp = await mockBossDataStore3.GetBossesAsync();
                List<Boss> bosses = temp.ToList();

                Assert.AreEqual(1, bosses.Count);
            });

        }
        public class TestClassObject : RealmObject
        {
            public string name { get; set; }

        }

    }
}
