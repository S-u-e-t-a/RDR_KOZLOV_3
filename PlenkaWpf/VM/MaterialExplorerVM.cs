using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
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
        con.SavedChanges += (sender, args) => { OnPropertyChanged(nameof(Materials)); };
        //var con = DbContextSingleton.GetInstance();
        Materials = con.MembraneObjects.Local.ToObservableCollection();
    }

    #endregion

    #endregion

    #region Properties

    private MembraneContext con = DbContextSingleton.GetInstance();
    public ObservableCollection<MembraneObject> Materials { get; set; }

    public MembraneObject SelectedMemObject { get; set; }

    #endregion


    #region Commands

    private RelayCommand _addNewMemObject;

    public RelayCommand AddNewMemObject
    {
        get { return _addNewMemObject ??= new RelayCommand(o => { ShowChildWindow(new CreateMaterialWindow()); }); }
    }

    private RelayCommand _editMemObject;

    public RelayCommand EditMemObject
    {
        get
        {
            return _editMemObject ??= new RelayCommand(o =>
                {
                    ShowChildWindow(new MaterialEdit(SelectedMemObject));
                },
                c => SelectedMemObject != null
            );
        }
    }

    private RelayCommand _deleteMemObject;

    public RelayCommand DeleteMemObject
    {
        get { return _deleteMemObject ?? (_deleteMemObject = new RelayCommand(o =>
        {
            
        },c=> SelectedMemObject != null)); }
    }


    #endregion
}