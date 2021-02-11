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
    /// Interaction logic for CarEditView.xaml
    /// </summary>
    public partial class CarEditView : Window
    {

        private readonly CarRepository repository = null;

        private Car car;

        public CarEditView(Car car)
        {
            InitializeComponent();
            repository = new CarRepository();

            this.car = car;
            InitializeFields();
        }

        private void editCar_Click(object sender, RoutedEventArgs e)
        {
            var validationMessage = ValidateCar();
            if (string.IsNullOrEmpty(validationMessage) == false)
            {
                MessageBox.Show(validationMessage);
            }
            else
            {
                EditCarToDataBase();
            }
        }

        private void EditCarToDataBase()
        {
            try
            {
                Car newCar = new Car()
                {
                    Marka = TextBoxBrand.Text,
                    Model = TextBoxModel.Text,
                    RegistrationNumber = TextBoxRegistrationNumber.Text,
                    Cost = Decimal.Parse(TextBoxCost.Text),
                };

                CarService newCarService = new CarService()
                {
                    From = DateTime.Parse(TextBoxFrom.Text),
                    To = DateTime.Parse(TextBoxFrom.Text).AddYears(1)
                };

                newCar.CarService = newCarService;
                try
                {
                    repository.UpdateCar(car.Id, newCar);
                    ResetFields();
                    MainWindow.carGridData.ItemsSource = repository.GetAll();
                    MessageBox.Show("Samochód został zaktualizowany");
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Nie można edytować pojazdu, na którym jest zamówienie");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nie udało się zaktualizować samochodu" + ex, "", MessageBoxButton.OK,
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

        private void InitializeFields()
        {
            TextBoxBrand.Text = car.Marka;
            TextBoxModel.Text = car.Model;
            TextBoxRegistrationNumber.Text = car.RegistrationNumber;
            TextBoxFrom.Text = car.CarService.From.ToShortDateString();
            TextBoxCost.Text = car.Cost.ToString();
        }

 
    }
}
