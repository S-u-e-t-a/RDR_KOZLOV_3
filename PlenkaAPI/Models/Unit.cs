using System.Collections.ObjectModel;

using PropertyChanged;


namespace PlenkaAPI.Models
{
    [AddINotifyPropertyChangedInterface]
    public class Unit
    {
        public Unit()
        {
            Properties = new ObservableCollection<Property>();
        }

        public long UnitId { get; set; }
        public string UnitName { get; set; }

        public virtual ObservableCollection<Property> Properties { get; set; }
    }
}
