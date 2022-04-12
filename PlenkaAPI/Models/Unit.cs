using System;
using System.Collections.Generic;

#nullable disable

namespace PlenkaAPI.Models
{
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
