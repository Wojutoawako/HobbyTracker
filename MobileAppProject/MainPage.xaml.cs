using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace MobileAppProject
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            // Вряд ли это нужно использовать здесь
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
        }
    }
}