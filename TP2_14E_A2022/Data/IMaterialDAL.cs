using System.Collections.Generic;
using TP2_14E_A2022.DataModels;

namespace TP2_14E_A2022.Data
{
    public interface IMaterialDAL
    {
        List<Material> GetMaterials();
        Material AddMaterial(Material material);
        Material UpdateMaterial(Material material);
        bool DeleteMaterial(Material materialId);
    }
}