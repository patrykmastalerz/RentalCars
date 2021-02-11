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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CarRepository carRepository = null;

        public static DataGrid carGridData;

        public MainWindow()
        {
            InitializeComponent();
            carRepository = new CarRepository();

            PopulateCarGrid();
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
    }
}
