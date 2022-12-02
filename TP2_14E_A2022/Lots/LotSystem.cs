using MongoDB.Bson;
using System.Collections.Generic;
using TP2_14E_A2022.Data;
using TP2_14E_A2022.DataModels;

namespace TP2_14E_A2022.Lots
{
    public static class LotSystem
    {
        public static ILotDAL lotDal;

        static LotSystem()
        {
            lotDal = new LotDAL();
        }

        public static List<Lot> GetLots()
        {
            return lotDal.GetLots();
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

        public static bool DeleteLot(object lot)
        {
            if (lot is Lot lotToDelete)
            {
               return lotDal.DeleteLot(lotToDelete);
            }
            return false;
        }
    }
}
