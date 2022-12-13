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
using System.Security.Policy;
using System.IO;

namespace TP2_14E_A2022.Pages
{
    public partial class PageGarden : Page
    {
        List<Image> lotImages;
        string? baseUri = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        public PageGarden()
        {
            InitializeComponent();
            tbUser.Text = UserSystem.GetConnectedUserName();

            lotImages = new List<Image> { Lot0, Lot1, Lot2, Lot3, Lot4, Lot5, Lot6, Lot7, Lot8, Lot9, Lot10, Lot11, Lot12, Lot13, Lot14, Lot15, Lot16, Lot17, Lot18, Lot19 };

            FetchMaterials();
            FetchMembers();
            FetchGarden();
            listViewMaterials.SelectedIndex = 0;
            listViewMembers.SelectedIndex = 0;
            lotNumberTextBox.Text = "";
            lotOwnerTextBox.Text = "";
            lotStatusTextBox.Text = "";

            imageMaterial.Source = new BitmapImage(new Uri(baseUri + @"\Ressources\material.png"));
        }
        
        private void Button_Back_Main_Menu(object sender, RoutedEventArgs e)
        {
            this.GoToMenuPage();
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
            string selectedLotNumberTextBoxText = lotNumberTextBox.Text;
            int selectedLotNumber = -1;
            if (selectedLotNumberTextBoxText != "")
            {
                selectedLotNumber = Convert.ToInt32(selectedLotNumberTextBoxText);
                LotSystem.DeActivateLot(selectedLotNumber);
                FetchGarden();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un lot", "Aucun Lot", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button_Add_Member(object sender, RoutedEventArgs e)
        {
            string selectedLotNumberTextBoxText = lotNumberTextBox.Text;
            int selectedLotNumber = -1;
            if (selectedLotNumberTextBoxText != "")
            {
                selectedLotNumber = Convert.ToInt32(selectedLotNumberTextBoxText);
                if (listViewMembers.SelectedItem is User selectedUser)
                {
                    LotSystem.UpdateLotOwner(selectedLotNumber, selectedUser.id);
                    FetchGarden();
                }
                else
                {
                    MessageBox.Show("Veuillez sélectionner un membre", "Aucun Membre", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un lot", "Aucun Lot", MessageBoxButton.OK, MessageBoxImage.Warning);
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
            foreach (Lot lot in lots)
            {
                if (lot.state == LotState.Active)
                {
                    lotImages[lot.lotNumber].Source = new BitmapImage(new Uri(baseUri + @"\Ressources\active-lot.png"));
                }
                else if (lot.state == LotState.Inactive)
                {
                    lotImages[lot.lotNumber].Source = new BitmapImage(new Uri(baseUri + @"\Ressources\inactive-lot.png"));
                }
                else if (lot.state == LotState.Wintered)
                {
                    lotImages[lot.lotNumber].Source = new BitmapImage(new Uri(baseUri + @"\Ressources\wintered-lot.png"));
                }
            }
            TextBoxBagCountText.Text = LotSystem.GetNumberOfSoilBagsNeeded().ToString();
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

        private void listViewMaterials_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listViewMaterials.SelectedItem is Material selectedMaterial)
            {
                materialNameTextBox.Text = selectedMaterial.name;
                materialDescriptionTextBox.Text = selectedMaterial.description;
            }
            else
            {
                materialNameTextBox.Text = "";
                materialDescriptionTextBox.Text = "";
            }
        }

        private void Changed_Selected_Lot(object sender, RoutedEventArgs e)
        {
            FetchGarden();
            List<Lot> lots = LotSystem.GetLots();
            for (int i = 0; i < lots.Count; i++)
            {
                if (e.Source == lotImages[i])
                {
                    lotImages[i].Source = new BitmapImage(new Uri(baseUri + @"\Ressources\selected-lot.png"));

                    lotNumberTextBox.Text = lots[i].lotNumber.ToString();

                    if (lots[i].state == LotState.Active)
                    {
                        lotStatusTextBox.Text = "Actif";

                        User owner = UserSystem.GetUser((MongoDB.Bson.ObjectId)lots[i].ownerId);

                        lotOwnerTextBox.Text = owner.firstName + " " + owner.lastName;
                    }
                    else if (lots[i].state == LotState.Inactive)
                    {
                        lotStatusTextBox.Text = "Inactif";
                        lotOwnerTextBox.Text = "Aucun Propriétaire";
                    }
                    else if (lots[i].state == LotState.Wintered)
                    {
                        lotStatusTextBox.Text = "Hiverné";
                        lotOwnerTextBox.Text = "Aucun Propriétaire";
                    }
                }
            }
        }
    }
}
