using System.Windows;
using System.Windows.Controls;
using TP2_14E_A2022.Users;

namespace TP2_14E_A2022.Pages
{
    public partial class PageConnection : Page
    {
        public PageConnection()
        {
            InitializeComponent();
        }
        
        private void Button_Connection_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectionSystem.ConnectUser(courrielTextBox.Text, mdpTextBox.Text))
            {
                if(ApplicationState.ConnectedUser.isAdmin)
                {
                    this.GoToMenuPage();
                }
                else 
                {
                    MessageBox.Show("Vous êtes un membre non-administrateur, votre page n'est pas encore finie..."
                        , "Page en construction", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Adresse ou mot de passe invalide", "Informations erronées", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
