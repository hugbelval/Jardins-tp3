using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TP2_14E_A2022.Materials;
using TP2_14E_A2022.Users;

namespace TP2_14E_A2022.Pages
{
    /// <summary>
    /// Logique d'interaction pour PageAjouterMaterial.xaml
    /// </summary>
    public partial class PageAjouterMaterial : Page
    {
        public PageAjouterMaterial()
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

        private void Bouton_Ajouter_Material(object sender, RoutedEventArgs e)
        {
            if(LotSystem.AddMaterial(nomTextBox.Text, descriptionTextBox.Text) != null)
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
            this.GoToPage<PageMaterial>();
        }
    }
}
