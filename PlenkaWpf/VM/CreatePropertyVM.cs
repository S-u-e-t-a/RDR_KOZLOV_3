using System.Collections.Generic;
using System.Linq;

using PlenkaAPI.Data;
using PlenkaAPI.Models;

using PlenkaWpf.Utils;

namespace PlenkaWpf.VM
{
    public class CreatePropertyVM: ViewModelBase
    {
        public Property Property { get; set; }

        public List<Unit> Units
        {
            get
            {
                return DbContextSingleton.GetInstance().Units.ToList();
            } 
        }


        public CreatePropertyVM(Property property)
        {
            Property = property;
        }

        private RelayCommand _saveProperty;



        public RelayCommand SaveProperty
        {
            get
            {
                return _saveProperty ?? (_saveProperty = new RelayCommand(o =>
                {
                    
                }));
            }
        }

    }
}
