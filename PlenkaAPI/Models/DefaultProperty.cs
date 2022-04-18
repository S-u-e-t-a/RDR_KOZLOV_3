using System;
using System.Collections.Generic;
using PropertyChanged;

namespace PlenkaAPI.Models
{
    [AddINotifyPropertyChangedInterface]
    public partial class DefaultProperty
    {
        public long TypeId { get; set; }
        public long PropId { get; set; }

        public virtual Property Prop { get; set; }
        public virtual ObjectType Type { get; set; }
    }
}