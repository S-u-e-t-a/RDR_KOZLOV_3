using System;
using System.Collections.Generic;

using PropertyChanged;

#nullable disable

namespace PlenkaAPI.Models
{
    [AddINotifyPropertyChangedInterface]
    public partial class UserType 
    {
        public UserType()
        {
            Users = new HashSet<User>();
        }

        public long UserTypeId { get; set; }
        public long UserTypeName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
