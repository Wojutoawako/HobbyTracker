using Google.Android.Material.BottomNavigation;
using MobileAppProject.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(TabbedPageRendererExtention))]
namespace MobileAppProject.Droid.Renderers
{
    public class TabbedPageRendererExtention : TabbedPageRenderer
    {
        public TabbedPageRendererExtention()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var tp = e.NewElement;
                var bottomNavigationView = (GetChildAt(0) as Android.Widget.RelativeLayout).GetChildAt(1) as BottomNavigationView;
                
            }
        }
    }
}