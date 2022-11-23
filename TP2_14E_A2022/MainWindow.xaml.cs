using System.Windows;
using TP2_14E_A2022.Pages;

namespace TP2_14E_A2022
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _NavigationFrame.Navigate(new PageConnexion());
        }

    }
}
