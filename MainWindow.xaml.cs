using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace ADOWPF1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection connection;
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Stationery_company;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public MainWindow()
        {
            InitializeComponent();
        }
        private void ClearDataGrid()
        {
            DG_Users.ItemsSource = null;
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                MessageBox.Show("Підключення успішне!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка підключення: {ex.Message}");
            }
        }
        private void DisconnectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    MessageBox.Show("Відключено від бази даних.");
                }
                else
                {
                    MessageBox.Show("Немає активного підключення до бази даних.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка відключення: {ex.Message}");
            }
        }

        private void LoadDataButton_Click1(object sender, RoutedEventArgs e)
        {
            ClearDataGrid();
            try
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    Button clickedButton = sender as Button;

                    string query = GetQueryByButtonId(clickedButton.Name);
                    if (query != null)
                    {
                        DataTable dataTable = new DataTable();
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                adapter.Fill(dataTable);
                            }
                        }

                        DG_Users.ItemsSource = dataTable.DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("Невідома операція.");
                    }
                }
                else
                {
                    MessageBox.Show("Спочатку підключіться до бази даних.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження даних: {ex.Message}");
            }
        }

        private string GetQueryByButtonId(string buttonId)
        {
            switch (buttonId)
            {
                case "LoadAllProductsButton":
                    return "SELECT * FROM Products";
                case "LoadProductTypesButton":
                    return "SELECT DISTINCT ProductType FROM Products";
                case "LoadSalesManagersButton":
                    return "SELECT * FROM SalesManagers";
                case "LoadMaxQuantityProductsButton":
                    return "SELECT * FROM Products WHERE QuantityInStock = (SELECT MAX(QuantityInStock) FROM Products)";
                case "LoadMinQuantityProductsButton":
                    return "SELECT * FROM Products WHERE QuantityInStock = (SELECT MIN(QuantityInStock) FROM Products)";
                case "LoadMinCostProductsButton":
                    return "SELECT * FROM Products WHERE CostPrice = (SELECT MIN(CostPrice) FROM Products)";
                case "LoadMaxCostProductsButton":
                    return "SELECT * FROM Products WHERE CostPrice = (SELECT MAX(CostPrice) FROM Products)";
                default:
                    return null;
            }
        }
    

    private void LoadDataButton_Click(object sender, RoutedEventArgs e)
        {
            ClearDataGrid();
            try
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    DataTable dataTable = new DataTable();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Products", connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }

                    DG_Users.ItemsSource = dataTable.DefaultView;
                }
                else
                {
                    MessageBox.Show("Спочатку підключіться до бази даних.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження даних: {ex.Message}");
            }
        }
    }

}
    


