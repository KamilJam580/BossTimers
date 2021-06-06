using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TestAPp.ViewModels;
using TestAPp.Views;
using Xamarin.Forms;

namespace TestAPp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {

        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(BossDetailPage), typeof(BossDetailPage));

            Routing.RegisterRoute(nameof(BossesPage), typeof(BossesPage));


            Routing.RegisterRoute(nameof(NewBossPage), typeof(NewBossPage));



        }
        protected override void OnNavigated(ShellNavigatedEventArgs args)
        {

            base.OnNavigated(args);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("ARGS LOC Current");
            Console.WriteLine(args.Current.Location.ToString());
            try
            {
                Console.WriteLine("ARGS LOC Previous");
                Console.WriteLine(args.Previous.Location.ToString());
            }
            catch (Exception)
            {

            }
            if (args.Current.Location.ToString().Contains("BossDetailPage"))
            {
                Console.WriteLine("ARGS LOC Current zawiera BossDetailPage");
                if (!args.Previous.Location.ToString().Contains("BossDetailPage"))
                {
                    Console.WriteLine("ARGS LOC Previous nie zawiera BossDetailPage");
                    string pattern = @"[\/]{2,3}(\w+)";
                    Regex rgx = new Regex(pattern);
                    foreach (Match match in rgx.Matches(args.Current.Location.ToString()))
                    {

                        Console.WriteLine("Found '{0}' at position {1}", match.Value.Trim('/'), 
                            match.Index);
                        string name = match.Value.Trim('/');
                        Shell.Current.GoToAsync(name);
                        Shell.Current.Navigation.PopToRootAsync();

                    }


                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

        }

    }
}
