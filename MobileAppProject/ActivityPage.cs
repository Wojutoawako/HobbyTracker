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
                Text = "Apply",
                FontSize = 20,
                TextColor = Color.Black,
            };

            applyButton.Clicked += (sender, args) => ApplyChangesAndEndTask();

            layout.Children.Add(label);
            layout.Children.Add(TimePicker);
            layout.Children.Add(HobbyPicker);
            layout.Children.Add(applyButton);

            Content = layout;
        }

        private async void ApplyChangesAndEndTask()
        {
            foreach (var day in SchedulePage.Calendar.SelectedDates)
            {
                if (HobbyPicker.SelectedItem == null)
                    break;
                var timeKey = new DateTime(day.Year, day.Month, day.Day, TimePicker.Time.Hours, TimePicker.Time.Minutes, 0);

                if (!SchedulePage.Calendar.Events.ContainsKey(timeKey))
                {
                    SchedulePage.Calendar.Events.Add(day, new ObservableCollection<ActivityModel>()
                        {
                            new ActivityModel(timeKey, (HobbyModel)HobbyPicker.SelectedItem),
                        });
                } else // под вопросом
                {
                    var item = SchedulePage.Calendar.Events[timeKey] as ObservableCollection<ActivityModel>;
                    item.Add(new ActivityModel(timeKey, (HobbyModel)HobbyPicker.SelectedItem));
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