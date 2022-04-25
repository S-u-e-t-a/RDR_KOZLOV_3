using PropertyChanged;


namespace PlenkaAPI.Models
{
    [AddINotifyPropertyChangedInterface]
    public class DefaultProperty
    {
        public long TypeId { get; set; }
        public long PropId { get; set; }

        public virtual Property Prop { get; set; }
        public virtual ObjectType Type { get; set; }
    }
}
