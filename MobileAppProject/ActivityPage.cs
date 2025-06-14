using System.Linq;
using Xamarin.Forms;
using System;
using System.Collections.ObjectModel;
using SkiaSharp;

namespace MobileAppProject
{
    public class ActivityPage : ContentPage
    {
        public TimePicker TimePicker;
        public Picker HobbyPicker;

        public ActivityPage()
        {
            TimePicker = new TimePicker()
            {
                Style = Styles.PickerStyle,
            };
            HobbyPicker = new Picker()
            {
                Title = "Choose a hobby...",
                TitleColor = Color.White,
                ItemsSource = HobbyListPage.HobbyList,
                ItemDisplayBinding = new Binding("Name"),
                SelectedItem = null,
                Style = Styles.PickerStyle,
            };

            var grid = new Grid();

            var layout = new StackLayout()
            {
                BackgroundColor = Color.Transparent,
            };

            var pickerLayout = new FlexLayout()
            {
                Direction = FlexDirection.Column,
            };

            var selectedDatesLabel = new Label()
            {
                Text = string.Format($"{SchedulePage.Calendar.SelectedDates.First():dd.MM.yyyy} - {SchedulePage.Calendar.SelectedDates.Last():dd.MM.yyyy}"),
                FontSize = 20,
                TextColor = Color.White,
            };

            var applyButton = new Button()
            {
                Text = "Apply",
                FontSize = 20,
                TextColor = Color.Black,

                Style = Styles.LargeButtonStyle,
            };

            applyButton.Clicked += (sender, args) => AddActivity();

            pickerLayout.Children.Add(TimePicker);
            pickerLayout.Children.Add(HobbyPicker);

            layout.Children.Add(selectedDatesLabel);
            layout.Children.Add(new ContentView()
            {
                Content = pickerLayout,
            });
            layout.Children.Add(applyButton);

            grid.Children.Add(new GrainEffect()
            {
                BackgroundColor = SKColor.Parse("#F88E41"),
                GrainColor = SKColors.White,
                Density = 0.05,
            });
            grid.Children.Add(layout);

            Content = grid;
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