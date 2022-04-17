using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PropertyChanged;

namespace PlenkaAPI.Models
{
    [AddINotifyPropertyChangedInterface]
    public partial class ObjectType
    {
        public ObjectType()
        {
            MembraneObjects = new ObservableCollection<MembraneObject>();
        }

        public long TypeId { get; set; }
        public string TypeName { get; set; }

        public virtual ObservableCollection<MembraneObject> MembraneObjects { get; set; }
    }
}
