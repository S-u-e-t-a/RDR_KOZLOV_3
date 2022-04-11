using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

using PlenkaAPI.Data;
using PlenkaAPI.Models;

using PlenkaWpf.Annotations;

namespace PlenkaWpf.VM
{
    internal class MaterialExplorerVM : INotifyPropertyChanged
    {
        public List<Material> Materials { get; set; }
        public Material SelectedMaterial { get; set; }


        public MaterialExplorerVM()
        {
            var con = DbContextSingleton.GetInstance();
            Materials = con.Materials.ToList();
        }


        public event PropertyChangedEventHandler PropertyChanged;


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}