using System.Windows;
using PlenkaAPI.Models;

namespace PlenkaWpf.View;

/// <summary>
///     Interaction logic for CreatePropertyWindow.xaml
/// </summary>
public partial class CreatePropertyWindow : Window
{
    public CreatePropertyWindow(Property property)
    {
        InitializeComponent();
    }
}