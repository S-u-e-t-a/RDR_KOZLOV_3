using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PlenkaAPI.Models
{
    public partial class MembraneObject
    {
        public double? this[string propname]
        {
            get
            {
                return this.Values.First(v => v.Prop.PropertyName == propname).Value1;
            }
            set
            {
                this.Values.First(v => v.Prop.PropertyName == propname).Value1=value;
            }
        }
    }
}
