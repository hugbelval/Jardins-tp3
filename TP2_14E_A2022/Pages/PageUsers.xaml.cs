using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TP2_14E_A2022.DataModels;
using TP2_14E_A2022.Users;

namespace TP2_14E_A2022.Pages
{
    public partial class PageUsers : Page
    {
        public PageUsers()
        {
            InitializeComponent();
            FetchUsers();
            listViewUsers.SelectedIndex = 0;
            tbUser.Text = UserSystem.GetConnectedUserName();
        }
        private void Button_Disconnect_Click(object sender, MouseButtonEventArgs e)
        {
            PageConnection pageConnection = new PageConnection();

            this.NavigationService.Navigate(pageConnection);
        }

        private void Button_Back_Main_Menu(object sender, RoutedEventArgs e)
        {
            this.GoToMenuPage();
        }

        private void Button_Add_User(object sender, RoutedEventArgs e)
        {
            this.GoToPage<PageAddUser>();
        }

        private void Button_Modify_User(object sender, RoutedEventArgs e)
        {
            if (listViewUsers.SelectedItem is User selectedUser && subscriptionEndDatePicker.SelectedDate != null)
            {
                int selectedIndex = listViewUsers.SelectedIndex;
                selectedUser.firstName = firstNameTextBox.Text;
                selectedUser.lastName = lastNameTextBox.Text;
                selectedUser.email = emailTextBox.Text;
                selectedUser.telephone = telephoneTextBox.Text;
                selectedUser.address = addressTextBox.Text;
                selectedUser.subscriptionEnd = (DateTime)subscriptionEndDatePicker.SelectedDate;

                if (UserSystem.UpdateUser(selectedUser) == null)
                {
                    MessageBox.Show("Veuillez entrer toutes les informations correctement", "Champs Invalides", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                FetchUsers();
                listViewUsers.SelectedIndex = selectedIndex;
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un utilisateur", "Aucun Utilisateur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button_Delete_User(object sender, RoutedEventArgs e)
        {
            UserSystem.DeleteUser(listViewUsers.SelectedItem);
            FetchUsers();
        }

        private void FetchUsers()
        {
            listViewUsers.ItemsSource = UserSystem.GetUsers();
        }

        private void listViewUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listViewUsers.SelectedItem is User selectedUser)
            {
                firstNameTextBox.Text = selectedUser.firstName;
                lastNameTextBox.Text = selectedUser.lastName;
                emailTextBox.Text = selectedUser.email;
                telephoneTextBox.Text = selectedUser.telephone;
                addressTextBox.Text = selectedUser.address;
                subscriptionEndDatePicker.SelectedDate = selectedUser.subscriptionEnd;
            }
            else
            {
                firstNameTextBox.Text = string.Empty;
                lastNameTextBox.Text = string.Empty;
                emailTextBox.Text = string.Empty;
                telephoneTextBox.Text = string.Empty;
                addressTextBox.Text = string.Empty;
                subscriptionEndDatePicker.SelectedDate = null;
            }
        }
    }
}




 

