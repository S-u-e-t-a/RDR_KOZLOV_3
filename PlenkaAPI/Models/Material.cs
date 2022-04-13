using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using JetBrains.Annotations;

using PropertyChanged;

#nullable disable

namespace PlenkaAPI.Models
{
    
    [AddINotifyPropertyChangedInterface]
    public partial class Material
    {
        public Material()
        {
            Values = new HashSet<Value>();
        }

        public long MaterialId { get; set; }
        public string MateriadName { get; set; }

        public virtual ICollection<Value> Values { get; set; }
        

    }
}
