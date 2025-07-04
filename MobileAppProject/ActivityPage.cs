﻿using System.Linq;
using Xamarin.Forms;
using System;
using System.Collections.ObjectModel;

namespace MobileAppProject
{
    public class ActivityPage : ContentPage
    {
        public TimePicker TimePicker;
        public Picker HobbyPicker;
        public Picker GoalPicker;

        private bool _isChanging;
        private ActivityModel _activity;

        public ActivityPage()
        {
            _isChanging = false;

            BackgroundImageSource = "sport.png";

            var mainPage = App.Current.MainPage as NavigationPage;
            mainPage.BarBackgroundColor = Color.Black; 
            mainPage.BarTextColor = Color.White;

            TimePicker = new TimePicker()
            {
                Margin = new Thickness(0, 0, 0, 10),

                Style = Styles.PickerStyle,
            };
            HobbyPicker = new Picker()
            {
                Margin = new Thickness(0, 0, 0, 10),

                Title = "Выберите хобби...",
                ItemsSource = HobbyListPage.HobbyList,
                ItemDisplayBinding = new Binding("Name"),
                SelectedItem = null,
                Style = Styles.PickerStyle,
            };
            GoalPicker = new Picker()
            {
                ItemsSource = NotesPage.Goals,
                Title = "Добавить цели",
                Style = Styles.PickerStyle,
            };

            var layout = new FlexLayout()
            {
                AlignItems = FlexAlignItems.Center,
                Direction = FlexDirection.Column,

                VerticalOptions = LayoutOptions.StartAndExpand,

                BackgroundColor = Color.Transparent,
            };

            var pickerLayout = new StackLayout();

            var selectedDatesLabel = new Label()
            {
                Margin = new Thickness(0, 10),

                Text = string.Format($"Выбранные даты:\n{SchedulePage.Calendar.SelectedDates.First():dd.MM.yyyy} - {SchedulePage.Calendar.SelectedDates.Last():dd.MM.yyyy}"),
                FontSize = 24,
                TextColor = Color.White,
            };

            var applyButton = new Button()
            {
                Text = "Применить",
                FontSize = 20,
                TextColor = Color.Black,

                VerticalOptions = LayoutOptions.End,

                Command = new Command(() =>
                {
                    if (!_isChanging)
                        AddActivity();
                    else
                        EditActivity();
                }),

                Style = Styles.LargeButtonStyle,
            };

            pickerLayout.Children.Add(TimePicker);
            pickerLayout.Children.Add(HobbyPicker);
            pickerLayout.Children.Add(GoalPicker);

            layout.Children.Add(selectedDatesLabel);
            layout.Children.Add(pickerLayout);
            layout.Children.Add(applyButton);

            Content = layout;
        }

        public ActivityPage(ActivityModel activityModel) : this()
        {
            _isChanging = true;
            _activity = activityModel;

            TimePicker.Time = activityModel.ActivityTime.TimeOfDay;
            HobbyPicker.SelectedItem = activityModel.Hobby;
        }

        private async void EditActivity()
        {
            var collection = SchedulePage.Calendar.Events[_activity.ActivityTime] as ObservableCollection<ActivityModel>;

            var date = _activity.ActivityTime.Date;

            var timeKey = new DateTime(date.Year, date.Month, date.Day, TimePicker.Time.Hours, TimePicker.Time.Minutes, 0);
            collection[collection.IndexOf(_activity)] = new ActivityModel(timeKey, HobbyPicker.SelectedItem as HobbyModel);

            await Navigation.PopAsync();
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
                    var collection = SchedulePage.Calendar.Events[timeKey] as ObservableCollection<ActivityModel>;
                    collection.Add(new ActivityModel(timeKey, (HobbyModel)HobbyPicker.SelectedItem));
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