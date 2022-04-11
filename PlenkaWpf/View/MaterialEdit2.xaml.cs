using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

using PlenkaWpf.Annotations;
//using PlenkaAPI.Models;

namespace PlenkaWpf.View
{
    /// <summary>
    ///     Логика взаимодействия для MaterialEdit2.xaml
    /// </summary>
    public partial class MaterialEdit2 : Window, INotifyPropertyChanged
    {
        //public MaterialEdit2(Material material)
        //{
        //    InitializeComponent();
        //    var values = material.Values;

        //    foreach (var value in values)
        //    {
        //        propertyGrid.PropertyDefinitions.Add(createProperty(value));
        //    }

        //    propertyGrid.SelectedObject = null;
        //}


        public event PropertyChangedEventHandler PropertyChanged;


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        //private PropertyDefinition createProperty(Value value)
        //{
        //    var property = new PropertyDefinition();
        //    property.DisplayName = value.Prop.PropertyName;

        //    return property;
        //}
    }
}