using System.Collections.Generic;
using System.Linq;
using PlenkaAPI.Data;
using PlenkaAPI.Models;
using PlenkaWpf.Utils;

namespace PlenkaWpf.VM;

internal class CreateMaterialVM : ViewModelBase

{
    #region Properties

    public MembraneObject Material { get; set; } = new() {ObName = ""};
    public List<ObjectType> AllTypes { get; set; }

    #endregion

    #region Functions

    #region Constructors

    public CreateMaterialVM()
    {
        AllTypes = DbContextSingleton.GetInstance().ObjectTypes.ToList();
    }
    #endregion

    #endregion

    #region Commands

    private RelayCommand _saveMaterial;

    public RelayCommand SaveMaterial
    {
        get
        {
            return _saveMaterial ?? (_saveMaterial = new RelayCommand(o =>
            {
                Material.TypeId = Material.Type.TypeId;
                var db = DbContextSingleton.GetInstance();
                db.MembraneObjects.Add(Material);
                db.SaveChanges();
                OnClosingRequest();
            }, o => Material?.ObName.Length > 0));
        }
    }

    #endregion
}