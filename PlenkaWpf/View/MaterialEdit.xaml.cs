using System.Windows;
using PlenkaAPI.Models;
using PlenkaWpf.VM;

namespace PlenkaWpf.View;

/// <summary>
///     Логика взаимодействия для MaterialEdit.xaml
/// </summary>
public partial class MaterialEdit : Window
{


    public MaterialEdit(MembraneObject material)
    {
        InitializeComponent();
        var vm = new MaterialEditVM(material);
        DataContext = vm;
        vm.ClosingRequest += (sender, e) => Close();
    }
}