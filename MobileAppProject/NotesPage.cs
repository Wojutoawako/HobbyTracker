using Xamarin.Forms;

namespace MobileAppProject
{
    public class NotesPage : ContentPage
    {
        public Label label = new Label()
        {
            Text = "No plans",
        };
        public NotesPage()
        {
            var layout = new FlexLayout()
            {
                Direction = FlexDirection.Column,
            };

            var textEditor = new Editor()
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                MinimumHeightRequest = 500,

                AutoSize = EditorAutoSizeOption.TextChanges,

                Placeholder = "Feel free to write your thoughts here!",
                Keyboard = Keyboard.Text,
            };

            textEditor.TextChanged += TextChanged;

            layout.Children.Add(textEditor);
            layout.Children.Add(label);

            Content = layout;
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Contains(@"<plan>"))
                label.Text = "Plan";
        }
    }
}