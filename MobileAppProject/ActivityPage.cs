using System.Linq;
using Xamarin.Forms;
using System;
using System.Collections.ObjectModel;

namespace MobileAppProject
{
    public class ActivityPage : ContentPage
    {
        public TimePicker TimePicker;
        public Picker HobbyPicker;

        public ActivityPage()
        {
            TimePicker = new TimePicker();
            HobbyPicker = new Picker()
            {
                Title = "Choose a hobby...",
                TitleColor = Color.Gray,
                ItemsSource = HobbyListPage.HobbyList,
                ItemDisplayBinding = new Binding("Name"),
                SelectedItem = null,
            };

            var layout = new StackLayout();

            var label = new Label()
            {
                Text = string.Format("{0} - {1}", 
                    SchedulePage.Calendar.SelectedDates.First(),
                    SchedulePage.Calendar.SelectedDates.Last()),
                FontSize = 20,
                TextColor = Color.Black,
            };

            var applyButton = new Button()
            {
                Margin = new Thickness(10, 10),

                Text = "Apply",
                FontSize = 20,
                TextColor = Color.Black,

                BorderWidth = 1,
                BorderColor = Color.Black,
            };

            applyButton.Clicked += (sender, args) => AddActivity();

            layout.Children.Add(label);
            layout.Children.Add(TimePicker);
            layout.Children.Add(HobbyPicker);
            layout.Children.Add(applyButton);

            Content = layout;
        }

        private async void AddActivity()
        {
            foreach (var day in SchedulePage.Calendar.SelectedDates)
            {
                if (HobbyPicker.SelectedItem == null)
                    break;

                var timeKey = new DateTime(day.Year, day.Month, day.Day, TimePicker.Time.Hours, TimePicker.Time.Minutes, 0);

                if (!SchedulePage.Calendar.Events.ContainsKey(timeKey))
                {
                    SchedulePage.Calendar.Events.Add(timeKey, new ObservableCollection<ActivityModel>()
                        {
                            new ActivityModel(timeKey, (HobbyModel)HobbyPicker.SelectedItem),
                        });
                } else
                {
                    SchedulePage.Calendar.Events[timeKey] = new ObservableCollection<ActivityModel>()
                    {
                        new ActivityModel(timeKey, (HobbyModel)HobbyPicker.SelectedItem),
                    };
                }
            }

            await Navigation.PopAsync();
        }
    }

    public class ActivityModel
    {
        public DateTime ActivityTime { get; private set; }

        public HobbyModel Hobby { get; private set; }

        public ActivityModel(DateTime activityTime, HobbyModel hobby)
        {
            ActivityTime = activityTime;
            Hobby = hobby;
        }
    }
}