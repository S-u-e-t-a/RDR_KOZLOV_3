using System.Windows.Controls;

using PlenkaWpf.VM;


namespace PlenkaWpf.View
{
    /// <summary>
    ///     Interaction logic for ObjectProperties.xaml
    /// </summary>
    public partial class ObjectProperties : UserControl
    {
        public ObjectProperties()
        {
            InitializeComponent();
            var vm = new ObjectPropertiesVM();
            DataContext = vm;
        }
    }
}
