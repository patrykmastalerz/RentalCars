﻿using RentalCars.Models;
using RentalCars.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RentalCars
{
    /// <summary>
    /// Głowny widok odpowiedzialny za wyświetlanie, usuwanie oraz modyfikowanie obiektów typu Car, Rental, Customer. 
    /// Widok wyświetla 3 siatki z aktualnymi danymi z samochodami, klientami oraz wynajmami zawartych w bazie danych oraz 
    /// przyciski akcji umożliwiające usuwanie lub modyfikacje wybranych elementów. Widok wyświetla osobne przyciski pod każdą z siatek
    /// otwierające widoki odpowiedzialne za dodawanie nowych elementów do bazy.
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CarRepository carRepository = null;
        private readonly CustomerRepository customerRepository = null;
        private readonly RentalRepository rentalRepository = null;

        /// <summary>
        /// Zmienna typu DataGrid posiadająca informacje o siatce widoku Car
        /// </summary>
        public static DataGrid carGridData;

        /// <summary>
        /// Zmienna typu DataGrid posiadająca informacje o siatce widoku Customer
        /// </summary>
        public static DataGrid customerGridData;

        /// <summary>
        /// Zmienna typu DataGrid posiadająca informacje o siatce widoku Rental
        /// </summary>
        public static DataGrid rentalGridData;

        /// <summary>
        /// Inicjalizuje widok oraz repozytoria CarRepository, CustomerRepository oraz RentalRepository użytych w klasie. 
        /// Konstruktor również wypełnia danymi każdą z siatek, odpowiedzialnych za wyświetlanie danych o Car, Rental oraz Customer.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            carRepository = new CarRepository();
            customerRepository = new CustomerRepository();
            rentalRepository = new RentalRepository();

            PopulateCarGrid();
            PopulateCustomerGrid();
            PopulateRentalGrid();
        }


        private void PopulateCarGrid()
        {
            carGrid.ItemsSource = carRepository.GetAll();
            carGridData = carGrid;
        }

        private void PopulateCustomerGrid()
        {
            customersGrid.ItemsSource = customerRepository.GetAll();
            customerGridData = customersGrid;
        }

        private void PopulateRentalGrid()
        {
            rentalGrid.ItemsSource = rentalRepository.GetAll();
            rentalGridData = rentalGrid;
        }


        private void addCarBatton_Click(object sender, RoutedEventArgs e)
        {
            AddCarView carView = new AddCarView();
            carView.Show();
        }




        private void carEdit_Click(object sender, RoutedEventArgs e)
        {
            var car = carGridData.SelectedItem as Car;
            CarEditView carEditView = new CarEditView(car);
            carEditView.Show();
        }

        private void carDelate_Click(object sender, RoutedEventArgs e)
        {
            var car = (Car)carGridData.SelectedItem;

            try
            {
                carRepository.RemoveCar(car);
                MessageBox.Show("Usunięto samochód");
            }
            catch (Exception)
            {
                MessageBox.Show("Nie można usunąć tego samochodu!");
            }
            finally
            {
                PopulateCarGrid();
            }
        }

        private void addCustomer_Click(object sender, RoutedEventArgs e)
        {
            AddCustomerView addCustomerView = new AddCustomerView();
            addCustomerView.Show();
        }

        private void editCustomer_Click(object sender, RoutedEventArgs e)
        {
            var customer = customerGridData.SelectedItem as Customer;
            EditCustomerView editCustomerView = new EditCustomerView(customer);
            editCustomerView.Show();
        }

        private void addRental_Click(object sender, RoutedEventArgs e)
        {
            AddRentalView addRentalView = new AddRentalView();
            addRentalView.Show();
        }

        private void rentalDelete_Click(object sender, RoutedEventArgs e)
        {
            var rental = (Rental)rentalGridData.SelectedItem;

            try
            {
                rentalRepository.RemoveRental(rental.Id);
                MessageBox.Show("Usunięto zamówienie");
            }
            catch (Exception)
            {
                MessageBox.Show("Nie można usunąć tego zamówienia!");
            }
            finally
            {
                PopulateRentalGrid();
            }
        }

        private void rentalEdit_Click(object sender, RoutedEventArgs e)
        {
            var rental = (Rental)rentalGridData.SelectedItem;
            EditRentalView editRentalView = new EditRentalView(rental);
            editRentalView.Show();
        }
    }
}
