using System.Collections.Generic;
using System.Linq;
using System.Windows;

using HandyControl.Interactivity;

using PlenkaAPI.Data;
using PlenkaAPI.Models;

using PlenkaWpf.Utils;

namespace PlenkaWpf.VM
{
    public class SelectPropertiesVM : ViewModelBase
    {
        #region Properties

        public List<Property> AvailableProperties { get; set; }
        public Material Material { get; set; }

        #endregion

        #region Functions

        #region Constructors

        public SelectPropertiesVM(Material material)
        {
            Material = material;
            var db = DbContextSingleton.GetInstance();
            AvailableProperties = db.Properties.ToList();
            var materialProperties = material.Values.Select(v => v.Prop);
            AvailableProperties = AvailableProperties.Except(materialProperties).ToList();
        }

        #endregion

        #endregion

        #region Commands

        private RelayCommand _selectProperties;

        public RelayCommand SelectProperties
        {
            get
            {
                return _selectProperties ??= new RelayCommand(o =>
                {
                    System.Collections.IList items = (System.Collections.IList)o;
                    var selectedProperties = items.Cast<Property>();
                    //var selectedProperties = new List<Property>();
                    foreach (var property in selectedProperties)
                    {
                        Material.Values.Add(new Value() { Mat = Material, Prop = property });
                    }
                    OnClosingRequest();
                },(o)=> ((System.Collections.IList)o).Count>0);
            }
        }

        private RelayCommand _createProperty;

        public RelayCommand CreateProperty
        {
            get
            {
                return _createProperty ?? (_createProperty = new RelayCommand(o =>
                {
                    
                }));
            }
        }

        #endregion

    }
}