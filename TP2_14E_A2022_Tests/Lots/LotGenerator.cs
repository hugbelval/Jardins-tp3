using TP2_14E_A2022.DataModels;

namespace TP2_14E_A2022_Tests.Lots
{
    public static class LotGenerator
    {
        public static Lot GenerateLot()
        {
            return new Lot(1, null, 10, 3);
        }

        public static List<Lot> GenerateLotList()
        {
            List<Lot> lots = new List<Lot>();
            for (int i = 1; i <= 3; i++)
            {
                lots.Add(new Lot(i, null, 10, 3));
            }
            return lots;
        }
    }
}