using System.Windows;

using PlenkaAPI.Models;

using PlenkaWpf.VM;


namespace PlenkaWpf.View
{
    /// <summary>
    ///     Interaction logic for SelectProperties.xaml
    /// </summary>
    public partial class SelectProperties
    {
        public SelectProperties(ObjectType ot)
        {
            InitializeComponent();
            var vm = new SelectPropertiesVm(ot);
            DataContext = vm;
            vm.ClosingRequest += (sender, e) => Close();
        }
    }
}
