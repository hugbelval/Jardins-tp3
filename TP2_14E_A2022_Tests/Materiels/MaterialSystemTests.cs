using Moq;
using TP2_14E_A2022.Data;
using TP2_14E_A2022.DataModels;
using TP2_14E_A2022.Materials;

namespace TP2_14E_A2022_Tests.Materials
{
    [TestClass]
    public class MaterialSystemTests
    {
        [TestMethod]
        public void GivenDbContainsMaterials_WhenGettingMaterials_ThenReturnsMaterialList()
        {
            Mock<IMaterialDAL> DALmock = new Mock<IMaterialDAL>();
            List<Material> generatedMaterials = new List<Material>();
            for (int i = 0; i < 3; i++)
            {
                generatedMaterials.Add(MaterialGenerator.GenerateMaterial());
            }
            DALmock.Setup(x => x.GetMaterials()).Returns(generatedMaterials);
            MaterialSystem.materialDal = DALmock.Object;

            List<Material> result = MaterialSystem.GetMaterials();
            
            Assert.AreEqual(generatedMaterials, result);
        }

        [TestMethod]
        public void GivenDbHasNoMaterials_WhenGettingMaterials_ThenReturnsEmptyList()
        {
            Mock<IMaterialDAL> DALmock = new Mock<IMaterialDAL>();
            List<Material> emptyList = new List<Material>();
            DALmock.Setup(x => x.GetMaterials()).Returns(emptyList);
            MaterialSystem.materialDal = DALmock.Object;

            List<Material> result = MaterialSystem.GetMaterials();

            Assert.AreEqual(emptyList, result);
        }

        [TestMethod]
        public void GivenMaterialInfo_WhenAddingMaterial_ThenReturnsMaterial()
        {
            Mock<IMaterialDAL> DALmock = new Mock<IMaterialDAL>();
            Material generatedMaterial = MaterialGenerator.GenerateMaterial();
            DALmock.Setup(x => x.AddMaterial(It.IsAny<Material>())).Returns(generatedMaterial);
            MaterialSystem.materialDal = DALmock.Object;

            Material? result = MaterialSystem.AddMaterial(generatedMaterial.name, generatedMaterial.description);

            Assert.AreEqual(generatedMaterial, result);
        }

        [TestMethod]
        public void GivenInvalidMaterialInfo_WhenAddingMaterial_ThenReturnsNull()
        {
            Mock<IMaterialDAL> DALmock = new Mock<IMaterialDAL>();
            Material generatedMaterial = MaterialGenerator.GenerateInvalidMaterial();
            DALmock.Setup(x => x.AddMaterial(It.IsAny<Material>())).Returns((Material)null);
            MaterialSystem.materialDal = DALmock.Object;

            Material? result = MaterialSystem.AddMaterial(generatedMaterial.name, generatedMaterial.description);
            
            Assert.AreEqual(null, result);
        }


        [TestMethod]
        public void GivenMaterialInfo_WhenUpdatingMaterial_ThenReturnsMaterial()
        {
            Mock<IMaterialDAL> DALmock = new Mock<IMaterialDAL>();
            Material generatedMaterial = MaterialGenerator.GenerateMaterial();
            DALmock.Setup(x => x.UpdateMaterial(It.IsAny<Material>())).Returns(generatedMaterial);
            MaterialSystem.materialDal = DALmock.Object;

            Material? result = MaterialSystem.UpdateMaterial(generatedMaterial);

            Assert.AreEqual(generatedMaterial, result);
        }

        [TestMethod]
        public void GivenInvalidMaterialInfo_WhenUpdatingMaterial_ThenReturnsNull()
        {
            Mock<IMaterialDAL> DALmock = new Mock<IMaterialDAL>();
            Material generatedMaterial = MaterialGenerator.GenerateInvalidMaterial();
            DALmock.Setup(x => x.UpdateMaterial(It.IsAny<Material>())).Returns((Material)null);
            MaterialSystem.materialDal = DALmock.Object;

            Material? result = MaterialSystem.UpdateMaterial(generatedMaterial);

            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void GivenMaterialInfo_WhenDeletingMaterial_ThenReturnsTrue()
        {
            Mock<IMaterialDAL> DALmock = new Mock<IMaterialDAL>();
            Material generatedMaterial = MaterialGenerator.GenerateMaterial();
            DALmock.Setup(x => x.DeleteMaterial(It.IsAny<Material>())).Returns(true);
            MaterialSystem.materialDal = DALmock.Object;

            bool result = MaterialSystem.DeleteMaterial(generatedMaterial);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void GivenInvalidMaterialInfo_WhenDeletingMaterial_ThenReturnsFalse()
        {
            Mock<IMaterialDAL> DALmock = new Mock<IMaterialDAL>();
            Material generatedMaterial = MaterialGenerator.GenerateInvalidMaterial();
            DALmock.Setup(x => x.DeleteMaterial(It.IsAny<Material>())).Returns(false);
            MaterialSystem.materialDal = DALmock.Object;

            bool result = MaterialSystem.DeleteMaterial(generatedMaterial);

            Assert.AreEqual(false, result);
        }
    }
}
