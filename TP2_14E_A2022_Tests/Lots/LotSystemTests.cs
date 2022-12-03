using MongoDB.Bson;
using Moq;
using TP2_14E_A2022.Data;
using TP2_14E_A2022.DataModels;
using TP2_14E_A2022.Lots;
using TP2_14E_A2022_Tests.Materials;

namespace TP2_14E_A2022_Tests.Lots
{
    [TestClass]
    public class LotSystemTests
    {
        [TestMethod]
        public void GivenDbContainsLots_WhenGettingLots_ThenReturnsLotList()
        {
            Mock<ILotDAL> DALmock = new Mock<ILotDAL>();
            List<Lot> generatedLots = LotGenerator.GenerateLotList();
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
            Mock<ILotDAL> DALmock = new Mock<ILotDAL>();
            Lot generatedLot = LotGenerator.GenerateLot();
            Lot expectedLot = generatedLot;
            expectedLot.ownerId = newOwnerId;
            expectedLot.state = LotState.Active;

            DALmock.Setup(x => x.UpdateLot(It.Is<Lot>(
                l => l.Equals(expectedLot)
                ))).Returns(generatedLot);
            LotSystem.lotDal = DALmock.Object;

            Lot? result = LotSystem.UpdateLotOwner(generatedLot.lotNumber, newOwnerId);

            Assert.AreEqual(generatedLot, result);
        }

        [TestMethod]
        public void GivenInvalidLotInfo_WhenUpdatingLot_ThenReturnsNull()
        {
            Mock<ILotDAL> DALmock = new Mock<ILotDAL>();
            Lot generatedLot = LotGenerator.GenerateInvalidLot();
            DALmock.Setup(x => x.UpdateLot(It.IsAny<Lot>())).Returns((Lot)null);
            LotSystem.lotDal = DALmock.Object;

            Lot? result = LotSystem.UpdateLot(generatedLot);

            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void GivenLotInfo_WhenDeletingLot_ThenReturnsTrue()
        {
            Mock<ILotDAL> DALmock = new Mock<ILotDAL>();
            Lot generatedLot = LotGenerator.GenerateLot();
            DALmock.Setup(x => x.DeleteLot(It.IsAny<Lot>())).Returns(true);
            LotSystem.lotDal = DALmock.Object;

            bool result = LotSystem.DeleteLot(generatedLot);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void GivenInvalidLotInfo_WhenDeletingLot_ThenReturnsFalse()
        {
            Mock<ILotDAL> DALmock = new Mock<ILotDAL>();
            Lot generatedLot = LotGenerator.GenerateInvalidLot();
            DALmock.Setup(x => x.DeleteLot(It.IsAny<Lot>())).Returns(false);
            LotSystem.lotDal = DALmock.Object;

            bool result = LotSystem.DeleteLot(generatedLot);

            Assert.AreEqual(false, result);
        }
    }
}
