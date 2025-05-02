using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Controls;
using System;

namespace MobileAppProject
{
    public class ActivityPage : ContentPage
    {
        public TimePicker TimePicker;
        public Picker HobbyPicker;

        public ActivityPage(Calendar calendar)
        {
            TimePicker = new TimePicker();
            HobbyPicker = new Picker()
            {
                Title = "Choose a hobby...",
                TitleColor = Color.Gray,
                ItemsSource = HobbyModel.hobbyNames, // только хобби, которые выбраны
                SelectedItem = null,
            };

            var layout = new StackLayout();

            var label = new Label()
            {
                Text = string.Format("{0} - {1}", calendar.SelectedDates.First(), calendar.SelectedDates.Last()),
                FontSize = 20,
                TextColor = Color.Black,
            };

            var applyButton = new Button()
            {
                Text = "Apply",
                FontSize = 20,
                TextColor = Color.Black,
            };

            applyButton.Clicked += (sender, args) => ApplyChangesAndEndTask(calendar);

            layout.Children.Add(label);
            layout.Children.Add(TimePicker);
            layout.Children.Add(HobbyPicker);
            layout.Children.Add(applyButton);

            Content = layout;
        }

        private async void ApplyChangesAndEndTask(Calendar calendar)
        {
            foreach (var day in calendar.SelectedDates)
            {
                if (HobbyPicker.SelectedItem == null)
                    break;
                var dateTime = new DateTime(day.Year, day.Month, day.Day, TimePicker.Time.Hours, TimePicker.Time.Minutes, 0);
                calendar.Events.Add(day, new List<ActivityModel>()
                {
                    new ActivityModel(dateTime, new HobbyModel((string)HobbyPicker.SelectedItem, null)),
                });
            }

            await Navigation.PopAsync();
        }
    }
}