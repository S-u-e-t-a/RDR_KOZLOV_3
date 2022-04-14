using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlenkaAPI.Data;
using PlenkaAPI.Models;
using PlenkaWpf.Utils;

namespace PlenkaWpf.VM
{
    internal class CreateMaterialVM : ViewModelBase

    {
        #region Functions

        #region Constructors



        #endregion

        #endregion

        #region Properties

        public Material Material { get; set; } = new Material(){MateriadName = ""};


        #endregion

        #region Commands

        private RelayCommand _saveMaterial;

        public RelayCommand SaveMaterial
        {
            get { return _saveMaterial ?? (_saveMaterial = new RelayCommand(o =>
            {
                var db = DbContextSingleton.GetInstance();
                db.Materials.Add(Material);
                db.SaveChanges();
                OnClosingRequest();
            },o=>  Material?.MateriadName.Length>0)); }
        }


        #endregion
    }
}
