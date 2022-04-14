using System.Collections.ObjectModel;
using PropertyChanged;

#nullable disable

namespace PlenkaAPI.Models;

[AddINotifyPropertyChangedInterface]
public class Material
{
    public Material()
    {
        Values = new ObservableCollection<Value>();
    }

    public long MaterialId { get; set; }
    public string MateriadName { get; set; }

    public virtual ObservableCollection<Value> Values { get; set; }
}