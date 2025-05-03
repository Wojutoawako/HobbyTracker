using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MobileAppProject
{
    public class HobbyListPage : ContentPage
    {
        public static ObservableCollection<HobbyModel> HobbyList 
            = new ObservableCollection<HobbyModel>();

        public HobbyListPage()
        {
            /* для пролистывания списка подходит ScrollView,
             * надо его подробнее изучить и применить здесь.*/
            var layout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Margin = new Thickness(5, 5),
                Padding = new Thickness(5, 5),
            };

            var listView = new ListView()
            {
                ItemsSource = HobbyList,
                RowHeight = 150,
            };

            listView.ItemTemplate = new DataTemplate(() =>
            {
                var itemLayout = new StackLayout();

                var nameLabel = new Label();
                nameLabel.SetBinding(Label.TextProperty, "Name");

                var infoButton = new Button()
                {
                    Text = "More info",
                };

                var deleteButton = new Button()
                {
                    Text = "Delete",
                };

                deleteButton.Clicked += 
                    (sender, args) => OnDeleteButtonPressed(listView);

                itemLayout.Children.Add(nameLabel);
                itemLayout.Children.Add(infoButton);
                itemLayout.Children.Add(deleteButton);

                return new ViewCell()
                {
                    View = itemLayout,
                };
            });

            Button addHobbyButton = new Button()
            {
                Text = "Add new hobby",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
            };

            layout.Children.Add(addHobbyButton);
            layout.Children.Add(listView);

            addHobbyButton.Pressed +=
                (sender, eventArgs) => AddNewHobby();

            Content = layout;
        }

        private async void OnDeleteButtonPressed(View view)
        {
            var res = await DisplayAlert("Are you shure?",
                "Are you shure you want to remove that hobby?", "Yes", "No");
            if (res)
            {
                var listView = view as ListView;
                var item = listView.SelectedItem as HobbyModel;

                HobbyList.Remove(item);
            }
        }

        protected async void AddNewHobby()
        {
            var cancel = "Later";
            var result = await DisplayActionSheet(
                "Choose the hobby you like", cancel, null, HobbyModel.hobbyNames);

            if (!result.Equals(cancel))
                HobbyList.Add(new HobbyModel(result, null));
        }
    }

    /// <summary>
    /// Класс модели хобби. Содержит отображаемые название, описание, а также краткую сводку статистики активности.
    /// </summary>
    public class HobbyModel
    {
        public string Name { get; }
        public string Description { get; private set; }

        public static readonly string[] hobbyNames =
        {
            "Reading", "Sports", "Music", "Gaming", "Gardening",
        };

        public HobbyModel(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}