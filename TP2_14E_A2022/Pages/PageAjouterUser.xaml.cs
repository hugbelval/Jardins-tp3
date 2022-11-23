using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TP2_14E_A2022.Users;

namespace TP2_14E_A2022.Pages
{
    /// <summary>
    /// Logique d'interaction pour PageAjouterUser.xaml
    /// </summary>
    public partial class PageAjouterUser : Page
    {
        public PageAjouterUser()
        {
            InitializeComponent();
            tbUser.Text = UserSystem.GetConnectedUserName();
        }
        private void BoutonDeconnexion_Click(object sender, MouseButtonEventArgs e)
        {
            this.GoToConnectionPage();
        }
        private void Bouton_Retour_Menu(object sender, RoutedEventArgs e)
        {
            this.GoToMenuPage();
        }

        private void Bouton_Ajouter_User(object sender, RoutedEventArgs e)
        {
            if (UserSystem.AddUser(emailTextBox.Text, pwdTextBox.Text, firstNameTextBox.Text,
                lastNameTextBox.Text, addressTextBox.Text, telephoneTextBox.Text, DateTime.Now.AddYears(1), false) != null)
            {
                GoToUsersPage();
            }
            else
            {
                MessageBox.Show("Veuillez entrer toutes les informations correctement", "Champs Invalides", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GoToUsersPage()
        {
            this.GoToPage<PageUsers>();
        }
    }
}
