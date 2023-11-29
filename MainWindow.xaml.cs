using ClassLibrary1;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using static ClassLibrary1.Class1;

namespace ADOWPF1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel viewModel;
        const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=storage;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        Class1 databaseManager;
        public MainWindow()
        {
            InitializeComponent();
            databaseManager = new Class1(connectionString);
            viewModel = new ViewModel();
            DataContext = viewModel;
        }
        private void Border_Click(object sender, MouseButtonEventArgs e)
        {
            ConnectToDatabase();
        }
            private void Border_Click2(object sender, MouseButtonEventArgs e)
            {
            DG_Users.ItemsSource = viewModel.GetAllProducts();
        }
        private void Border_Click3(object sender, MouseButtonEventArgs e)
        {
            DG_Users.ItemsSource = viewModel.GetAllProducts();
        }

        private void ConnectToDatabase()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("Підключено до бази даних.");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка підключення до бази даних: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                    MessageBox.Show("Відключено від бази даних.");
                }
            }

        }
        private void ShowAllInformation_Click(object sender, RoutedEventArgs e)
        {
            DG_Users.ItemsSource = viewModel.GetAllProducts();
        }
        private void ShowAllProducts_Click(object sender, RoutedEventArgs e)
        {
            DG_Users.ItemsSource = viewModel.Products;
        }
        private void ShowProductsByCategory_Click(object sender, RoutedEventArgs e)
        {
            string selectedCategory = "Тип1"; 
            DG_Users.ItemsSource = viewModel.GetProductsByCategory(selectedCategory);
        }

        private void ShowProductsBySupplier_Click(object sender, RoutedEventArgs e)
        {
            string selectedSupplier = "Постачальник1"; 
            DG_Users.ItemsSource = viewModel.GetProductsBySupplier(selectedSupplier);
        }

        private void ShowOldestProductInStock_Click(object sender, RoutedEventArgs e)
        {
            DG_Users.ItemsSource = new List<Product> { viewModel.GetOldestProductInStock() };
        }

        private void ShowAverageQuantityByProductType_Click(object sender, RoutedEventArgs e)
        {
            var averageQuantities = viewModel.GetAverageQuantityByProductType();

            var averageQuantityProducts = averageQuantities.Select(kv => new Product
            {
                ProductType = kv.Key,
                Quantity = (int)kv.Value 
            }).ToList();

            DG_Users.ItemsSource = averageQuantityProducts;
        }
    }
}

