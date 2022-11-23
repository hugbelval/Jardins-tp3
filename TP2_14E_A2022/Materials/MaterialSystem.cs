using System.Collections.Generic;
using TP2_14E_A2022.Data;
using TP2_14E_A2022.DataModels;

namespace TP2_14E_A2022.Materials
{
    public static class MaterialSystem
    {
        public static IMaterialDAL materialDal;

        static MaterialSystem()
        {
            materialDal = new MaterialDAL();
        }

        public static Material? AddMaterial(string name, string description)
        {
            if(name == string.Empty)
            {
                return null;
            }
            else
            {
                Material material = new Material(name, description);
                return materialDal.AddMaterial(material);
            }
        }

        public static List<Material> GetMaterials()
        {
            return materialDal.GetMaterials();
        }

        public static Material? UpdateMaterial(Material material)
        {
            if(material.name == string.Empty)
            {
                return null;
            }
            return materialDal.UpdateMaterial(material);
        }

        public static bool DeleteMaterial(object material)
        {
            if (material is Material materialToDelete)
            {
               return materialDal.DeleteMaterial(materialToDelete);
            }
            return false;
        }
    }
}
