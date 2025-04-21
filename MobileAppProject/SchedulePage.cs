using System.Globalization;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Controls;
using System.Collections.Generic;

namespace MobileAppProject
{
    public class SchedulePage : ContentPage
    {
        public RangeSelectionCalendar Calendar;
        public TimePicker TimePicker;
        public Picker HobbyPicker;

        public SchedulePage()
        {
            Calendar = new RangeSelectionCalendar()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Culture = new CultureInfo("ru-RU"),
            };

            TimePicker = new TimePicker()
            {
                IsVisible = false,
                IsEnabled = false,
            };

            HobbyPicker = new Picker();

            foreach (var item in HobbyModel.hobbyNames)
                HobbyPicker.Items.Add(item);

            var layout = new StackLayout();

            layout.Children.Add(Calendar);
            layout.Children.Add(TimePicker);
            layout.Children.Add(HobbyPicker);

            TimePicker.PropertyChanged += OnTimePickerPropertyChanged;

            Calendar.DayTappedCommand = new Command(
                () =>
                {
                    ToggleTimePicking(Calendar.SelectedDates.Count > 0);
                    //EventsInfoWhenDaysSelected(layout);
                });

            Content = layout;
        }

        private void OnTimePickerPropertyChanged(object sender,
            System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Time")
                AddNewEvents();
        }

        private void AddNewEvents()
        {
            foreach (var day in Calendar.SelectedDates)
            {
                var dateTime = new System.DateTime(day.Year, day.Month, day.Day, TimePicker.Time.Hours, TimePicker.Time.Minutes, 0);
                Calendar.Events.Add(dateTime, new List<HobbyModel>() { new HobbyModel("Piano", "") });
            }
        }

        private void EventsInfoWhenDaysSelected(StackLayout layout)
        {
            foreach (var item in Calendar.SelectedDayEvents)
            {
                
            }
        }

        private void ToggleTimePicking(bool daysSelected)
        {
            TimePicker.IsEnabled = daysSelected;
            TimePicker.IsVisible = daysSelected;
        }
    }
}