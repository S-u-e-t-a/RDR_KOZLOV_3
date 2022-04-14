using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            Values = new ObservableCollection<Value>();
        }

        public long MaterialId { get; set; }
        public string MateriadName { get; set; }

        public virtual ObservableCollection<Value> Values { get; set; }
        

    }
}
