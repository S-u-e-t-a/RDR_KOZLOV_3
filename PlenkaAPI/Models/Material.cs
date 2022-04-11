using System;
using System.Collections.Generic;

#nullable disable

namespace PlenkaAPI.Models
{
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
