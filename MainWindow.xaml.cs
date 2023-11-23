using ClassLibrary1;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
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
        const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Vegetables_and_fruits;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        Class1 databaseManager;
        public MainWindow()
        {
            InitializeComponent();
            databaseManager = new Class1(connectionString);
        }
        private void Border_Click(object sender, MouseButtonEventArgs e)
        {
            ConnectToDatabase();
        }
        private void Border_Click2(object sender, MouseButtonEventArgs e)
        {
            LoadDataGrid();
            DisplayVegetableFruitNames();
            DisplayColors();
            ShowMaxCalories();
            ShowMinCalories();
            ShowAverageCalories();

            LoadDataGrid();
        }
        private void Border_Click3(object sender, MouseButtonEventArgs e)
        {
            ShowVegetableCount();
            ShowFruitCount();

            LoadDataGrid();
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
        private void LoadDataGrid()
        {
            DG_Users.ItemsSource = databaseManager.GetAllVegetablesAndFruits();
        }

        private void DisplayVegetableFruitNames()
        {
            List<string> names = databaseManager.GetVegetableFruitNames();
        }

        private void DisplayColors()
        {
            List<string> colors = databaseManager.GetVegetableFruitColors();
        }

        private void ShowMaxCalories()
        {
            int maxCalories = databaseManager.GetMaxCalories();
            MessageBox.Show($"Максимальна калорійність: {maxCalories}");
        }

        private void ShowMinCalories()
        {
            int minCalories = databaseManager.GetMinCalories();
            MessageBox.Show($"Мінімальна калорійність: {minCalories}");
        }

        private void ShowAverageCalories()
        {
            double averageCalories = databaseManager.GetAverageCalories();
            MessageBox.Show($"Середня калорійність: {averageCalories}");
        }
        private void ShowVegetableCount()
        {
            int vegetableCount = databaseManager.GetVegetableCount();
            MessageBox.Show($"Кількість овочів: {vegetableCount}");
        }

        private void ShowFruitCount()
        {
            int fruitCount = databaseManager.GetFruitCount();
            MessageBox.Show($"Кількість фруктів: {fruitCount}");
        }

        private void ShowItemCountByColor(string color)
        {
            int itemCount = databaseManager.GetItemCountByColor(color);
            MessageBox.Show($"Кількість овочів і фруктів кольору {color}: {itemCount}");
        }

        private void ShowItemCountByColor()
        {
            Dictionary<string, int> itemCounts = databaseManager.GetItemCountByColor();
            string result = "Кількість овочів і фруктів кожного кольору:\n";

            foreach (var pair in itemCounts)
            {
                result += $"{pair.Key}: {pair.Value}\n";
            }

            MessageBox.Show(result);
        }

        private void ShowItemsByCaloriesRange(int minCalories, int maxCalories)
        {
            List<VegetableFruit> items = databaseManager.GetItemsByCaloriesRange(minCalories, maxCalories);

        }

        private void ShowItemsByColorAndType(string color, string type)
        {
            List<VegetableFruit> items = databaseManager.GetItemsByColorAndType(color, type);

        }

    }
}

