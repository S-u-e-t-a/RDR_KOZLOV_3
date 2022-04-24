using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using PlenkaAPI.Data;
using PlenkaAPI.Models;
using PlenkaWpf.Utils;
using PlenkaWpf.View;
using MessageBox = HandyControl.Controls.MessageBox;

namespace PlenkaWpf.VM;

/// <summary>
/// Конвертер значений для корректного отображенияпринадлежности свойства объекту
/// </summary>
public class ObjectInListConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var subset = values[1] as IList;
        bool? result = subset.Contains(values[0]);
        return result;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class SelectPropertiesVM : ViewModelBase
{
    #region Functions

    #region Constructors

    public SelectPropertiesVM(MembraneObject material)
    {
        Material = material;
        AvailableProperties = db.Properties.ToList();
        var materialProperties = material.Values.Select(v => v.Prop);
        AvailableProperties = AvailableProperties.Except(materialProperties).ToList();
        AllProperties = db.Properties.Local.ToObservableCollection();
    }

    #endregion

    #endregion

    #region Properties

    private MembraneContext db = DbContextSingleton.GetInstance();
    public ObservableCollection<Property> AllProperties { get; set; }
    public List<Property> AvailableProperties { get; set; }
    public MembraneObject Material { get; set; }
    private readonly List<Property> _propertiesToDelete = new();
    private readonly List<Property> _propertiesToAdd = new();
    public Property SelectedProperty { get; set; }

    public List<Property> MaterialProperties
    {
        get { return Material.Values.Select(o => o.Prop).ToList(); }
    }

    #endregion

    #region Commands

    private RelayCommand _selectProperties;

    /// <summary>
    /// Команда, сохраняющая результаты выбора свойств
    /// </summary>
    public RelayCommand SelectProperties
    {
        get
        {
            return _selectProperties ??= new RelayCommand(o =>
            {
                foreach (var property in _propertiesToDelete)
                    Material.Values.Remove(Material.Values.Where(o => o.Prop == property).First());

                foreach (var property in _propertiesToAdd)
                    if (Material.Values.Select(o => o)
                            .Where(o => o.MatId == Material.ObId && o.PropId == property.ProperrtyId).Count() ==
                        0)
                        Material.Values.Add(new Value {Mat = Material, Prop = property});


                DbContextSingleton.GetInstance().SaveChanges();
                OnClosingRequest();
            }); //, o => ((IList) o).Count > 0);
        }
    }

    private RelayCommand _isCompletedUncheckedCommand;

    /// <summary>
    /// Команда, обрабатывающая на выключение чекбокса со свойством
    /// </summary>
    public RelayCommand IsCompletedUncheckedCommand
    {
        get
        {
            return _isCompletedUncheckedCommand ??= new RelayCommand(o =>
            {
                _propertiesToAdd.Remove((Property) o);
                _propertiesToDelete.Add((Property) o);
            });
        }
    }


    private RelayCommand _isCompletedCheckedCommand;

    /// <summary>
    /// Команда, обрабатывающая на включение чекбокса со свойством
    /// </summary>
    public RelayCommand IsCompletedCheckedCommand
    {
        get
        {
            return _isCompletedCheckedCommand ??= new RelayCommand(o =>
            {
                _propertiesToDelete.Remove((Property) o);
                _propertiesToAdd.Add((Property) o);
                //MembraneObject.Values.Add((new Value() { Mat = MembraneObject, Prop = (TempProperty)o }));
            });
        }
    }


    private RelayCommand _createProperty;

    /// <summary>
    /// Команда, открывающая окно создания свойства
    /// </summary>
    public RelayCommand CreateProperty
    {
        get
        {
            return _createProperty ??= new RelayCommand(o =>
            {
                ShowChildWindow(new CreatePropertyWindow(new Property()));
            });
        }
    }

    private RelayCommand _editProperty;

    /// <summary>
    /// Команда, открывающая окно редактирования свойства
    /// </summary>
    public RelayCommand EditProperty
    {
        get
        {
            return _editProperty ??= new RelayCommand(
                o => { ShowChildWindow(new CreatePropertyWindow(SelectedProperty)); }, o => SelectedProperty != null);
        }
    }

    private RelayCommand _deleteProperty;

    /// <summary>
    /// Команда, удаляющая свойство
    /// </summary>
    public RelayCommand DeleteProperty
    {
        get
        {
            return _deleteProperty ??= new RelayCommand(o =>
            {
                //if (MessageBox.Show($"Вы действительно хотите удалить пользователя {SelectedUser.UserName}?", "Удаление пользователя", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                //{
                //    db.Properties.Remove(SelectedProperty);
                //    db.SaveChanges();
                //}
            });
        }
    }

    #endregion
}