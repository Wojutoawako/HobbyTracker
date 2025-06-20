using System.Linq;
using Xamarin.Forms;

namespace MobileAppProject
{
    public class SingleNotePage : ContentPage
    {
        public Editor TextEditor;

        private bool _changeNote;
        private NoteItem _note;

        public SingleNotePage()
        {
            _changeNote = false;

            BackgroundColor = Color.White;

            var mainPage = App.Current.MainPage as NavigationPage;
            mainPage.BarBackgroundColor = Color.White;
            mainPage.BarTextColor = Color.Black;

            var layout = new FlexLayout()
            {
                Padding = new Thickness(10),

                Direction = FlexDirection.Column,
                AlignItems = FlexAlignItems.Center,
            };

            TextEditor = new Editor()
            {
                Margin = new Thickness(0, 0, 0, 10),

                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                MinimumHeightRequest = 500,

                AutoSize = EditorAutoSizeOption.TextChanges,

                Placeholder = "Вы можете написать здесь всё, что думаете!",
                Keyboard = Keyboard.Text,
            };

            TextEditor.TextChanged += TextChanged;

            layout.Children.Add(TextEditor);

            Content = layout;
        }

        public SingleNotePage(NoteItem note) : this()
        {
            _changeNote = true;
            _note = note;
            TextEditor.Text = note.Text;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            SaveNote(TextEditor.Text);
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = e.NewTextValue;

            TakeGoalsFromText(text);
        }

        private void SaveNote(string text)
        {
            var isNull = string.IsNullOrEmpty(text.Trim());
            if (!isNull && !_changeNote)
            {
                NotesPage.Notes.Add(new NoteItem(text));
            } else if (!isNull && _changeNote)
            {
                NotesPage.Notes[NotesPage.Notes.IndexOf(_note)] = new NoteItem(_note.Id, text);
            } else if (isNull && _changeNote)
            {
                NotesPage.Notes.RemoveAt(NotesPage.Notes.IndexOf(_note));
            }
        }

        private static void TakeGoalsFromText(string text)
        {
            NotesPage.Goals.Clear();

            var tagOpen = @"<goal>";
            var tagClose = @"</goal>";

            var split = text.Split(new[] { tagOpen, tagClose }, System.StringSplitOptions.RemoveEmptyEntries);

            if (text.IndexOf(tagOpen) != 0)
                split = split.Skip(1).ToArray();
            if (text.LastIndexOf(tagClose) != text.Length - tagClose.Length)
                split = split.Take(split.Length - 1).ToArray();

            for (int i = 0; i < split.Length; i += 2)
            {
                if (!string.IsNullOrEmpty(split[i]))
                    NotesPage.Goals.Add(split[i]);
            }
        }
    }
}