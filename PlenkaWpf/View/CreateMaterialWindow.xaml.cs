﻿using PlenkaWpf.VM;


namespace PlenkaWpf.View
{
    /// <summary>
    ///     Логика взаимодействия для CreateMaterialWindow.xaml
    /// </summary>
    public partial class CreateMaterialWindow
    {
        public CreateMaterialWindow()
        {
            InitializeComponent();
            var vm = new CreateMaterialVm();
            DataContext = vm;
            vm.ClosingRequest += (sender, e) => Close();
        }
    }
}
