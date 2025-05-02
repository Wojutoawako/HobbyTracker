using System.Globalization;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Controls;
using System.Collections.Generic;
using System;

namespace MobileAppProject
{
    public class SchedulePage : ContentPage
    {
        public Xamarin.Plugin.Calendar.Controls.Calendar Calendar;

        public SchedulePage()
        {
            Calendar = new RangeSelectionCalendar()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,

                SelectedDates = new List<System.DateTime>(),

                Culture = new CultureInfo("ru-RU"),
            };

            Calendar.PropertyChanged += SelectedDatesChanged;

            var layout = new StackLayout();

            var addActivityButton = new Button()
            {
                Text = "Add",
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,
                MinimumHeightRequest = 70,
                MinimumWidthRequest = 100,
            };

            addActivityButton.Pressed += AddActivity;

            layout.Children.Add(Calendar);
            layout.Children.Add(addActivityButton);

            Content = layout;
        }

        private void SelectedDatesChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var dayCountPositive = Calendar.SelectedDates.Count > 0;
            if (e.PropertyName == "SelectedDates" && dayCountPositive)
            {
                Calendar.EventTemplate = new DataTemplate(() =>
                {
                    var templateLayout = new StackLayout();

                    var listView = new ListView()
                    {
                        ItemsSource = Calendar.SelectedDayEvents,
                        SelectionMode = ListViewSelectionMode.None,
                        IsRefreshing = false,
                    };

                    listView.ItemTemplate = new DataTemplate(() =>
                        {
                            var itemCell = new TextCell();

                            itemCell.SetBinding(TextCell.TextProperty, "HobbyName");

                            return itemCell;
                        });

                    templateLayout.Children.Add(listView);

                    return templateLayout;
                });
            }
            else if (e.PropertyName == "SelectedDates" && !dayCountPositive)
                Calendar.EventTemplate = Calendar.EmptyTemplate;
        }

        private async void AddActivity(object sender, System.EventArgs e)
        {
            if (Calendar.SelectedDates.Count > 0)
            {
                var activityPage = new ActivityPage(Calendar);
                await Navigation.PushAsync(activityPage);
            } else
            {
                return;
            }
        }
    }

    public class ActivityModel
    {
        public DateTime ActivityTime { get; private set; }

        public HobbyModel Hobby { get; private set; }

        public string HobbyName { get { return Hobby.Name; } }

        public ActivityModel(DateTime activityTime, HobbyModel hobby)
        {
            ActivityTime = activityTime;
            Hobby = hobby;
        }
    }
}