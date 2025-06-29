using Newtonsoft.Json;
using PCLStorage;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MobileAppProject
{
    public class HobbyListPage : ContentPageExtension
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
                ItemTemplate = new DataTemplate(() =>
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

                    HobbyModel hobby = null;
                    nameLabel.BindingContextChanged += (sender, args) =>
                    {
                        if (nameLabel.BindingContext is HobbyModel h)
                            hobby = h;
                    };

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

                        Command = new Command(async () =>
                        {
                            var res = await DisplayAlert("Вы уверены?", "Вы действительно хотите удалить это хобби?", "Да", "Нет");
                            if (res)
                            {
                                HobbyList.RemoveAt(HobbyList.IndexOf(hobby));
                            }
                        }),

                        Style = Styles.MicroButtonStyle,
                    };

                    textLayout.Children.Add(nameLabel);

                    //buttonsLayout.Children.Add(infoButton);
                    buttonsLayout.Children.Add(deleteButton);

                    itemLayout.Children.Add(new ContentView() { Content = textLayout });
                    itemLayout.Children.Add(new ContentView() { Content = buttonsLayout });

                    frame.Content = itemLayout;

                    return new ViewCell()
                    {
                        View = frame,
                    };
                }),

                SelectionMode = ListViewSelectionMode.None,

                RowHeight = 90,
            };

            var addHobbyButton = new Button()
            {
                Margin = new Thickness(0, 0, 0, 10),

                Text = "Выбрать хобби",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,

                Command = new Command(() =>
                {
                    AddNewHobby();
                }),

                Style = Styles.LargeButtonStyle,
            };

            layout.Children.Add(addHobbyButton);
            layout.Children.Add(listView);

            Content = layout;
        }

        protected async void AddNewHobby()
        {
            var cancel = "Позже";
            var result = await DisplayActionSheet(
                "Выберите хобби, которое вас заинтересовало", cancel, null, HobbyModel.hobbyNames);

            if (!result.Equals(cancel))
                HobbyList.Add(new HobbyModel(result));
        }

        public override async void SavePageData()
        {
            var saveFile = await App.SaveFolder.CreateFileAsync("HobbyListPageSave", CreationCollisionOption.ReplaceExisting);

            await saveFile.WriteAllTextAsync(JsonConvert.SerializeObject(HobbyListPage.HobbyList));
        }

        public override async void LoadPageData() 
        {
            if (App.SaveFolder.CheckExistsAsync("HobbyListPageSave").Result == ExistenceCheckResult.FileExists)
            {
                var file = await App.SaveFolder.GetFileAsync("HobbyListPageSave");

                var saveData = JsonConvert.DeserializeObject<ObservableCollection<HobbyModel>>(file.ReadAllTextAsync().Result);
                foreach (var item in saveData)
                {
                    HobbyList.Add(item);
                }
            }
        }
    }

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