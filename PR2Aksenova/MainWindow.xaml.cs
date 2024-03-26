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

namespace PR2Aksenova
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ShowWindow_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("/Page2.xaml", UriKind.Relative));
        }

        private void AddRemove_Click(object sender, RoutedEventArgs e)
        {
            //MainFrame.Content = new Page1();
            MainFrame.NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
        }
    }
}
