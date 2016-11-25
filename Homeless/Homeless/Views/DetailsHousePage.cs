using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Homeless.Extras;
using Homeless.Models;
using Homeless.DependencyServices;
namespace Homeless.Views
{
    public class DetailsHousePage:ContentPage
    {
        private databaseManager db;
        private House house;

        private Label lblShortDescription;
        private Label lblDescription;
        private Label lblPhone;
        private Label lblPrice;
        //private Label lblIsVisited;
        //private Label lblIsRented;
        //private Label lblIsCalled;
        //private Label lblNameContact;

        private Entry ShortDescription;
        private Entry Description;
        private Entry PhoneNumber;
        private Entry Price;
        //private Entry NameContact;

        private Button btnEdit;
        private Button btnDelete;
        private Button btnSave;
        private Button btnCancel;
        private Button bCallOwner;

        private StackLayout layout;

        public DetailsHousePage(int HouseId)
        {
           
            InitializeComponent();
            Bind(HouseId);
        }

        private void InitializeComponent()
        {
            lblShortDescription =
                new Label
                {
                    Text = "Descripcion Corta",
                    FontSize = 15,
                    HorizontalOptions = LayoutOptions.Center
                };

            ShortDescription = new Entry
            {
                IsEnabled = false,
                Keyboard =  Keyboard.Text
            };

            lblDescription =
                new Label
                {
                    Text = "Descripcion de la propiedad",
                    FontSize = 15,
                    HorizontalOptions = LayoutOptions.Center
                };

            Description = new Entry
            {
                IsEnabled = false,
                Keyboard = Keyboard.Text
            };

            lblPhone =
                new Label
                {
                    Text = "Telefono de contacto",
                    FontSize = 15,
                    HorizontalOptions = LayoutOptions.Center
                };

            PhoneNumber = new Entry
            {
                IsEnabled = false,
                Keyboard = Keyboard.Telephone
            };
            bCallOwner =
                new Button
                {

                    Text = "Llamar al propietario",
                    BorderColor = Color.Black,
                    BorderWidth = 1,
                    HorizontalOptions = LayoutOptions.Center,
                    IsVisible = true
                };

            lblPrice =
                new Label
                {
                    Text = "Precio ",
                    FontSize = 15,
                    HorizontalOptions =  LayoutOptions.Center
                };

            Price = new Entry
            {
                IsEnabled = false,
                Keyboard = Keyboard.Numeric
            };

            btnEdit = new Button
            {
                Text = "Edit House"

            };

            btnDelete = new Button
            {
                Text = "Delete house"
            };

            btnSave = new Button
            {
                 Text = "Save Changes",
                 IsVisible = false
            };

            btnCancel = new Button
            {
                Text = "Cancel",
                IsVisible = false
            };

            btnDelete.Clicked += BtnDelete_Clicked;
            btnEdit.Clicked += BtnEdit_Clicked;
            btnSave.Clicked += BtnSave_Clicked;
            btnCancel.Clicked += BtnCancel_Clicked;
            bCallOwner.Clicked += BCallOwner_Clicked;

            layout = new StackLayout();
            layout.Children.Add(lblShortDescription);
            layout.Children.Add(ShortDescription);
            layout.Children.Add(lblDescription);
            layout.Children.Add(Description);
            layout.Children.Add(lblPhone);
            layout.Children.Add(PhoneNumber);
            layout.Children.Add(bCallOwner);
            layout.Children.Add(lblPrice);
            layout.Children.Add(Price);
            layout.Children.Add(btnEdit);
            layout.Children.Add(btnDelete);
            layout.Children.Add(btnSave);
            layout.Children.Add(btnCancel);

            Content = layout;


        }

        private void BCallOwner_Clicked(object sender, EventArgs e)
        {
            try
            {
               if(!String.IsNullOrEmpty( PhoneNumber.Text))
                {
                    DependencyService.Get<IPhoneCall>().MakeQuickCall(PhoneNumber.Text);
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        private void BtnCancel_Clicked(object sender, EventArgs e)
        {
            EnableEdit(false);
        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            db = new databaseManager();

            house.Phone = PhoneNumber.Text;
            house.Price = Decimal.Parse( Price.Text);
            

            if( db.updateItem(house)==1)
            {
                DisplayAlert("Guardar", "Se guardaron los datos correctamente", "OK");
                EnableEdit(false);

            }
            else
            {
                DisplayAlert("Error", "Error al guardar los datos correctamente", "OK");
            }
        }

        private void BtnEdit_Clicked(object sender, EventArgs e)
        {
            EnableEdit(true);
        }

        private void BtnDelete_Clicked(object sender, EventArgs e)
        {
            db = new databaseManager();
            if(db.deleteItem(house.IdHouse)==1)
            {
                DisplayAlert("Deleted", "Property has been deleted", "OK");
                Navigation.PopAsync(true);
            }
            else
            {
                DisplayAlert("Error", "Error trying to delete", "OK");
            }
        }

        private void Bind(int houseId)
        {
            db = new databaseManager();
            house = db.selectHouseById(houseId);

            ShortDescription.Text = house.ShortDescription;

            // Revisar para checar la expresion regular a utilizar si se reconocer el numero
            Description.Text = house.Comments;
            PhoneNumber.Text = house.Phone;
            Price.Text = house.Price.ToString();
        }

        private void EnableEdit (bool isEditing)
        {
            ShortDescription.IsEnabled = isEditing;
            Description.IsEnabled = isEditing;
            PhoneNumber.IsEnabled = isEditing;
            Price.IsEnabled = isEditing;

            btnDelete.IsEnabled = !isEditing;
            btnDelete.IsVisible = !isEditing;

            btnCancel.IsEnabled = isEditing;
            btnCancel.IsVisible = isEditing;

            btnSave.IsEnabled = isEditing;
            btnSave.IsVisible = isEditing;

            btnEdit.IsVisible = !isEditing;
            btnEdit.IsEnabled = !isEditing;

            bCallOwner.IsVisible = !isEditing;
            bCallOwner.IsEnabled = !isEditing;
        }
    }
}
