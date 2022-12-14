using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Windows;
using TP2_14E_A2022.DataModels;
using TP2_14E_A2022.Lots;

namespace TP2_14E_A2022.Data
{
    public class LotDAL : DAL, ILotDAL
    {
        public List<Lot> GetLots()
        {
            List<Lot> lots = new List<Lot>();

            try
            {
                lots = db.GetCollection<Lot>("Lots").Aggregate().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de se connecter à la base de données " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return lots;
        }

        public List<Lot> GetActiveLots()
        {
            List<Lot> lots = new List<Lot>();

            try
            {
                lots = db.GetCollection<Lot>("Lots").Find(x => x.state == LotState.Active).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de se connecter à la base de données " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return lots;
        }

        public List<Lot> GetLotsOwnedBy(ObjectId ownerId)
        {
            List<Lot> lots = new List<Lot>();

            try
            {
                lots = db.GetCollection<Lot>("Lots").Find(x => x.ownerId == ownerId).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de se connecter à la base de données " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return lots;
        }

        public Lot? GetLot(int lotNumber)
        {
            return db.GetCollection<Lot>("Lots").Find(x => x.lotNumber == lotNumber).SingleOrDefaultAsync<Lot>().Result;
        }

        public Lot AddLot(Lot lot)
        {
            try
            {
                IMongoCollection<Lot> lots = db.GetCollection<Lot>("Lots");
                lots.InsertOne(lot);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de se connecter à la base de données " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return lot;
        }

        public Lot UpdateLot(Lot lot)
        {
            try
            {
                IMongoCollection<Lot> lots = db.GetCollection<Lot>("Lots");
                FilterDefinition<Lot> filter = Builders<Lot>.Filter.Eq("lotNumber", lot.lotNumber);
                UpdateDefinition<Lot> update = Builders<Lot>.Update.Set("ownerId", lot.ownerId)
                                                      .Set("state", lot.state)
                                                      .Set("areaInSquareMeters", lot.areaInSquareMeters)
                                                      .Set("depthInMeters", lot.depthInMeters);
                lots.UpdateOne(filter, update);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de se connecter à la base de données " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return lot;
        }
    }
}
