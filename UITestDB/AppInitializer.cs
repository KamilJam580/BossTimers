using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITestDB
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {

                return ConfigureApp.Android.InstalledApp("com.companyname.testapp").StartApp();

        }
    }
}