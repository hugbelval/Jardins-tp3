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
using TP2_14E_A2022.Materials;
using TP2_14E_A2022.Users;

namespace TP2_14E_A2022.Pages
{
    public partial class PageGarden : Page
    {
        public PageGarden()
        {
            InitializeComponent();
            tbUser.Text = UserSystem.GetConnectedUserName();
        }
        
        private void Button_Back_Main_Menu(object sender, RoutedEventArgs e)
        {
            PageConnection pageConnection = new PageConnection();

            this.NavigationService.Navigate(pageConnection);
        }

        private void Button_Disconnect_Click(object sender, RoutedEventArgs e)
        {
            this.GoToConnectionPage();
        }

        private void Button_Add_Material(object sender, RoutedEventArgs e)
        {
            this.GoToPage<PageAddMaterial>();
        }

        private void Button_Delete_Material(object sender, RoutedEventArgs e)
        {
            MaterialSystem.DeleteMaterial(listViewMaterials.SelectedItem);
            FetchMaterials();
        }

        private void Button_Remove_Member(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Add_Member(object sender, RoutedEventArgs e)
        {

        }

        private void FetchMaterials()
        {
            listViewMaterials.ItemsSource = MaterialSystem.GetMaterials();
        }

        private void FetchMembers()
        {
            listViewMembers.ItemsSource = UserSystem.GetUsers();
        }

    }
}
