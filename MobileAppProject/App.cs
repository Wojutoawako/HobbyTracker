using Newtonsoft.Json;
using PCLStorage;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Forms;

namespace MobileAppProject
{
    public partial class App : Application
    {
        public App()
        {
            Current.Resources.Add(Styles.LargeButtonStyle);

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override async void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
