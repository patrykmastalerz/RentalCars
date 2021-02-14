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
    /// Interaction logic for EditRentalView.xaml
    /// </summary>
    public partial class EditRentalView : Window
    {

        private readonly RentalRepository rentalRepository = null;
        private readonly CarRepository carRepository = null;

        private Rental rental;
        private DateTime textBoxData;
        private int carId;
        private int rentalDuration;


        public EditRentalView(Rental rental)
        {
            InitializeComponent();

            rentalRepository = new RentalRepository();
            carRepository = new CarRepository();


            this.rental = rental;
            InitializeFields();
        }

        private void editRental_Click(object sender, RoutedEventArgs e)
        {
            var validationMessage = ValidateRental();
            if (string.IsNullOrEmpty(validationMessage) == false)
            {
                MessageBox.Show(validationMessage);
            }
            else
            {
                EditRentalToDataBase();
            }

        }

        private void EditRentalToDataBase()
        {
            try
            {
                Rental newRental = new Rental()
                {
                    From = textBoxData,
                    To = textBoxData.AddDays(rentalDuration),
                    Cost = CalculateCost(),
                    CarId = carId
                };

                try
                {
                    rentalRepository.UpdateRental(rental.Id, newRental);
                    MainWindow.rentalGridData.ItemsSource = rentalRepository.GetAll();
                    ResetFields();
                    MessageBox.Show("Zaktualizowano zamówienie!");
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

        private void InitializeFields()
        {
            TextBoxCar.Text = rental.CarId.ToString();
            TextBoxRentalFrom.Text = rental.From.ToShortDateString();
            TextBoxRentalDuration.Text = CalculateDuration().ToString();

        }

        private int CalculateDuration()
        {
            var from = rental.From;
            var to = rental.To;
            var result = to - from;

            return result.Days;
        }

        private void ResetFields()
        {
            TextBoxCar.Text = "";
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
