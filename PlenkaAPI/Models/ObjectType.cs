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
            Props = new ObservableCollection<Property>();
        }

        public long TypeId { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<MembraneObject> MembraneObjects { get; set; }

        public virtual ObservableCollection<Property> Props { get; set; }
    }
}
