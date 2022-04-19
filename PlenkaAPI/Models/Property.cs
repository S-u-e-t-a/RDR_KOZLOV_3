using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PropertyChanged;

namespace PlenkaAPI.Models
{
    [AddINotifyPropertyChangedInterface]
    public partial class Property
    {
        public Property()
        {
            Values = new ObservableCollection<Value>();
        }

        public long ProperrtyId { get; set; }
        public string PropertyName { get; set; }
        public long UnitId { get; set; }

        public virtual Unit Unit { get; set; }
        public virtual ObservableCollection<Value> Values { get; set; }
    }
}
