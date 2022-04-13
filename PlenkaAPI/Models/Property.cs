using System;
using System.Collections.Generic;

using PropertyChanged;

#nullable disable

namespace PlenkaAPI.Models
{
    [AddINotifyPropertyChangedInterface]
    public partial class Property 
    {
        public Property()
        {
            Values = new HashSet<Value>();
        }

        public long ProperrtyId { get; set; }
        public string PropertyName { get; set; }
        public long UnitId { get; set; }

        public virtual Unit Unit { get; set; }
        public virtual ICollection<Value> Values { get; set; }
    }
}
