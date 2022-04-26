using System.Windows;

using PlenkaAPI.Models;

using PlenkaWpf.VM;


namespace PlenkaWpf.View
{
    /// <summary>
    ///     Interaction logic for SelectProperties.xaml
    /// </summary>
    public partial class SelectProperties : Window
    {
        public SelectProperties(MembraneObject material)
        {
            InitializeComponent();
            var vm = new SelectPropertiesVm(material);
            DataContext = vm;
            vm.ClosingRequest += (sender, e) => Close();
        }
    }
}
