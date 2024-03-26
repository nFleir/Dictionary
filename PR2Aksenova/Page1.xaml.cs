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
using System.Data.SqlClient;

namespace PR2Aksenova
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
            string connectionString = "Data Source=FLEIRPC;Initial Catalog=definition;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = $@"
                                    SELECT * р
                                    FROM (
                                        SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS rownum
                                        FROM definition
                                    ) AS a;";

                // берем кол-во строк в бд
                string sqlQueryCount = "select count(*) from definition;";
                int count;
                using (SqlCommand command = new SqlCommand(sqlQueryCount, connection))
                {
                    count = (int)command.ExecuteScalar();
                }

                // выводим данные из бд
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListText.Items.Add($"{reader.GetString(1)} | {reader.GetString(2)} | {reader.GetString(3)}");
                        }
                    }
                }
            }
        }

        private void ShowWindow_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page2.xaml", UriKind.Relative));
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PonyatieText_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (PonyatieText.Text == "Понятие")
            {
                PonyatieText.Text = "";
            }
        }

        private void OpdredelenieText_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (OpdredelenieText.Text == "Определение")
            {
                OpdredelenieText.Text = "";
            }
        }
        private void AvtorText_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (AvtorText.Text == "Автор")
            {
                AvtorText.Text = "";
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (PonyatieText.Text == "Понятие" || PonyatieText.Text == "")
            {
                MessageBox.Show("Введите Понятие!", "Понятие", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (OpdredelenieText.Text == "Определение" || OpdredelenieText.Text == "")
            {
                MessageBox.Show("Введите определение!", "Определение", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (AvtorText.Text == "Автор" || AvtorText.Text == "")
            {
                MessageBox.Show("Введите автора!", "Автор", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string connectionString = "Data Source=FLEIRPC;Initial Catalog=definition;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Поместите значения в кавычки, если это строки
                string sqlQuery = $@"INSERT INTO definition(Понятие, Определение, Автор) VALUES ('{PonyatieText.Text}', '{OpdredelenieText.Text}', '{AvtorText.Text}')";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.ExecuteNonQuery(); // Используйте ExecuteNonQuery для операций вставки, обновления и удаления
                }
            }
            PonyatieText.Text = "Понятие";
            OpdredelenieText.Text = "Определение";
            AvtorText.Text = "Автор";
            MessageBox.Show("Данные успешно добавлены!", "Добавление", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            
            if (ListText.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите понятие!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var text = (ListText.SelectedItem).ToString().Split('|');
            string connectionString = "Data Source=FLEIRPC;Initial Catalog=definition;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = $@"delete from definition where понятие='{text[0]}'";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Данные успешно удалены!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
            ListText.Items.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = $@"
                                    SELECT * 
                                    FROM (
                                        SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS rownum
                                        FROM definition
                                    ) AS a;";

                // берем кол-во строк в бд
                string sqlQueryCount = "select count(*) from definition;";
                int count;
                using (SqlCommand command = new SqlCommand(sqlQueryCount, connection))
                {
                    count = (int)command.ExecuteScalar();
                }

                // выводим данные из бд
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListText.Items.Add($"{reader.GetString(1)} | {reader.GetString(2)} | {reader.GetString(3)}");
                        }
                    }
                }
            }
        }
    }
}
