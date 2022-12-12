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
using TP2_14E_A2022.Lots;
using TP2_14E_A2022.Materials;
using TP2_14E_A2022.Users;
using TP2_14E_A2022.DataModels;

namespace TP2_14E_A2022.Pages
{
    public partial class PageGarden : Page
    {
        public PageGarden()
        {
            InitializeComponent();
            FetchMaterials();
            FetchMembers();
            FetchGarden();
            listViewMaterials.SelectedIndex = 0;
            listViewMembers.SelectedIndex = 0;
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
            // TODO : remove user from lot

            FetchGarden();
        }

        private void Button_Add_Member(object sender, RoutedEventArgs e)
        {
            // TODO : add user to lot

            FetchGarden();
        }

        private void FetchMaterials()
        {
            listViewMaterials.ItemsSource = MaterialSystem.GetMaterials();
        }

        private void FetchMembers()
        {
            listViewMembers.ItemsSource = UserSystem.GetUsers();
        }

        private void FetchGarden()
        {
            List<Lot> lots = LotSystem.GetLots();
            Image[] lotImages = { Lot0, Lot1, Lot2, Lot3, Lot4, Lot5, Lot6, Lot7, Lot8, Lot9, Lot10, Lot11, Lot12, Lot13, Lot14, Lot15, Lot16, Lot17, Lot18, Lot19 };
            foreach (Lot lot in lots)
            {
                if (lot.state == LotState.Active)
                {
                    lotImages[lot.lotNumber].Source = new BitmapImage(new Uri("../Ressources/active-lot.png", UriKind.Relative));
                }
                else if (lot.state == LotState.Inactive)
                {
                    lotImages[lot.lotNumber].Source = new BitmapImage(new Uri("../Ressources/inactive-lot.png", UriKind.Relative));
                }
                else if (lot.state == LotState.Wintered)
                {
                    lotImages[lot.lotNumber].Source = new BitmapImage(new Uri("../Ressources/wintered-lot.png", UriKind.Relative));
                }
            }
        }

        private void Button_Winter_Lots(object sender, RoutedEventArgs e)
        {
            List<Lot> lots = LotSystem.GetLots();
            foreach (Lot lot in lots)
            {
                LotSystem.WinterLot(lot.lotNumber);
            }
        }
    }
}
