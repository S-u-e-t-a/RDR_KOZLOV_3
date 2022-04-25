using System.Collections.ObjectModel;

using PlenkaAPI.Data;
using PlenkaAPI.Models;

using PlenkaWpf.Utils;
using PlenkaWpf.View;


namespace PlenkaWpf.VM
{
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

        /// <summary>
        ///     Команад, открывающая окно со свойствами для редактирования
        /// </summary>
        public RelayCommand OpenSelectPropertyesToChange
        {
            get
            {
                return _openSelectPropertyesToChange ?? (_openSelectPropertyesToChange =
                                                             new RelayCommand(o =>
                                                             {
                                                                 ShowChildWindow(new SelectProperties(Material));
                                                             }));
            }
        }

        private RelayCommand _saveChanges;

        /// <summary>
        ///     Команда, сохраняющая резульаьы редактирования в базу данных
        /// </summary>
        public RelayCommand SaveChanges
        {
            get
            {
                return _saveChanges ?? (_saveChanges = new RelayCommand(o =>
                                           {
                                               DbContextSingleton.GetInstance().SaveChanges();
                                           }));
            }
        }

    #endregion
    }
}
