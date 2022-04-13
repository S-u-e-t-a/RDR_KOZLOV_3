using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

using PlenkaAPI.Models;

using PlenkaWpf.VM;

namespace PlenkaWpf.View
{
    /// <summary>
    ///     Логика взаимодействия для MaterialEdit3.xaml
    /// </summary>
    public partial class MaterialEdit3 : Window
    {
        private readonly Material material;


        public MaterialEdit3(Material material)
        {
            this.material = material;
            InitializeComponent();
            DataContext = new MaterialEdit3VM(material);
        }

        
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new SelectProperties(material);
            win.ShowDialog();
        }


        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new SelectProperties(material);
            win.ShowDialog();
        }
    }
}