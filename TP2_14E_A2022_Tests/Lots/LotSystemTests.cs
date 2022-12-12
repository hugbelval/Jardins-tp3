using MongoDB.Bson;
using Moq;
using TP2_14E_A2022.Data;
using TP2_14E_A2022.DataModels;
using TP2_14E_A2022.Lots;

namespace TP2_14E_A2022_Tests.Lots
{
    [TestClass]
    public class LotSystemTests
    {
        [TestMethod]
        public void GivenDbContainsLots_WhenGettingLots_ThenReturnsLotList()
        {
            Mock<ILotDAL> DALmock = new Mock<ILotDAL>();
            List<Lot> generatedLots = LotGenerator.GenerateNormalLotList();
            DALmock.Setup(x => x.GetLots()).Returns(generatedLots);
            LotSystem.lotDal = DALmock.Object;

            List<Lot> result = LotSystem.GetLots();
            
            Assert.AreEqual(generatedLots, result);
        }

        [TestMethod]
        public void GivenDbHasNoLots_WhenGettingLots_ThenReturnsEmptyList()
        {
            Mock<ILotDAL> DALmock = new Mock<ILotDAL>();
            List<Lot> emptyList = new List<Lot>();
            DALmock.Setup(x => x.GetLots()).Returns(emptyList);
            LotSystem.lotDal = DALmock.Object;

            List<Lot> result = LotSystem.GetLots();

            Assert.AreEqual(emptyList, result);
        }

        [TestMethod]
        public void GivenNewOwner_WhenUpdatingLotOwner_ThenReturnsUpdatedLot()
        {
            ObjectId newOwnerId = ObjectId.GenerateNewId();
            Lot generatedLot = LotGenerator.GenerateLot();
            Lot expectedLot = LotGenerator.GenerateLot();
            expectedLot.ownerId = newOwnerId;
            expectedLot.state = LotState.Active;
            SetupUpdateMockDAL(generatedLot, expectedLot);

            Lot? result = LotSystem.UpdateLotOwner(generatedLot.lotNumber, newOwnerId);

            Assert.AreEqual(expectedLot, result);
        }

        [TestMethod]
        public void GivenLotNumber_WhenWinteringLot_ThenReturnsWinteredLot()
        {
            Lot generatedLot = LotGenerator.GenerateLot();
            generatedLot.ownerId = ObjectId.GenerateNewId();
            Lot expectedLot = LotGenerator.GenerateLot();
            expectedLot.state = LotState.Wintered;
            SetupUpdateMockDAL(generatedLot, expectedLot);

            Lot? result = LotSystem.WinterLot(generatedLot.lotNumber);

            Assert.AreEqual(expectedLot, result);
        }

        [TestMethod]
        public void GivenLotNumber_WhenDeActivatingLot_ThenReturnsInactiveLot()
        {
            Lot generatedLot = LotGenerator.GenerateLot();
            generatedLot.ownerId = ObjectId.GenerateNewId();
            Lot expectedLot = LotGenerator.GenerateLot();
            expectedLot.state = LotState.Inactive;
            SetupUpdateMockDAL(generatedLot, expectedLot);

            Lot? result = LotSystem.DeActivateLot(generatedLot.lotNumber);

            Assert.AreEqual(expectedLot, result);
        }

        private void SetupUpdateMockDAL(Lot generatedLot, Lot expectedLot)
        {
            Mock<ILotDAL> DALmock = new Mock<ILotDAL>();

            DALmock.Setup(x => x.GetLot(It.Is<int>(
                l => l == generatedLot.lotNumber
                ))).Returns(generatedLot);

            DALmock.Setup(x => x.UpdateLot(It.Is<Lot>(
                l => l.Equals(expectedLot)
                ))).Returns(expectedLot);

            LotSystem.lotDal = DALmock.Object;
        }

        [TestMethod]
        public void GivenNormalLots_WhenGettingNumberOfSoilBagsNeeded_ThenReturnsAccurateNumber()
        {
            Mock<ILotDAL> DALmock = new Mock<ILotDAL>();
            List<Lot> generatedLots = LotGenerator.GenerateNormalLotList();
            DALmock.Setup(x => x.GetActiveLots()).Returns(generatedLots);
            LotSystem.lotDal = DALmock.Object;

            int result = LotSystem.GetNumberOfSoilBagsNeeded();

            Assert.AreEqual(LotGenerator.numberOfSoilBagsNeeded, result);
        }

        [TestMethod]
        public void GivenBigLots_WhenGettingNumberOfSoilBagsNeeded_ThenReturnsAccurateNumber()
        {
            Mock<ILotDAL> DALmock = new Mock<ILotDAL>();
            List<Lot> generatedLots = LotGenerator.GenerateBigLotList();
            DALmock.Setup(x => x.GetActiveLots()).Returns(generatedLots);
            LotSystem.lotDal = DALmock.Object;

            int result = LotSystem.GetNumberOfSoilBagsNeeded();

            Assert.AreEqual(LotGenerator.numberOfSoilBagsNeeded, result);
        }
    }
}
