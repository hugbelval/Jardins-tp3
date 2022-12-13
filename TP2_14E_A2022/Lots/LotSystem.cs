using MongoDB.Bson;
using System;
using System.Collections.Generic;
using TP2_14E_A2022.Data;
using TP2_14E_A2022.DataModels;

namespace TP2_14E_A2022.Lots
{
    public static class LotSystem
    {
        private const int numberOfLitersInSoilBag = 50;
        public static ILotDAL lotDal;

        static LotSystem()
        {
            lotDal = new LotDAL();
        }

        public static List<Lot> GetLots()
        {
            return lotDal.GetLots();
        }

        public static Lot? GetLot(int lotNumber)
        {
            return lotDal.GetLot(lotNumber);
        }

        public static List<Lot> GetLotsOwnedBy(ObjectId ownerId)
        {
            return lotDal.GetLotsOwnedBy(ownerId);
        }

        public static Lot? UpdateLotOwner(int lotNumber, ObjectId newOwnerId)
        {
            return UpdateState(lotNumber, newOwnerId, LotState.Active);
        }

        public static Lot? WinterLot(int lotNumber)
        {
            return UpdateState(lotNumber, null, LotState.Wintered);
        }

        public static Lot? DeActivateLot(int lotNumber)
        {
            return UpdateState(lotNumber, null, LotState.Inactive);
        }

        private static Lot? UpdateState(int lotNumber, ObjectId? newOwner, LotState newState)
        {
            Lot? lot = lotDal.GetLot(lotNumber);
            if (lot != null)
            {
                lot.ownerId = newOwner;
                lot.state = newState;
                return lotDal.UpdateLot(lot);
            }
            return null;
        }

        public static int GetNumberOfSoilBagsNeeded()
        {
            List<Lot> activeLots = lotDal.GetActiveLots();
            float totalLiters = 0;
            foreach (Lot activeLot in activeLots)
            {
                totalLiters += activeLot.GetVolumeInLiters();
            }
            return (int)Math.Ceiling(totalLiters / numberOfLitersInSoilBag);
        }
    }
}
