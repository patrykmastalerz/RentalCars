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
    /// Widok odpowiedzialny za dodanie wynajmu do bazy danych. Widok waliduje wpisane przez użytkownika w polach wartości oraz po pozytywnym dodaniu
    /// resetuje je, oraz oblicza koszt wynajmu.
    /// </summary>
    public partial class AddRentalView : Window
    {
        private readonly RentalRepository rentalRepository = null;
        private readonly CarRepository carRepository = null;
        private DateTime textBoxData;
        private int customerId;
        private int carId;
        private int rentalDuration;

        /// <summary>
        /// Inicjalizuje obiekt AddRentalView oraz inicjalizuje RentalRepository oraz CarRepository użyte w klasie.
        /// </summary>
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
                    From = textBoxData,
                    To = textBoxData.AddDays(Convert.ToInt32(TextBoxRentalDuration.Text)),
                    Cost = CalculateCost(),
                    CustomerId = customerId,
                    CarId = carId
                };

                try
                {
                    rentalRepository.AddRental(rental);
                    MainWindow.rentalGridData.ItemsSource = rentalRepository.GetAll();
                    ResetFields();
                    MessageBox.Show("Nowy zamówienie zostało dodane");
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message);
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

        private void ResetFields()
        {
            TextBoxCar.Text = "";
            TextBoxCustomerId.Text = "";
            TextBoxRentalFrom.Text = "";
            TextBoxRentalDuration.Text = "";
        }

        private string ValidateRental()
        {
            string output = "";

            if (!int.TryParse(TextBoxCar.Text, out carId))
            {
                output = "Wprowadż numer samochodu!";
            }
            else if (!int.TryParse(TextBoxCustomerId.Text, out customerId))
            {
                output = "Wprowadż numer użytkownika!";
            }
            else if (!DateTime.TryParse(TextBoxRentalFrom.Text, out textBoxData))
            {
                output = "Wprowadż date!";
            }
            else if (!int.TryParse(TextBoxRentalDuration.Text, out rentalDuration))
            {
                output = "Wprowadż czas wynajmu!";
            }

            return output;
        }

        private decimal CalculateCost()
        {
            var duration = rentalDuration;
            var carToFind = carId;

            var car = carRepository.Get(carToFind);
            if (car != null)
            {
                var carCost = car.Cost;
                return (decimal)(duration * carCost);
            }

            return 0;   
        }


    }
}
