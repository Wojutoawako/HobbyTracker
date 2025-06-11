using System.Globalization;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Controls;
using System.Collections.Generic;
using Xamarin.Essentials;
using System;
using SkiaSharp;

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
                JustifyContent = FlexJustify.SpaceBetween,
                AlignItems = FlexAlignItems.Center,
            };

            var templateText = new FlexLayout()
            {
                Direction = FlexDirection.Column,
            };

            var templateButtons = new FlexLayout()
            {
                JustifyContent = FlexJustify.End,
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

            var timeKey = new DateTime();
            timeLabel.BindingContextChanged += (sender, args) =>
            {
                if (timeLabel.BindingContext is ActivityModel act)
                    timeKey = act.ActivityTime;
            };

            var deleteButton = new Button()
            {
                Text = "D",

                HorizontalOptions = LayoutOptions.End,

                Style = Styles.MicroButtonStyle,
            };

            deleteButton.Clicked += (sender, args) =>
            {
                Calendar.Events.Remove(timeKey);
            };

            templateText.Children.Add(timeLabel);
            templateText.Children.Add(nameLabel);

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
            Calendar = new RangeSelectionCalendar()
            {
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

            var grid = new Grid();

            var layout = new StackLayout()
            {
                BackgroundColor = Color.Transparent,
            };

            var addActivityButton = new Button()
            {
                Margin = new Thickness(0, 0, 10, 10),

                Text = "+",
                FontSize = 30,

                MinimumHeightRequest = 70,
                MinimumWidthRequest = 100,

                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,

                Style = Styles.MiniButtonStyle,
            };

            addActivityButton.Pressed += AddActivity;

            layout.Children.Add(Calendar);
            layout.Children.Add(addActivityButton);

            grid.Children.Add(new GrainEffect()
            {
                BackgroundColor = SKColor.Parse("#F88E41"),
                GrainColor = SKColors.White,
                Density = 0.05,
            });
            grid.Children.Add(layout);

            Content = grid;
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