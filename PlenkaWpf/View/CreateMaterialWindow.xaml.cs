using System.Windows;

using PlenkaWpf.VM;


namespace PlenkaWpf.View
{
    /// <summary>
    ///     Логика взаимодействия для CreateMaterialWindow.xaml
    /// </summary>
    public partial class CreateMaterialWindow : Window
    {
        public CreateMaterialWindow()
        {
            InitializeComponent();
            var vm = new CreateMaterialVM();
            DataContext = vm;
            vm.ClosingRequest += (sender, e) => Close();
        }
    }
}
