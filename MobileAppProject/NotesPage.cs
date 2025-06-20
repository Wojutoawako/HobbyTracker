using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace MobileAppProject
{
    public class NotesPage : ContentPage
    {
        public static ObservableCollection<NoteItem> Notes = 
            new ObservableCollection<NoteItem>();

        public static ObservableCollection<string> Goals =
            new ObservableCollection<string>();

        public NotesPage()
        {
            BackgroundImageSource = "birds.png";

            var layout = new FlexLayout()
            {
                Padding = new Thickness(10, 10),

                Direction = FlexDirection.Column,

                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
            };

            var listView = new ListView()
            {
                ItemsSource = Notes,
                ItemTemplate = new DataTemplate(() =>
                {
                    var templateLayout = new FlexLayout()
                    {
                        AlignItems = FlexAlignItems.Center,

                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.Fill,
                    };

                    var textLabel = new Label()
                    {
                        FontSize = 24,
                        TextColor = Color.Black,
                        VerticalTextAlignment = TextAlignment.Center,
                    };
                    textLabel.SetBinding(Label.TextProperty, "Preview");
                    FlexLayout.SetGrow(textLabel, 2);

                    NoteItem note = null;
                    textLabel.BindingContextChanged += (sender, args) =>
                    {
                        if (textLabel.BindingContext is NoteItem n)
                            note = n;
                    };

                    var deleteButton = new Button()
                    {
                        Text = "D",

                        Command = new Command(async () =>
                        {
                            var res = await DisplayAlert("Вы уверены?", "Эта заметка будет удалена", "Продолжить", "Отменить");
                            if (res)
                                Notes.Remove(note);
                        }),

                        Style = Styles.MicroButtonStyle,
                    };

                    templateLayout.Children.Add(textLabel);
                    templateLayout.Children.Add(deleteButton);

                    var frame = new Frame()
                    {
                        Margin = new Thickness(0, 0, 0, 10),

                        CornerRadius = 15,
                        BorderColor = Color.Black,

                        GestureRecognizers =
                        {
                            new TapGestureRecognizer()
                            {
                                Command = new Command(() =>
                                {
                                    ChangeNote(note);
                                }),
                            },
                        },
                    };

                    frame.Content = templateLayout;

                    return new ViewCell()
                    {
                        View = frame,
                    };
                }),

                SelectionMode = ListViewSelectionMode.None,
                RowHeight = 90,

                VerticalOptions = LayoutOptions.Fill,
            };
            FlexLayout.SetGrow(listView, 2);

            var addNoteButton = new Button()
            {
                Text = "+",
                FontSize = 30,

                Style = Styles.MiniButtonStyle,
            };
            FlexLayout.SetAlignSelf(addNoteButton, FlexAlignSelf.End);

            addNoteButton.Clicked += AddNote;

            layout.Children.Add(listView);
            layout.Children.Add(addNoteButton);

            Content = layout;
        }

        private async void AddNote(object sender, System.EventArgs e)
        {
            var page = new SingleNotePage();
            await Navigation.PushAsync(page);
        }

        private void ChangeNote(NoteItem node)
        {
            var page = new SingleNotePage(node);
            Navigation.PushAsync(page);
        }
    }

    public class NoteItem
    {
        public int Id { get; private set; } 
        public string Text { get; private set; }

        public string Preview { get { return string.Join(" ", Text.Split(' ').Take(5)); } }

        private static int LastId = 0;

        public NoteItem(string text)
        {
            Text = text;
            Id = LastId++;
        }

        public NoteItem(int id, string text)
        {
            Id = id;
            Text = text;
        }
    }
}