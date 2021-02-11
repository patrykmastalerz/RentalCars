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
    /// Interaction logic for AddCustomerView.xaml
    /// </summary>
    public partial class AddCustomerView : Window
    {
        private readonly CustomerRepository repository = null;
        public AddCustomerView()
        {
            InitializeComponent();
            repository = new CustomerRepository();
        }

        private void addCustomer_Click(object sender, RoutedEventArgs e)
        {
            var validationMessage = ValidateCustomer();
            if (string.IsNullOrEmpty(validationMessage) == false)
            {
                MessageBox.Show(validationMessage);
            }
            else
            {
                SaveCustomerToDataBase();
            }
        }

        private void SaveCustomerToDataBase()
        {
            try
            {
                Customer customer = new Customer()
                {
                    FirstName = TextBoxFirstName.Text,
                    SecondName = TextBoxSecondName.Text,
                    PhoneNumber = TextBoxPhoneNumber.Text
                };

                repository.AddCustomer(customer);
                ResetFields();
                MainWindow.customerGridData.ItemsSource = repository.GetAll();
                MessageBox.Show("Nowy użytkownik został dodany");
            }
            catch (Exception)
            {
                MessageBox.Show("Nie udało się dodać użytkownika", "", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private string ValidateCustomer()
        {
            string output = "";
            if (string.IsNullOrEmpty(TextBoxFirstName.Text))
            {
                output = "Wprowadź Imie!";
            }
            else if (TextBoxFirstName.Text.Length >= 30)
            {
                output = "Przekroczono liczbe znaków!";
            }
            else if (string.IsNullOrEmpty(TextBoxSecondName.Text))
            {
                output = "Wprowadź Nazwisko!";
            }
            else if (TextBoxSecondName.Text.Length >= 30)
            {
                output = "Przekroczono liczbe znaków!";
            }
            else if ((TextBoxPhoneNumber.Text.Length != 9) || System.Text.RegularExpressions.Regex.IsMatch(TextBoxPhoneNumber.Text, "[^0-9]"))
            {
                output = "Wprowadzono nie prawidłowy numer telefonu!";
            }
            return output;
        }

        private void ResetFields()
        {
            TextBoxFirstName.Text = "";
            TextBoxSecondName.Text = "";
            TextBoxPhoneNumber.Text = "";
        }

    }
}
