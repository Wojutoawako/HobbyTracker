using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileAppProject
{
    public static class Styles
    {
        public static Style LargeButtonStyle = new Style(typeof(Button))
        {
            Setters =
            {
                new Setter() { Property = Button.BackgroundColorProperty, Value = Color.White },
                new Setter() { Property = Button.CornerRadiusProperty, Value = 60 },
                new Setter() { Property = Button.WidthRequestProperty, Value = DeviceDisplay.MainDisplayInfo.Width - 20 },
            }
        };

        public static Style MiniButtonStyle = new Style(typeof(Button))
        {
            Setters =
            {
                new Setter() { Property = Button.BackgroundColorProperty, Value = Color.White },
                new Setter() { Property = Button.TextColorProperty, Value = Color.Black },
                new Setter() { Property = Button.CornerRadiusProperty, Value = 100 },
                new Setter() { Property = Button.WidthRequestProperty, Value = 60 },
                new Setter() { Property = Button.HeightRequestProperty, Value = 60 },
            }
        };

        public static Style MicroButtonStyle = new Style(typeof(Button))
        {
            Setters =
            {
                new Setter() { Property = Button.BackgroundColorProperty, Value = Color.White },
                new Setter() { Property = Button.TextColorProperty, Value = Color.DeepSkyBlue },
                new Setter() { Property = Button.BorderColorProperty, Value = Color.DeepSkyBlue },
                new Setter() { Property = Button.CornerRadiusProperty, Value = 15 },
                new Setter() { Property = Button.BorderWidthProperty, Value = 1 },
                new Setter() { Property = Button.WidthRequestProperty, Value = 45 },
                new Setter() { Property = Button.HeightRequestProperty, Value = 45 },
            }
        };
    }
}
