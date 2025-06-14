using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileAppProject
{
    public static class Styles
    {
        public static double DeviceWidth =
            DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;

        public static Style LargeButtonStyle = new Style(typeof(Button))
        {
            Setters =
            {
                new Setter() { Property = Button.BackgroundColorProperty, Value = Color.White },
                new Setter() { Property = Button.CornerRadiusProperty, Value = 60 },
                new Setter() { Property = Button.WidthRequestProperty, Value = DeviceWidth * 0.85 },
            }
        };

        public static Style MiniButtonStyle = new Style(typeof(Button))
        {
            Setters =
            {
                new Setter() { Property = Button.BackgroundColorProperty, Value = Color.White },
                new Setter() { Property = Button.TextColorProperty, Value = Color.Black },
                new Setter() { Property = Button.CornerRadiusProperty, Value = 100 },
                new Setter() { Property = Button.WidthRequestProperty, Value = DeviceWidth * 0.2 },
                new Setter() { Property = Button.HeightRequestProperty, Value = DeviceWidth * 0.2 },
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
                new Setter() { Property = Button.WidthRequestProperty, Value = DeviceWidth * 0.1125 },
                new Setter() { Property = Button.HeightRequestProperty, Value = DeviceWidth * 0.1125 },
            }
        };

        public static Style PickerStyle = new Style(typeof(Picker))
        {
            Setters =
            {
                new Setter() { Property = Picker.TitleColorProperty, Value = Color.White },
                new Setter() { Property = Picker.TextColorProperty, Value = Color.White },
            }
        };
    }
}
