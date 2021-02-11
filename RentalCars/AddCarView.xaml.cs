using RentalCars.Models;
using RentalCars.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RentalCars
{
    /// <summary>
    /// Interaction logic for AddCarView.xaml
    /// </summary>
    public partial class AddCarView : Window
    {

        private readonly CarRepository repository = null;


        public AddCarView()
        {
            InitializeComponent();
            repository = new CarRepository();
        }

        private void addCarButton_Click(object sender, RoutedEventArgs e)
        {
            var validationMessage = ValidateCar();
            if (string.IsNullOrEmpty(validationMessage) == false)
            {
                MessageBox.Show(validationMessage);
            }
            else
            {
                SaveCarToDataBase();
            }

        }

        private void SaveCarToDataBase()
        {
            try
            {
                Car car = new Car()
                {
                    Marka = TextBoxBrand.Text,
                    Model = TextBoxModel.Text,
                    RegistrationNumber = TextBoxRegistrationNumber.Text,
                    Cost = Decimal.Parse(TextBoxCost.Text)
                };

                CarService carService = new CarService()
                {
                    From = DateTime.Parse(TextBoxFrom.Text),
                    To = DateTime.Parse(TextBoxFrom.Text).AddYears(1)
                };

                repository.AddCar(car, carService);
                ResetFields();
                MainWindow.carGridData.ItemsSource = repository.GetAll();
                MessageBox.Show("Nowy samochód został dodany");

            }
            catch (Exception)
            {
                MessageBox.Show("Nie udało się dodać samochodu", "", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }


        private string ValidateCar()
        {
            string output = "";


            if (string.IsNullOrEmpty(TextBoxBrand.Text))
            {
                output = "Wprowadz Marke!";
            }
            else if (TextBoxBrand.Text.Length > 20)
            {
                output = "Przekroczono liczbe znaków!";
            }
            else if (string.IsNullOrEmpty(TextBoxModel.Text))
            {
                output = "Wprowadz Model!";
            }
            else if (TextBoxModel.Text.Length > 20)
            {
                output = "Przekroczono liczbe znaków!";
            }
            else if (string.IsNullOrEmpty(TextBoxRegistrationNumber.Text))
            {
                output = "Wprowadż Rejestracje!";
            }
            else if (TextBoxRegistrationNumber.Text.Length > 8)
            {
                output = "Przekroczono liczbe znaków!";
            }
            else if (string.IsNullOrEmpty(TextBoxFrom.Text))
            {
                output = "Wprowadż date!";
            }
            else if (string.IsNullOrEmpty(TextBoxCost.Text))
            {
                output = "Wprowadż kwote!";
            }
            return output;
        }

        private void ResetFields()
        {
            TextBoxBrand.Text = "";
            TextBoxModel.Text = "";
            TextBoxRegistrationNumber.Text = "";
            TextBoxFrom.Text = "";
            TextBoxCost.Text = "";
        }

    }
}
