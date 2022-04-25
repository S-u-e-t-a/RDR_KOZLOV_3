﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

using PlenkaWpf.Annotations;
using PlenkaWpf.Utils;


namespace PlenkaWpf.VM
{
    /// <summary>
    ///     Абстрактный класс для VM
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        private RelayCommand _closeWindow;

        /// <summary>
        ///     Команда, закрывающая текущее окно
        /// </summary>
        public RelayCommand CloseWindow
        {
            get
            {
                return _closeWindow ?? (_closeWindow = new RelayCommand(o =>
                                           {
                                               OnClosingRequest();
                                           }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Команда, открывающая новое окно
        /// </summary>
        /// <param name="window"></param>
        public void ShowChildWindow(Window window)
        {
            window.Show();
        }

        public event EventHandler ClosingRequest;

        /// <summary>
        ///     Функция, закрывающая текущее окно
        /// </summary>
        protected void OnClosingRequest()
        {
            if (ClosingRequest != null)
            {
                ClosingRequest(this, EventArgs.Empty);
            }
        }

        /// <summary>
        ///     Обработчик изменения свойств
        /// </summary>
        /// <param name="propertyName"></param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
