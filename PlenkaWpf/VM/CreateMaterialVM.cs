using PlenkaAPI.Data;
using PlenkaAPI.Models;
using PlenkaWpf.Utils;

namespace PlenkaWpf.VM;

internal class CreateMaterialVM : ViewModelBase

{
    #region Properties

    public Material Material { get; set; } = new() {MateriadName = ""};

    #endregion

    #region Functions

    #region Constructors

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
                var db = DbContextSingleton.GetInstance();
                db.Materials.Add(Material);
                db.SaveChanges();
                OnClosingRequest();
            }, o => Material?.MateriadName.Length > 0));
        }
    }

    #endregion
}