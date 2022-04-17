using System.Windows;
using PlenkaAPI.Models;
using PlenkaWpf.VM;

namespace PlenkaWpf.View;

/// <summary>
///     Логика взаимодействия для MaterialEdit.xaml
/// </summary>
public partial class MaterialEdit : Window
{
    private readonly MembraneObject material;


    public MaterialEdit(MembraneObject material)
    {
        this.material = material;
        InitializeComponent();
        DataContext = new MaterialEditVM(material);
    }
}