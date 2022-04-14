using System.Collections.ObjectModel;
using PropertyChanged;

#nullable disable

namespace PlenkaAPI.Models;

[AddINotifyPropertyChangedInterface]
public class UserType
{
    public UserType()
    {
        Users = new ObservableCollection<User>();
    }

    public long UserTypeId { get; set; }
    public long UserTypeName { get; set; }

    public virtual ObservableCollection<User> Users { get; set; }
}