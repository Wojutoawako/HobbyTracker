using PCLStorage;
using Xamarin.Forms;

namespace MobileAppProject
{
    public partial class App : Application
    {
        public static IFolder RootFolder = FileSystem.Current.LocalStorage;
        public static IFolder SaveFolder;

        public App()
        {
            SaveFolder = RootFolder.CreateFolderAsync("HTSave", CreationCollisionOption.OpenIfExists).Result;

            Current.Resources.Add(Styles.LargeButtonStyle);

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            var mainPage = (MainPage as NavigationPage).RootPage as TabbedPage;

            foreach (var page in mainPage.Children)
            {
                if (page is ContentPageExtension pageExtended)
                    pageExtended?.LoadPageData();
            }
        }

        protected override void OnSleep()
        {
            var mainPage = (MainPage as NavigationPage).RootPage as TabbedPage;

            foreach (var page in mainPage.Children)
            {
                if (page is ContentPageExtension pageExtended)
                    pageExtended?.SavePageData();
            }
        }

        protected override void OnResume()
        {
        }
    }
}
