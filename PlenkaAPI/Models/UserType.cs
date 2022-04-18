using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PropertyChanged;

namespace PlenkaAPI.Models
{
    [AddINotifyPropertyChangedInterface]
    public partial class UserType
    {
        public UserType()
        {
            Users = new ObservableCollection<User>();
        }

        public long UserTypeId { get; set; }
        public long UserTypeName { get; set; }

        public virtual ObservableCollection<User> Users { get; set; }
    }
}