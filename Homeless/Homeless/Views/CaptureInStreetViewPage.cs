using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Homeless.DependencyServices;
using Homeless.Models;
using Homeless.Extras;

using System.IO;

namespace Homeless.Views
{
    public class CaptureInStreetView: ContentPage
    {
        private Image img;
        private Button bCapture;
        private Button bGetLocatiopn;
        private Button bSave;
        private Button bCancel;
        private Button bCallOwner;
        private Editor ShortDescription;

        private databaseManager db = new databaseManager();
        private House newHouse = new House();

        public StackLayout imageContainer;
        public StackLayout verticalStackLayout;
        public Label lblCaptureHouseInStreet;
        public Label lblShortDescription; 

        public String imagePath = String.Empty;




        public CaptureInStreetView()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            lblCaptureHouseInStreet =
               new Label
               {
                   Text = "Capturar anuncio de la casa",
                   FontSize = 20,
                   HorizontalOptions = LayoutOptions.Center
               };

            bCapture =
                new Button
                {
                    Text = "Capturar Foto",
                    BorderColor = Color.Black,
                    BorderWidth = 1,
                    HorizontalOptions = LayoutOptions.Center
                };

            bGetLocatiopn =
                new Button
                {
                    Text = "Obtener Localizacion",
                    BorderColor = Color.Black,
                    BorderWidth = 1,
                    HorizontalOptions = LayoutOptions.Center,
                    IsVisible = false

                };

            bSave =
              new Button
              {
                  Text = "Guardar Casa",
                  BorderColor = Color.Black,
                  BorderWidth = 1,
                  HorizontalOptions = LayoutOptions.Center,
                  IsVisible = false

              };

            bCancel =
            new Button
            {
                Text = "Cancelar",
                BorderColor = Color.Black,
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                IsVisible = false

            };

           

            lblShortDescription =
              new Label
              {
                  Text = "Capturar descripcion corta para referencia de la casa o el sistema asignara uno",
                  FontSize = 20,
                  HorizontalOptions = LayoutOptions.Center,
                  VerticalOptions = LayoutOptions.Center
              };

            ShortDescription =
                new Editor
                {
                    BackgroundColor = Device.OnPlatform(Color.FromHex("#A4EAFF"), Color.FromHex("#2c3e50"), Color.FromHex("#2c3e50")),
                    IsVisible = true
                };

            imageContainer =
               new StackLayout
               {
                   Orientation = StackOrientation.Vertical,
                   HorizontalOptions = LayoutOptions.Center,
                   WidthRequest = 200
               };

            img =
                new Image
                {
                    Aspect = Aspect.AspectFit,
                    WidthRequest = 150,
                    HeightRequest = 150
                };

            Title = "Captura rapida";

            verticalStackLayout =
              new StackLayout
              {
                  Orientation = StackOrientation.Vertical,
                  VerticalOptions = LayoutOptions.CenterAndExpand,
                  HorizontalOptions = LayoutOptions.FillAndExpand
              };

            imageContainer.Children.Add(img);

            verticalStackLayout.Children.Add(lblCaptureHouseInStreet);
            verticalStackLayout.Children.Add(imageContainer);
            verticalStackLayout.Children.Add(bCapture);
            verticalStackLayout.Children.Add(bGetLocatiopn);
            verticalStackLayout.Children.Add(bSave);
            verticalStackLayout.Children.Add(ShortDescription);
            verticalStackLayout.Children.Add(lblShortDescription);

            bCapture.Clicked += BCapture_Clicked;
            bSave.Clicked += BSave_Clicked;
            
            Content = verticalStackLayout;
        }

        private void BSave_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty( ShortDescription.Text))
            {
                newHouse.ShortDescription = String.Format("Propiedad vista el {0} de {1} a las {2} hrs", 
                    DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Hour); 
            }
            else
            {
                newHouse.ShortDescription = ShortDescription.Text;
            }

            Dictionary<string, string> location = DependencyService.Get<IGeoLocation>().getLocation();
            newHouse.Lat = location["Lat"];
            newHouse.Lon = location["Lon"];
    
            int result = db.insertItem(newHouse);
            if (result == 1)
            {
                DisplayAlert("Guardar registro", "Registro guardado exitosamente", "OK");
            }
            db.closeConnection();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DependencyService.Get<IGeoLocation>().activarGPS();
            MessagingCenter.Subscribe<CaptureInStreetView, string>(this, "foto capturada", (sender, args) =>
            {
                ShowPhoto(args);
            });

            MessagingCenter.Subscribe<CaptureInStreetView, string>(this, "texto foto", (sender, args) =>
            {
                // DisplayAlert("Mensaje de la foto", args, "OK");
                // UpdateShortDescription(args);
                newHouse.Comments = args;

            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<CaptureInStreetView, string>(this, "foto capturada");
            MessagingCenter.Unsubscribe<CaptureInStreetView, string>(this, "texto foto");
            DependencyService.Get<IGeoLocation>().apagarGPS();
        }
        private  async void BCapture_Clicked(object sender, EventArgs e)
        {
            imagePath = await DependencyService.Get<ICamera>().takePhoto();
            if (imagePath == String.Empty || imagePath == "Cancel" || imagePath == null)
            {
                bCapture.IsEnabled = false;
            }
            else
            {
                img.Source = ImageSource.FromFile(imagePath);
                bCapture.IsEnabled = true;
            }

        }

        private void ShowPhoto(String imagePath)
        { 
            img.Source =  ImageSource.FromFile(imagePath);

            newHouse.Images = imagePath;
            bSave.IsVisible = true;
        }

        private void UpdateShortDescription (string message)
        {
            ShortDescription.Text = message;
        }


    }
}
