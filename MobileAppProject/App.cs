using Xamarin.Forms;

namespace MobileAppProject
{
    public partial class App : Application
    {
        public App()
        {
            Current.Resources.Add(Styles.LargeButtonStyle);

            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.Black,
                BarTextColor = Color.White,
            };
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
