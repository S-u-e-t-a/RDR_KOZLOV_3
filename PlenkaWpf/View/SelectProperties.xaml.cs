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
        public SelectProperties(Material material)
        {
            InitializeComponent();
            DataContext = new SelectPropertiesVM(material);
        }
    }
}