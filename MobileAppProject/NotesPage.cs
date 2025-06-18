using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace MobileAppProject
{
    public class NotesPage : ContentPage
    {
        public static ObservableCollection<string> Notes = new ObservableCollection<string>();

        public Editor textEditor;

        public NotesPage()
        {
            var layout = new FlexLayout()
            {
                Direction = FlexDirection.Column,
            };

            textEditor = new Editor()
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                MinimumHeightRequest = 500,

                AutoSize = EditorAutoSizeOption.TextChanges,

                Placeholder = "Вы можете написать здесь всё, что думаете!",
                Keyboard = Keyboard.Text,
            };

            textEditor.TextChanged += TextChanged;

            var planButton = new Button()
            {
                Text = "Поставить цель",
            };

            planButton.Clicked += AddPlanTags;

            layout.Children.Add(textEditor);
            layout.Children.Add(planButton);

            Content = layout;
        }

        private void AddPlanTags(object sender, System.EventArgs e)
        {

        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            var editor = (Editor)sender;

            Notes.Clear();
            
            var tagOpen = @"<goal>";
            var tagClose = @"</goal>";

            var text = e.NewTextValue;

            var split = text.Split(new[] { tagOpen, tagClose }, System.StringSplitOptions.RemoveEmptyEntries);

            if (text.IndexOf(tagOpen) != 0)
                split = split.Skip(1).ToArray();
            if (text.LastIndexOf(tagClose) != text.Length - tagClose.Length)
                split = split.Take(split.Length - 1).ToArray();

            for (int i = 0; i < split.Length; i += 2)
            {
                if (!string.IsNullOrEmpty(split[i]))
                    Notes.Add(split[i]);
            }
        }
    }
}