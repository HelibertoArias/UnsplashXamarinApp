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

       

        async void OnTextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            // Here we are checking for "null" because if the user presses Cancel, 
            // e.NewTextValue will be null. 
            if (e.NewTextValue == null || e.NewTextValue.Length < UnsplashService.MinSearchLength)
                return;

            // Note that I've movied the logic of setting the ListView's ItemSource
            // and handling errors to LoadMovies method. The reason for this is 
            // because try/catch blocks should not be in the middle of your methods. 
            // They make the code look ugly. 
            await GetPhotos(query: e.NewTextValue);
        }

        async Task GetPhotos(string query)
        {
            // Clean coding tip: try/catch should NOT be in the middle 
            // of a method. 

            // The responsibility of this method is quite clear. 
            // It calls the service and displays the result (either movies 
            // in the ListView or a Label). If anything unexpected happens,
            // it prevents the application from crashing and displays an alert. 
            // If we were to call this method from two different places, we 
            // wouldn't have to do error handling or setting IsSearching in
            // two different places. All these go hand-in-hand with setting
            // ListView's ItemSource. That's why I've encapsulated all that 
            // in one method. 
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

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var item = e.SelectedItem as Photo;

            listView.SelectedItem = null;

            await Navigation.PushAsync(new DetailsPage(item));
        }
    }
}