using System;
using System.Linq;
using System.Threading.Tasks;
using UnsplashApp.Models;
using UnsplashApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnsplashApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            BindingContext = this;
            InitializeComponent();
        }

        private UnsplashService _service = new UnsplashService();

        private BindableProperty IsSearchingProperty =
            BindableProperty.Create("IsSearching", typeof(bool), typeof(SearchPage), false);

        public bool IsSearching
        {
            get { return (bool)GetValue(IsSearchingProperty); }
            set { SetValue(IsSearchingProperty, value); }
        }

        private async void OnTextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (e.NewTextValue == null || e.NewTextValue.Length < UnsplashService.MinSearchLength)
                return;

            await GetPhotos(query: e.NewTextValue);
        }

        private async Task GetPhotos(string query)
        {
            try
            {
                IsSearching = true;

                var data = await _service.GetPhotos(query);
                listView.ItemsSource = data;
                listView.IsVisible = data.Any();
                notFound.IsVisible = !listView.IsVisible;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Could not retrieve the list of photos." + ex.Message, "OK");
            }
            finally
            {
                IsSearching = false;
            }
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var item = e.SelectedItem as Photo;

            listView.SelectedItem = null;

            await Navigation.PushAsync(new DetailsPage(item));
        }
    }
}