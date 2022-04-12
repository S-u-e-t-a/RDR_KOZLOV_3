using System;
using System.Collections.Generic;

#nullable disable

namespace PlenkaAPI.Models
{
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
