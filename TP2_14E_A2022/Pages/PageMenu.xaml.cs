using System.Windows.Controls;
using System.Windows.Input;
using TP2_14E_A2022.Users;

namespace TP2_14E_A2022.Pages
{
    /// <summary>
    /// Logique d'interaction pour PageMenu.xaml
    /// </summary>
    public partial class PageMenu : Page
    {
        public PageMenu()
        {
            InitializeComponent();
            tbUser.Text = UserSystem.GetConnectedUserName();
        }

        private void Button_Deconnect_Click(object sender, MouseButtonEventArgs e)
        {
            this.GoToConnectionPage();
        }

        private void Button_NavigationPageMembres(object sender, System.Windows.RoutedEventArgs e)
        {
            this.GoToPage<PageUsers>();
        }
        
        private void Button_NavigationPageMaterial(object sender, System.Windows.RoutedEventArgs e)
        {
            this.GoToPage<PageMaterial>();
        }
    }
}
