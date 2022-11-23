using MongoDB.Driver;
using System;
using System.Windows;

namespace TP2_14E_A2022.Data
{
    public class DAL
    {
        protected IMongoDatabase db;
        public DAL()
        {
            db = OpenConnection();
        }
        
        protected IMongoDatabase OpenConnection()
        {
            try
            {
                MongoClient dbClient = new MongoClient("mongodb://localhost:27017");
                return dbClient.GetDatabase("TP2DB");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de se connecter à la base de données " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }
    }
}
