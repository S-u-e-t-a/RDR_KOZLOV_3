using System.Collections.Generic;
using System.Linq;
using PlenkaAPI.Data;
using PlenkaAPI.Models;
using PlenkaWpf.Utils;
using PlenkaWpf.View;

namespace PlenkaWpf.VM;

public class MaterialExplorerVM : ViewModelBase
{
    #region Functions

    #region Constructors

    public MaterialExplorerVM()
    {
        var con = DbContextSingleton.GetInstance();
        Materials = con.Materials.ToList();
    }

    #endregion

    #endregion

    #region Properties

    public List<Material> Materials { get; set; }
    public Material SelectedMaterial { get; set; }

    #endregion


    #region Commands

    private RelayCommand _addNewMaterial;

    public RelayCommand AddNewMaterial
    {
        get
        {
            return _addNewMaterial ??= new RelayCommand(o => { ShowChildWindow(new MaterialEdit(new Material())); });
        }
    }

    private RelayCommand _editMaterial;

    public RelayCommand EditMaterial
    {
        get
        {
            return _editMaterial ??= new RelayCommand(o => { ShowChildWindow(new MaterialEdit(SelectedMaterial)); },
                c => SelectedMaterial != null
            );
        }
    }

    #endregion
}