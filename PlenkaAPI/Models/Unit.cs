using System;
using System.Collections.Generic;

using PropertyChanged;

#nullable disable

namespace PlenkaAPI.Models
{
    [AddINotifyPropertyChangedInterface]
    public partial class Unit 
    {
        public Unit()
        {
            Properties = new HashSet<Property>();
        }

        public long UnitId { get; set; }
        public string UnitName { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}
