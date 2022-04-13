using System;
using System.Collections.Generic;

using PropertyChanged;

#nullable disable

namespace PlenkaAPI.Models
{
    [AddINotifyPropertyChangedInterface]
    public partial class Value 
    {
        public long MatId { get; set; }
        public long PropId { get; set; }
        public double? Value1 { get; set; }

        public virtual Material Mat { get; set; }
        public virtual Property Prop { get; set; }
    }
}
