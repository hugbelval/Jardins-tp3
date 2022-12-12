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
            BitmapImage bitmapImage = new BitmapImage();
            Uri uri = new("../Ressources/selected-lot.png");
            bitmapImage.BaseUri = uri;

            List <Lot> lots = LotSystem.GetLots();
            Image[] lotImages = { Lot0, Lot1, Lot2, Lot3, Lot4, Lot5, Lot6, Lot7, Lot8, Lot9, Lot10, Lot11, Lot12, Lot13, Lot14, Lot15, Lot16, Lot17, Lot18, Lot19 };
            
            for (int i = 0; i < lotImages.Length; i++)
            {
                if (lotImages[i].Source == bitmapImage)
                {
                    if(LotSystem.DeActivateLot(i) != null)
                    {
                        FetchGarden();
                    }
                    else
                    {
                        MessageBox.Show("Le lot n'existe pas dans le système", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez sélectionner un lot", "Aucun Lot", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            FetchGarden();
        }

        private void Button_Add_Member(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmapImage = new BitmapImage();
            Uri uri = new("../Ressources/selected-lot.png");
            bitmapImage.BaseUri = uri;

            List<Lot> lots = LotSystem.GetLots();
            Image[] lotImages = { Lot0, Lot1, Lot2, Lot3, Lot4, Lot5, Lot6, Lot7, Lot8, Lot9, Lot10, Lot11, Lot12, Lot13, Lot14, Lot15, Lot16, Lot17, Lot18, Lot19 };

            if (listViewMembers.SelectedItem is User selectedUser)
            {
                for (int i = 0; i < lotImages.Length; i++)
                {
                    if (lotImages[i].Source == bitmapImage)
                    {
                        if (LotSystem.UpdateLotOwner(i, selectedUser.id) != null)
                        {
                            FetchGarden();
                        }
                        else
                        {
                            MessageBox.Show("Le lot ou le membre n'existe pas dans le système", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Veuillez sélectionner un lot", "Aucun Lot", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un membre", "Aucun Membre", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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
                if (LotSystem.WinterLot(lot.lotNumber) != null)
                {
                    FetchGarden();
                }
                else
                {
                    MessageBox.Show("Les lots n'ont pas étés hivernés", "Lots Invalides", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
