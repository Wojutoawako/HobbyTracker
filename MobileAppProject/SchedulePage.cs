using System.Globalization;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Controls;
using System.Collections.Generic;
using Xamarin.Essentials;
using System.Collections.ObjectModel;

namespace MobileAppProject
{
    public class SchedulePage : ContentPage
    {
        public static Xamarin.Plugin.Calendar.Controls.Calendar Calendar;

        private static DataTemplate EventDataTemplate = new DataTemplate(() =>
        {
            var frame = new Frame()
            {
                Margin = new Thickness(10, 0, 10, 10),
                Padding = new Thickness(10, 10),

                BorderColor = Color.Black,
                CornerRadius = 15,
            };

            var templateContent = new FlexLayout()
            {
                Direction = FlexDirection.Row,

                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,

                JustifyContent = FlexJustify.SpaceBetween,
                AlignItems = FlexAlignItems.Center,
            };

            var templateText = new FlexLayout()
            {
                Direction = FlexDirection.Column,
            };
            FlexLayout.SetGrow(templateText, 2);

            var templateButtons = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,

                HorizontalOptions = LayoutOptions.End,

                Spacing = 10,
            };

            var timeLabel = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            timeLabel.SetBinding(Label.TextProperty, "ActivityTime");

            var nameLabel = new Label()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            nameLabel.SetBinding(Label.TextProperty, "Hobby.Name");

            ActivityModel activity = null;
            timeLabel.BindingContextChanged += (sender, args) =>
            {
                if (timeLabel.BindingContext is ActivityModel act)
                    activity = act;
            };

            var deleteButton = new Button()
            {
                Text = "D",

                HorizontalOptions = LayoutOptions.End,

                Command = new Command(() =>
                {
                    var collection = Calendar.Events[activity.ActivityTime] as ObservableCollection<ActivityModel>;
                    collection.RemoveAt(collection.IndexOf(activity));
                }),

                Style = Styles.MicroButtonStyle,
            };

            var editButton = new Button()
            {
                Text = "E",

                HorizontalOptions = LayoutOptions.End,

                Command = new Command(() =>
                {
                    (App.Current.MainPage as NavigationPage).Navigation.PushAsync(new ActivityPage(activity));
                }),

                Style = Styles.MicroButtonStyle,
            };

            templateText.Children.Add(timeLabel);
            templateText.Children.Add(nameLabel);

            templateButtons.Children.Add(editButton);
            templateButtons.Children.Add(deleteButton);

            templateContent.Children.Add(new ContentView()
            {
                WidthRequest = DeviceDisplay.MainDisplayInfo.Width / 2,
                Content = templateText,
            });
            templateContent.Children.Add(new ContentView()
            {
                WidthRequest = DeviceDisplay.MainDisplayInfo.Width / 2,
                Content = templateButtons,
            });

            frame.Content = templateContent;

            return frame;
        });

        public SchedulePage()
        {
            BackgroundImageSource = "pink_bicycle.png";

            Calendar = new RangeSelectionCalendar()
            {
                Padding = new Thickness(10),

                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,

                SelectedDates = new List<System.DateTime>(),
                EventTemplate = EventDataTemplate,

                Culture = new CultureInfo("ru-RU"),

                MonthLabelColor = Color.White,
                YearLabelColor = Color.White,

                SelectedDateColor = Color.White,

                DaysTitleColor = Color.White,

                DeselectedDayTextColor = Color.White,

                SelectedDayBackgroundColor = Color.White,
                SelectedDayTextColor = Color.Black,

                TodayOutlineColor = Color.White,
                TodayFillColor = Color.LightGray,
                TodayTextColor = Color.Black,

                ArrowsColor = Color.Black,

                EventIndicatorColor = Color.White,
                EventIndicatorTextColor = Color.White,
                EventIndicatorSelectedColor = Color.Red,
                EventIndicatorSelectedTextColor = Color.Black,

                OtherMonthDayIsVisible = false,
                OtherMonthDayColor = Color.Transparent,
            };

            var layout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,

                BackgroundColor = Color.Transparent,
            };

            var buttonBox = new AbsoluteLayout()
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,
            };

            var addActivityButton = new Button()
            {
                Margin = new Thickness(0, 0, 10, 10),

                Text = "+",
                FontSize = 30,

                MinimumHeightRequest = 70,
                MinimumWidthRequest = 100,

                Style = Styles.MiniButtonStyle,
            };

            buttonBox.Children.Add(addActivityButton);

            addActivityButton.Pressed += AddActivity;

            layout.Children.Add(Calendar);
            layout.Children.Add(buttonBox);

            Content = layout;
        }

        private async void AddActivity(object sender, System.EventArgs e)
        {
            if (Calendar.SelectedDates.Count > 0)
            {
                var activityPage = new ActivityPage();
                await Navigation.PushAsync(activityPage);
            } else
            {
                return;
            }
        }
    }
}