using System.Collections.ObjectModel;
using PlenkaAPI.Models;
using PlenkaWpf.Utils;
using PlenkaWpf.View;

namespace PlenkaWpf.VM;

public class MaterialEditVM : ViewModelBase
{
    #region Functions

    #region Constructors

    public MaterialEditVM(MembraneObject material)
    {
        Material = material;
        values = Material.Values;
    }

    #endregion

    #endregion

    #region Properties

    public ObservableCollection<Value> values { get; set; }
    public MembraneObject Material { get; set; }

    #endregion


    #region Commands

    private RelayCommand _openSelectPropertyesToChange;

    public RelayCommand OpenSelectPropertyesToChange
    {
        get
        {
            return _openSelectPropertyesToChange ?? (_openSelectPropertyesToChange =
                new RelayCommand(o => { ShowChildWindow(new SelectProperties(Material)); }));
        }
    }

    #endregion
}