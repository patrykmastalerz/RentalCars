using RentalCars.Models;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CarRepository carRepository = null;
        private readonly CustomerRepository customerRepository = null;

        public static DataGrid carGridData;
        public static DataGrid customerGridData;

        public MainWindow()
        {
            InitializeComponent();
            carRepository = new CarRepository();
            customerRepository = new CustomerRepository();

            PopulateCarGrid();
            PopulateCustomerGrid();
        }


        private void addCarBatton_Click(object sender, RoutedEventArgs e)
        {
            AddCarView carView = new AddCarView();
            carView.Show();
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


    }
}
