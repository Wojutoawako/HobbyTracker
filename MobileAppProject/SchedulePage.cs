using System.Globalization;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Controls;
using System.Collections.Generic;
using Xamarin.Essentials;
using System;

namespace MobileAppProject
{
    public class SchedulePage : ContentPage
    {
        public static Xamarin.Plugin.Calendar.Controls.Calendar Calendar;

        private static DataTemplate EventDataTemplate = new DataTemplate(() =>
        {
            var frame = new Frame()
            {
                Margin = new Thickness(0, 0, 0, 10),

                BorderColor = Color.Black,
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
                Text = "Delete",

                HorizontalOptions = LayoutOptions.End,
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
            };

            var layout = new StackLayout();

            var addActivityButton = new Button()
            {
                Text = "Add",
                MinimumHeightRequest = 70,
                MinimumWidthRequest = 100,

                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,
            };

            addActivityButton.Pressed += AddActivity;

            layout.Children.Add(Calendar);
            layout.Children.Add(addActivityButton);

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