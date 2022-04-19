using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlenkaAPI.Data;
using PlenkaAPI.Models;

namespace PlenkaWpf.VM
{
    internal class UserEditVM : ViewModelBase

    {
        #region Functions

        #region Constructors

        public UserEditVM(User user)
        {
            User = user;
            UserTypes = DbContextSingleton.GetInstance().UserTypes.Local.ToObservableCollection();
        }

        #endregion

        #endregion

        #region Properties

        public ObservableCollection<UserType> UserTypes { get; set; }
        public User User { get; set; }

        #endregion

        #region Commands



        #endregion
    }
}
