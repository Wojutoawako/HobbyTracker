using Android.Content;
using MobileAppProject.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Editor), typeof(EditorRendererExtention))]
namespace MobileAppProject.Droid.Renderer
{
    public class EditorRendererExtention : EditorRenderer
    {
        public EditorRendererExtention(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetHorizontallyScrolling(false);

                var button = new Android.Widget.Button(Context)
                {
                    Text = "Make plan",
                };

                button.Click += (sender, args) =>
                {
                    int selectionStart = Control.SelectionStart;
                    int selectionEnd = Control.SelectionEnd;

                    string selectedText = Control.Text.Substring(selectionStart, selectionEnd - selectionStart);
                    string tagText = $"<plan>{selectedText}</plan>";

                    Control.Text = Control.Text.Substring(0, selectionStart) +
                                   tagText +
                                   Control.Text.Substring(selectionEnd);

                    Control.SetSelection(selectionStart + tagText.Length);
                };
            }
        }
    }
}