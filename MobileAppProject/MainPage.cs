using Xamarin.Forms;

namespace MobileAppProject
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.SetToolbarPlacement(this,
                Xamarin.Forms.PlatformConfiguration.AndroidSpecific.ToolbarPlacement.Bottom);

            NavigationPage.SetHasNavigationBar(this, false);

            this.BarBackgroundColor = Color.Black;

            this.UnselectedTabColor = Color.LightGray;
            this.SelectedTabColor = Color.Yellow;

            Children.Add(new HobbyListPage()
            {
                Title = "Мои хобби",
                IconImageSource = "hobbies.png",
            });

            Children.Add(new SchedulePage()
            {
                Title = "Планы",
                IconImageSource = "schedule.png",
            });
            
            Children.Add(new NotesPage()
            {
                Title = "Заметки",
                IconImageSource = "notes.png",
            });
        }
    }
}