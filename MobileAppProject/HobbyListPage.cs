using SkiaSharp;
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
            var grid = new Grid();

            var layout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Margin = new Thickness(5, 5),
                Padding = new Thickness(5, 5),

                BackgroundColor = Color.Transparent,
            };

            var listView = new ListView()
            {
                ItemsSource = HobbyList,
                RowHeight = 150,
            };

            listView.ItemTemplate = new DataTemplate(() =>
            {
                var frame = new Frame()
                {
                    BorderColor = Color.Black,
                    CornerRadius = 15,
                };

                var itemLayout = new FlexLayout()
                {
                    BackgroundColor = Color.White,
                };

                var textLayout = new FlexLayout()
                {
                    Direction = FlexDirection.Column,
                };

                var buttonsLayout = new FlexLayout()
                {
                    JustifyContent = FlexJustify.End,
                };

                var nameLabel = new Label()
                {
                    FontSize = 24,
                };
                nameLabel.SetBinding(Label.TextProperty, "Name");

                var infoButton = new Button()
                {
                    Margin = new Thickness(5, 0),

                    Text = "I",

                    Style = Styles.MicroButtonStyle,
                };

                var deleteButton = new Button()
                {
                    Margin = new Thickness(5, 0),

                    Text = "D",

                    Style = Styles.MicroButtonStyle,
                };

                deleteButton.Clicked += 
                    (sender, args) => OnDeleteButtonPressed(listView);

                textLayout.Children.Add(nameLabel);

                buttonsLayout.Children.Add(infoButton);
                buttonsLayout.Children.Add(deleteButton);

                itemLayout.Children.Add(new ContentView() { Content = textLayout });
                itemLayout.Children.Add(new ContentView() { Content = buttonsLayout });

                frame.Content = itemLayout;

                return new ViewCell()
                {
                    View = frame,
                };
            });

            Button addHobbyButton = new Button()
            {
                Text = "Add new hobby",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,

                Style = Styles.LargeButtonStyle,
            };

            layout.Children.Add(addHobbyButton);
            layout.Children.Add(listView);

            addHobbyButton.Pressed +=
                (sender, eventArgs) => AddNewHobby();

            grid.Children.Add(new GrainEffect()
            {
                BackgroundColor = SKColor.Parse("#F88E41"),
                GrainColor = SKColors.White,
                Density = 0.05,
            });

            grid.Children.Add(layout);

            Content = grid;
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