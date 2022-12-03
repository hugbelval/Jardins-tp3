using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TP2_14E_A2022.Users;

namespace TP2_14E_A2022.Pages
{
    /// <summary>
    /// Logique d'interaction pour PageAddUser.xaml
    /// </summary>
    public partial class PageAddUser : Page
    {
        public PageAddUser()
        {
            InitializeComponent();
            tbUser.Text = UserSystem.GetConnectedUserName();
        }
        private void Button_Deconnect_Click(object sender, MouseButtonEventArgs e)
        {
            this.GoToConnectionPage();
        }
        private void Button_Back_Main_Menu(object sender, RoutedEventArgs e)
        {
            this.GoToMenuPage();
        }

        private void Button_Ajouter_User(object sender, RoutedEventArgs e)
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
