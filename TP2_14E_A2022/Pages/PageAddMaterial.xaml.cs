using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TP2_14E_A2022.Materials;
using TP2_14E_A2022.Users;

namespace TP2_14E_A2022.Pages
{
    public partial class PageAddMaterial : Page
    {
        public PageAddMaterial()
        {
            InitializeComponent();
            tbUser.Text = UserSystem.GetConnectedUserName();
        }
        private void Button_Disconnect_Click(object sender, MouseButtonEventArgs e)
        {
            this.GoToConnectionPage();
        }
        private void Button_Back_Main_Menu(object sender, RoutedEventArgs e)
        {
            this.GoToMenuPage();
        }

        private void Button_Add_Material(object sender, RoutedEventArgs e)
        {
            if(MaterialSystem.AddMaterial(nomTextBox.Text, descriptionTextBox.Text) != null)
            {
                GoToMaterialsPage();
            }
            else
            {
                MessageBox.Show("Le nom du matériel ne peut pas être vide", "Champs Invalides", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void GoToMaterialsPage()
        {
            this.GoToPage<PageMaterials>();
        }
    }
}
