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
            InitializeFields();
        }

        private void editCustomer_Click(object sender, RoutedEventArgs e)
        {
            var validateMessage = ValidateCustomer();
            if (string.IsNullOrEmpty(validateMessage) == false)
            {
                MessageBox.Show(validateMessage);
            }
            else
            {
                UpdateCustomerToDataBase();
            }

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
                ResetFields();
                MainWindow.customerGridData.ItemsSource = repository.GetAll();
                MessageBox.Show("Zaktualizowano użytkownika!");

            }
            catch (Exception)
            {
                MessageBox.Show("Niestety wystapił błąd, który uniemożliwia zapisanie użytkownika!");
            }
        }

        private string ValidateCustomer()
        {
            string output = "";
            if (string.IsNullOrEmpty(TextBoxFirstName.Text))
            {
                output = "Wprowadz Imie!";
            }
            else if (TextBoxFirstName.Text.Length > 30)
            {
                output = "Przekroczono liczbe znaków!";
            }
            else if (string.IsNullOrEmpty(TextBoxSecondName.Text))
            {
                output = "Wprowadz Nazwisko!";
            }
            else if (TextBoxSecondName.Text.Length > 30)
            {
                output = "Przekroczono liczbe znaków!";
            }
            else if ((TextBoxPhoneNumber.Text.Length != 9) || System.Text.RegularExpressions.Regex.IsMatch(TextBoxPhoneNumber.Text, "[^0-9]"))
            {
                output = "Wprowadz nie prawidłowy numer telefonu!";
            }
            return output;
        }


        private void ResetFields()
        {
            TextBoxFirstName.Text = "";
            TextBoxSecondName.Text = "";
            TextBoxPhoneNumber.Text = "";
        }

        private void InitializeFields()
        {
            TextBoxFirstName.Text = customer.FirstName;
            TextBoxSecondName.Text = customer.SecondName;
            TextBoxPhoneNumber.Text = customer.PhoneNumber;
        }

    }
}
