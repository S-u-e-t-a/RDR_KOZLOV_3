using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

using PlenkaAPI.Data;
using PlenkaAPI.Models;

namespace ViewModels
{
    public class MaterialExplorerVM : ViewModelBase
    {
        public List<Material> Materials { get; set; }
        public Material SelectedMaterial { get; set; }
       
        public MaterialExplorerVM()
        {
            var con = DbContextSingleton.GetInstance();
            Materials = con.Materials.ToList();
        }

    }
}