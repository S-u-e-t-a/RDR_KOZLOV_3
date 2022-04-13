using System;
using System.Collections.Generic;

using PropertyChanged;

#nullable disable

namespace PlenkaAPI.Models
{
    [AddINotifyPropertyChangedInterface]
    public partial class User
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public long UserTypeId { get; set; }

        public virtual UserType UserType { get; set; }
    }
}
