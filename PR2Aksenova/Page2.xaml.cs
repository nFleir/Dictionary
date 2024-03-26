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
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public int back = 0;
        public int count = 0;
        public Page2()
        {
            InitializeComponent();
            string connectionString = "Data Source=FLEIRPC;Initial Catalog=definition;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = $@"
                                    SELECT * 
                                    FROM (
                                        SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS rownum
                                        FROM definition
                                    ) AS a
                                    WHERE rownum > {0} AND rownum <= {5};";

                // берем кол-во строк в бд
                string sqlQueryCount = "select count(*) from definition;";
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
            //ShowFrame.Content = new Page2();
        }

        private void AddRemove_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
        }


        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (back >= 5) back -= 5;
            ListText.Items.Clear();
            string connectionString = "Data Source=FLEIRPC;Initial Catalog=definition;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = $@"
                                    SELECT * 
                                    FROM (
                                        SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS rownum
                                        FROM definition
                                    ) AS a
                                    WHERE rownum > {back} AND rownum <= {back + 5};";

                // берем кол-во строк в бд
                string sqlQueryCount = "select count(*) from definition;";
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

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (back < count - 5) back += 5;
            ListText.Items.Clear();
            string connectionString = "Data Source=FLEIRPC;Initial Catalog=definition;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = $@"
                                    SELECT * 
                                    FROM (
                                        SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS rownum
                                        FROM definition
                                    ) AS a
                                    WHERE rownum > {back} AND rownum <= {back + 5};";

                // берем кол-во строк в бд
                string sqlQueryCount = "select count(*) from definition;";
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
