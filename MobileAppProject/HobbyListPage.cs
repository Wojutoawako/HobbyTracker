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
            BackgroundImageSource = "nature.png";

            var layout = new StackLayout()
            {
                Padding = new Thickness(10, 10),

                BackgroundColor = Color.Transparent,
            };

            var listView = new ListView()
            {
                ItemsSource = HobbyList,
                RowHeight = 90,
            };

            listView.ItemTemplate = new DataTemplate(() =>
            {
                var frame = new Frame()
                {
                    Margin = new Thickness(0, 0, 0, 10),

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

                    Text = "i",

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
                Margin = new Thickness(0, 0, 0, 10),

                Text = "Выбрать хобби",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,

                Style = Styles.LargeButtonStyle,
            };

            layout.Children.Add(addHobbyButton);
            layout.Children.Add(listView);

            addHobbyButton.Pressed +=
                (sender, eventArgs) => AddNewHobby();

            Content = layout;
        }
        private async void OnDeleteButtonPressed(View view)
        {
            var res = await DisplayAlert("Вы уверены?",
                "Вы действительно хотите удалить это хобби?", "Да", "Нет");
            if (res)
            {
                var listView = view as ListView;
                var item = listView.SelectedItem as HobbyModel;

                HobbyList.Remove(item);
            }
        }

        protected async void AddNewHobby()
        {
            var cancel = "Позже";
            var result = await DisplayActionSheet(
                "Выберите хобби, которое вас заинтересовало", cancel, null, HobbyModel.hobbyNames);

            if (!result.Equals(cancel))
                HobbyList.Add(new HobbyModel(result));
        }
    }

    /// <summary>
    /// Класс модели хобби. Содержит отображаемые название, описание, а также краткую сводку статистики активности.
    /// </summary>
    public class HobbyModel
    {
        public string Name { get; }
        public string Information { get; private set; }

        public static readonly string[] hobbyNames =
        {
            "Чтение", "Спорт", "Музыка", "Видеоигры", "Садоводство",
        };

        public HobbyModel(string name)
        {
            Name = name;
        }
    }
}