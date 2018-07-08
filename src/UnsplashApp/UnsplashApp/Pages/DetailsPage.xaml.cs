using System;
using UnsplashApp.Models;
using UnsplashApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnsplashApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        private UnsplashService _photoService = new UnsplashService();

        private Photo _photo;

        public DetailsPage(Photo item)
        {
            _photo = item ?? throw new ArgumentNullException(nameof(item));

            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            BindingContext = await _photoService.GetPhotoById(_photo.Id);
            base.OnAppearing();
        }
    }
}