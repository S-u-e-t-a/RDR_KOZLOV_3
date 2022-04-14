using System.Windows;
using PlenkaAPI.Data;

namespace PlenkaWpf.View;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
    }


    private void Button_Click(object sender, RoutedEventArgs e) //todo убрать заглушку
    {
        var con = DbContextSingleton.GetInstance();
        //var mat = con.Materials.Local.First();
        //var win = new MaterialEdit3(mat);
        var win = new MaterialExplorer();
        win.Show();
    }
}