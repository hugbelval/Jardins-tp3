using TP2_14E_A2022.DataModels;

namespace TP2_14E_A2022_Tests.Materials
{
    public static class MaterialGenerator
    {
        public static Material GenerateMaterial()
        {
            return new Material("nom", "description");
        }

        public static Material GenerateInvalidMaterial()
        {
            return new Material("", "description");
        }
    }
}