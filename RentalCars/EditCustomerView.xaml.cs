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
    /// Interaction logic for EditCustomerView.xaml
    /// </summary>
    public partial class EditCustomerView : Window
    {
        private readonly CustomerRepository repository = null;
        private Customer customer;

        public EditCustomerView(Customer customer)
        {
            InitializeComponent();
            repository = new CustomerRepository();
            this.customer = customer;
        }

        private void editCustomer_Click(object sender, RoutedEventArgs e)
        {
            UpdateCustomerToDataBase();
        }

        private void UpdateCustomerToDataBase()
        {
            try
            {
                Customer newCustomer = new Customer()
                {
                    FirstName = TextBoxFirstName.Text,
                    SecondName = TextBoxSecondName.Text,
                    PhoneNumber = TextBoxPhoneNumber.Text
                };

                repository.UpdateCustomer(customer.Id, newCustomer);
                MainWindow.customerGridData.ItemsSource = repository.GetAll();
                MessageBox.Show("Zaktualizowano użytkownika!");

            }
            catch (Exception)
            {
                MessageBox.Show("Niestety wystapił błąd, który uniemożliwia zapisanie użytkownika!");
            }
        }

    }
}
