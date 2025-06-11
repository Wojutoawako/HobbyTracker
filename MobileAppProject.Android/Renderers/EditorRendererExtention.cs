using Android.Content;
using Android.Widget;
using MobileAppProject.Droid.Renderers;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Editor), typeof(EditorRendererExtention))]
namespace MobileAppProject.Droid.Renderers
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
                var ctrl = Control;
                ctrl.SetHorizontallyScrolling(false);

                var button = new Android.Widget.Button(Context)
                {
                    Text = "Goal",
                    LayoutParameters = new LinearLayout.LayoutParams(
                        LayoutParams.WrapContent,
                        LayoutParams.WrapContent)
                };

                button.Click += (sender, args) =>
                {
                    var position = ctrl.SelectionStart;

                    var builder = new StringBuilder();
                    builder.Append(ctrl.Text.Substring(0, position));
                    builder.Append("<goal></goal>");
                    builder.Append(ctrl.Text.Substring(position + 1));

                    ctrl.Text = builder.ToString();
                    ctrl.SetSelection(position + 6);
                };
            }
        }
    }
}