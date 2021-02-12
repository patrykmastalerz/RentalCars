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

        private Rental rental;

        public EditRentalView(Rental rental)
        {
            InitializeComponent();

            rentalRepository = new RentalRepository();

            this.rental = rental;

        }

        private void editRental_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
