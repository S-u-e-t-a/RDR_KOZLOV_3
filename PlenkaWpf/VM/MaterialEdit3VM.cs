using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using PlenkaAPI.Models;

namespace PlenkaWpf.VM
{
    internal class MaterialEdit3VM : INotifyPropertyChanged
    {
        public ObservableCollection<Value> values { get; set; }


        public MaterialEdit3VM(Material material)
        {
            values = new ObservableCollection<Value>(material.Values);
        }


        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}