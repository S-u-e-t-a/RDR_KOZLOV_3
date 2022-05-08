using PlenkaAPI.Models;

using PlenkaWpf.VM;


namespace PlenkaWpf.View
{
    /// <summary>
    ///     Interaction logic for CreatePropertyWindow.xaml
    /// </summary>
    public partial class CreatePropertyWindow
    {
        public CreatePropertyWindow(Property property)
        {
            InitializeComponent();
            var vm = new CreatePropertyVm(property);
            DataContext = vm;
            vm.ClosingRequest += (sender, e) => Close();
        }
    }
}
