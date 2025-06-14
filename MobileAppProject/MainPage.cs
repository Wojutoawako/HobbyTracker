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
                Title = "My Hobbies",
                IconImageSource = "hobbies.png",
            });

            Children.Add(new SchedulePage()
            {
                Title = "Schedule",
                IconImageSource = "schedule.png",
            });
            
            Children.Add(new NotesPage()
            {
                Title = "Notes",
                IconImageSource = "notes.png",
            });
        }
    }
}