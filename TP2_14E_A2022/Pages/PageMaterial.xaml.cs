﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TP2_14E_A2022.DataModels;
using TP2_14E_A2022.Materials;
using TP2_14E_A2022.Users;

namespace TP2_14E_A2022.Pages
{
    /// <summary>
    /// Logique d'interaction pour PageMaterial.xaml
    /// </summary>
    public partial class PageMaterial : Page
    {
        public PageMaterial()
        {
            InitializeComponent();
            FetchMaterials();
            listViewMaterials.SelectedIndex = 0;
            tbUser.Text = UserSystem.GetConnectedUserName();
        }
        private void BoutonDeconnexion_Click(object sender, MouseButtonEventArgs e)
        {
            PageConnexion pageConnexion = new PageConnexion();

            this.NavigationService.Navigate(pageConnexion);
        }

        private void Bouton_Retour_Menu(object sender, RoutedEventArgs e)
        {
            this.GoToMenuPage();
        }
        
        private void Button_Ajouter_Material(object sender, RoutedEventArgs e)
        {
            this.GoToPage<PageAjouterMaterial>();
        }

        private void Button_Modifier_Material(object sender, RoutedEventArgs e)
        {
            if(listViewMaterials.SelectedItem is Material selectedMaterial)
            {
                int selectedIndex = listViewMaterials.SelectedIndex;
                selectedMaterial.name = nameTextBox.Text;
                selectedMaterial.description = descriptionTextBox.Text;
                    
                if(MaterialSystem.UpdateMaterial(selectedMaterial) == null)
                {
                    MessageBox.Show("Le nom du matériel ne peut pas être vide", "Champs Invalides", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                FetchMaterials();
                listViewMaterials.SelectedIndex = selectedIndex;
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un matériel", "Aucun Matériel", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button_Supprimer_Material(object sender, RoutedEventArgs e)
        {
            MaterialSystem.DeleteMaterial(listViewMaterials.SelectedItem);
            FetchMaterials();
        }

        private void FetchMaterials()
        {
            listViewMaterials.ItemsSource = MaterialSystem.GetMaterials();
        }

        private void listViewMaterials_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listViewMaterials.SelectedItem is Material selectedMaterial)
            {
                nameTextBox.Text = selectedMaterial.name;
                descriptionTextBox.Text = selectedMaterial.description;
            }
            else
            {
                nameTextBox.Text = "";
                descriptionTextBox.Text = "";
            }
        }
    }
}