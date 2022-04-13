using System.Collections.ObjectModel;

using PlenkaAPI.Models;

using PlenkaWpf.Utils;

namespace PlenkaWpf.VM
{
    public class MaterialEdit3VM : ViewModelBase
    {
        #region Properties

        public ObservableCollection<Value> values { get; set; }
        public Material Material { get; set; }

        #endregion


        #region Functions

        #region Constructors

        public MaterialEdit3VM(Material material)
        {
            Material = material;
            values = new ObservableCollection<Value>(material.Values);
        }

        #endregion

        #endregion


        #region Commands

        private RelayCommand _addProperty;

        public RelayCommand AddProperty
        {
            get
            {
                return _addProperty ?? (_addProperty = new RelayCommand(o =>
                {
            
                }));
            }
        }


        #endregion
        

        

    }
}