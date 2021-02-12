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
    /// Interaction logic for AddRentalView.xaml
    /// </summary>
    public partial class AddRentalView : Window
    {
        private readonly RentalRepository rentalRepository = null;
        private readonly CarRepository carRepository = null;

        public AddRentalView()
        {
            InitializeComponent();
            rentalRepository = new RentalRepository();
            carRepository = new CarRepository();
        }

        private void addRental_Click(object sender, RoutedEventArgs e)
        {
            var validationMessage = ValidateRental();
            if (string.IsNullOrEmpty(validationMessage) == false)
            {
                MessageBox.Show(validationMessage);
            }
            else
            {
                SaveRentalToDataBase();
            }

        }

        private void SaveRentalToDataBase()
        {
            try
            {
                Rental rental = new Rental()
                {
                    From = DateTime.Parse(TextBoxRentalFrom.Text),
                    To = DateTime.Parse(TextBoxRentalFrom.Text).AddDays(Convert.ToInt32(TextBoxRentalDuration.Text)),
                    Cost = CalculateCost(),
                    CustomerId = Convert.ToInt32(TextBoxCustomerId.Text),
                    CarId = Convert.ToInt32(TextBoxCar.Text)
                };

                try
                {
                    rentalRepository.AddRental(rental);
                    MessageBox.Show("Nowy zamówienie zostało dodane");
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Wprowadzono nieprawidłowe id użytkownika lub id samochodu!");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Niestety, podane dane są nieprawidłowe - popraw je!", "", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }




        }

        private string ValidateRental()
        {
            string output = "";

            if (string.IsNullOrEmpty(TextBoxCustomerId.Text) || System.Text.RegularExpressions.Regex.IsMatch(TextBoxRentalDuration.Text, "[^0-9]"))
            {
                output = "Wprowadż numer użytkownika!";
            }
            else if (string.IsNullOrEmpty(TextBoxCar.Text) || System.Text.RegularExpressions.Regex.IsMatch(TextBoxRentalDuration.Text, "[^0-9]"))
            {
                output = "Wprowadż numer samochodu!";
            }
            else if (string.IsNullOrEmpty(TextBoxRentalFrom.Text))
            {
                output = "Wprowadż date!";
            }
            else if (string.IsNullOrEmpty(TextBoxRentalDuration.Text) || System.Text.RegularExpressions.Regex.IsMatch(TextBoxRentalDuration.Text, "[^0-9]"))
            {
                output = "Wprowadż czas wynajmu!";
            }

            return output;
        }

        private decimal CalculateCost()
        {
            var duration = Decimal.Parse(TextBoxRentalDuration.Text);
            var carId = Convert.ToInt32(TextBoxCar.Text);

            var car = carRepository.Get(carId);
            if (car != null)
            {
                var carCost = car.Cost;
                return (decimal)(duration * carCost);
            }

            return 0;   
        }


    }
}
