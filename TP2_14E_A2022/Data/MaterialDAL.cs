using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Windows;
using TP2_14E_A2022.DataModels;

namespace TP2_14E_A2022.Data
{
    public class MaterialDAL : DAL, IMaterialDAL
    {
        public List<Material> GetMaterials()
        {
            List<Material> materials = new List<Material>();

            try
            {
                materials = db.GetCollection<Material>("Material").Aggregate().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de se connecter à la base de données " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return materials;
        }

        public Material AddMaterial(Material material)
        {
            try
            {
                IMongoCollection<Material> materials = db.GetCollection<Material>("Material");
                materials.InsertOne(material);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de se connecter à la base de données " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return material;
        }

        public Material UpdateMaterial(Material material)
        {
            try
            {
                IMongoCollection<Material> materials = db.GetCollection<Material>("Material");
                FilterDefinition<Material> filter = Builders<Material>.Filter.Eq("id", material.id);
                UpdateDefinition<Material> update = Builders<Material>.Update.Set("name", material.name)
                                                      .Set("description", material.description);
                materials.UpdateOne(filter, update);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de se connecter à la base de données " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return material;
        }

        public bool DeleteMaterial(Material material)
        {
            try
            {
                IMongoCollection<Material> materials = this.db.GetCollection<Material>("Material");
                FilterDefinition<Material> filter = Builders<Material>.Filter.Eq("id", material.id);
                return materials.DeleteOne(filter).DeletedCount > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de se connecter à la base de données " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
