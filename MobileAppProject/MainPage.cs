using Xamarin.Forms;

namespace MobileAppProject
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.SetToolbarPlacement(this,
                Xamarin.Forms.PlatformConfiguration.AndroidSpecific.ToolbarPlacement.Bottom);

            Children.Add(new HobbyListPage()
            {
                Title = "My Hobbies",
            });

            Children.Add(new SchedulePage()
            {
                Title = "Schedule",
            });
            
            Children.Add(new NotesPage()
            {
                Title = "Notes",
            });
        }
    }
}