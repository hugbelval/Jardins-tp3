using TP2_14E_A2022.DataModels;

namespace TP2_14E_A2022_Tests.Lots
{
    public static class LotGenerator
    {
        public static int numberOfSoilBagsNeeded;
        private const int numberOfLitersInSoilBag = 50;
        private const float cubicMeterToLiterConversionRate = 1000f;

        private const float normalLotArea = 5;
        private const float normalLotDepth = 1;

        private const float bigLotArea = 15;
        private const float bigLotDepth = 3;


        public static Lot GenerateLot()
        {
            return new Lot(1, null, 10, 3);
        }

        public static List<Lot> GenerateNormalLotList()
        {
            return GenerateLotList(3, normalLotArea, normalLotDepth);
        }

        public static List<Lot> GenerateBigLotList()
        {
            return GenerateLotList(5, bigLotArea, bigLotDepth);
        }

        private static List<Lot> GenerateLotList(int numberOfLots, float lotArea, float lotDepth)
        {
            numberOfSoilBagsNeeded = 0;
            float totalLiters = 0;
            List<Lot> lots = new List<Lot>();
            for (int i = 1; i <= numberOfLots; i++)
            {
                lots.Add(new Lot(i, null, lotArea, lotDepth));
                totalLiters += lotArea * lotDepth * cubicMeterToLiterConversionRate;
            }
            numberOfSoilBagsNeeded = (int)Math.Ceiling(totalLiters / numberOfLitersInSoilBag);
            return lots;
        }
    }
}