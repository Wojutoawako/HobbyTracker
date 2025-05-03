using System.Globalization;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Controls;
using System.Collections.Generic;

namespace MobileAppProject
{
    public class SchedulePage : ContentPage
    {
        public static Xamarin.Plugin.Calendar.Controls.Calendar Calendar;

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
                    var listView = new ListView()
                    {
                        ItemsSource = Calendar.SelectedDayEvents,
                    };

                    listView.ItemTemplate = new DataTemplate(() =>
                        {
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

                            return new ViewCell()
                            {
                                View = new StackLayout()
                                {
                                    Children = { timeLabel, nameLabel },
                                }
                            };
                        });

                    return listView;
                });
            }
            else if (e.PropertyName == "SelectedDates" && !dayCountPositive)
                Calendar.EventTemplate = Calendar.EmptyTemplate;
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