using System.Collections.Generic;
using System.Linq;
using PlenkaAPI.Data;
using PlenkaAPI.Models;
using PlenkaWpf.Utils;

namespace PlenkaWpf.VM;

public class CreatePropertyVM : ViewModelBase
{
    private RelayCommand _saveProperty;


    public CreatePropertyVM(Property property)
    {
        Property = property;
    }

    public Property Property { get; set; }

    public List<Unit> Units => DbContextSingleton.GetInstance().Units.ToList();


    public RelayCommand SaveProperty
    {
        get { return _saveProperty ?? (_saveProperty = new RelayCommand(o => { })); }
    }
}