using System.Collections.ObjectModel;
using PlenkaAPI.Models;
using PlenkaWpf.Utils;

namespace PlenkaWpf.VM;

public class MaterialEdit3VM : ViewModelBase
{
    #region Functions

    #region Constructors

    public MaterialEdit3VM(Material material)
    {
        Material = material;
        values = Material.Values;
    }

    #endregion

    #endregion

    #region Properties

    public ObservableCollection<Value> values { get; set; }
    public Material Material { get; set; }

    #endregion


    #region Commands

    private RelayCommand _addProperty;

    public RelayCommand AddProperty
    {
        get { return _addProperty ?? (_addProperty = new RelayCommand(o => { })); }
    }

    private RelayCommand _openEditMaterialToEdit;

    public RelayCommand OpenEditMaterialToEdit
    {
        get { return _openEditMaterialToEdit ?? (_openEditMaterialToEdit = new RelayCommand(o => { })); }
    }

    #endregion
}