using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MobileAppProject
{
    public class HobbyListPage : ContentPage
    {
        public HobbyList HobbyList = new HobbyList();

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

            Button addHobbyButton = new Button()
            {
                Text = "Add new hobby",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
            };

            layout.Children.Add(addHobbyButton);
            layout.Children.Add(HobbyList);

            addHobbyButton.Pressed +=
                (sender, eventArgs) => OnAddHobbyButtonPressed();

            Content = layout;
        }

        protected async void OnAddHobbyButtonPressed()
        {
            var cancel = "Later";
            var result = await DisplayActionSheet(
                "Choose the hobby you like", cancel, null, HobbyModel.hobbyNames);
            AddHobbyToLayout(!result.Equals(cancel) ? result : null);
            UpdateChildrenLayout();
        }

        private void AddHobbyToLayout(string name)
        {
            if (name != null)
            {
                var hobby = new HobbyModel(name, null);
                HobbyList.AddHobby(hobby);
            }
        }
    }

    public class HobbyList : StackLayout // возможно, ObservableCollection может лучше
    {
        public void AddHobby(HobbyModel hobby)
        {
            var stackLayout = new StackLayout();
            var deleteButton = new Button()
            {
                Text = "Delete",
            };
            var infoButton = new Button()
            {
                Text = "Info",
            };

            stackLayout.Children.Add(new Label()
            {
                Text = hobby.Name,
            });
            infoButton.Pressed += (sender, args) =>
            {
                infoButton.Text = hobby.Name;
            };
            deleteButton.Pressed += (sender, args) =>
            {
                OnDeleteButtonPressed(stackLayout);
            };
            stackLayout.Children.Add(infoButton);
            stackLayout.Children.Add(deleteButton);
            this.Children.Add(stackLayout);
        }

        public async void OnDeleteButtonPressed(StackLayout layout)
        {
            var page = this.Parent.Parent as ContentPage;
            var res = await page.DisplayAlert("Are you shure?", "Are you shure you want to remove that hobby?", "Yes", "No");
            if (res)
                RemoveHobby(layout);
        }

        public void RemoveHobby(StackLayout layout)
        {
            this.Children.Remove(layout);
        }
    }

    public class HobbyModel
    {
        public readonly string Name;
        public readonly string Description;

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