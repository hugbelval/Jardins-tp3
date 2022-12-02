using System.Collections.Generic;
using TP2_14E_A2022.DataModels;

namespace TP2_14E_A2022.Data
{
    public interface ILotDAL
    {
        List<Lot> GetLots();
        Lot? GetLot(int lotNumber);
        Lot AddLot(Lot lot);
        Lot UpdateLot(Lot lot);
        bool DeleteLot(Lot lot);
    }
}