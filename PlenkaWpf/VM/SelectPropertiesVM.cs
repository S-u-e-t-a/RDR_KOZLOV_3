using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

using PlenkaAPI.Data;
using PlenkaAPI.Models;

using PlenkaWpf.Annotations;

namespace PlenkaWpf.VM
{
    public class SelectPropertiesVM : INotifyPropertyChanged
    {
        public List<Property> AvailableProperties { get; set; }

        public List<Property> SelectedProperties { get; set; }


        public SelectPropertiesVM(Material material)
        {
            var db = DbContextSingleton.GetInstance();
            AvailableProperties = db.Properties.ToList();
            var materialProperties = material.Values.Select(v => v.Prop);
            AvailableProperties = AvailableProperties.Except(materialProperties).ToList();
        }


        public event PropertyChangedEventHandler PropertyChanged;


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}