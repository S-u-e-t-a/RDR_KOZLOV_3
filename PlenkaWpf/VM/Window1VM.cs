using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using PlenkaAPI;
using PlenkaAPI.Data;
using PlenkaAPI.Models;
using PlenkaWpf.Utils;

namespace PlenkaWpf.VM
{

    internal class Window1VM : ViewModelBase

    {
        #region Functions

        #region Constructors

        public Window1VM()
        {
            Material = DbContextSingleton.GetInstance().MembraneObjects.First(v => v.ObName == "НашМатериал");
            Canal = DbContextSingleton.GetInstance().MembraneObjects.First(v => v.ObName == "Канал");
            MatModel = DbContextSingleton.GetInstance().MembraneObjects.First(v => v.ObName == "Стандартная модель");

        }

        #endregion

        private Value GetMatValueByPropertyName(string propName, MembraneObject mat)
        {
            return mat.Values.First(v => v.Prop.PropertyName == propName);
        }
        #endregion

        #region Properties

        #region CanalProps

        public MembraneObject Canal { get; set; }
        public double? Length
        {
            get
            {
                return GetMatValueByPropertyName("Длина", Canal).Value1;
            }
            set
            {
                GetMatValueByPropertyName("Длина", Canal).Value1 = value;
                OnPropertyChanged();
            }
        }
        public double? Width
        {
            get
            {
                return GetMatValueByPropertyName("Ширина", Canal).Value1;
            }
            set
            {
                GetMatValueByPropertyName("Ширина", Canal).Value1 = value;
                OnPropertyChanged();
            }
        }
        public double? Depth
        {
            get
            {
                return GetMatValueByPropertyName("Глубина", Canal).Value1;
            }
            set
            {
                GetMatValueByPropertyName("Глубина", Canal).Value1 = value;
                OnPropertyChanged();
            }
        }

        #endregion
        
        #region MaterialProps

        public MembraneObject Material { get; set; }
        public double? Density
        {
            get
            {
                return GetMatValueByPropertyName("Плотность", Material).Value1;
            }
            set
            {
                GetMatValueByPropertyName("Плотность", Material).Value1 = value;
                OnPropertyChanged();
            }
        }
        public double? SpecifiсHeatCapacity
        {
            get
            {
                return GetMatValueByPropertyName("Удельная теплоемкость", Material).Value1;
            }
            set
            {
                GetMatValueByPropertyName("Удельная теплоемкость", Material).Value1 = value;
                OnPropertyChanged();
            }
        }
        public double? MeltingTemperature
        {
            get
            {
                return GetMatValueByPropertyName("Температура плавления", Material).Value1;
            }
            set
            {
                GetMatValueByPropertyName("Температура плавления", Material).Value1 = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region VarProps
        public double? CapSpeed
        {
            get;
            set;
        }
        public double? CapTemperature
        {
            get;
            set;
        }

        #endregion

        #region MatModelProps
        public MembraneObject MatModel { get; set; }
        
        public double? СonsСoef
        {
            get
            {
                return GetMatValueByPropertyName("Коэффициент констистенции материала при температуре приведения", MatModel).Value1;
            }
            set
            {
                GetMatValueByPropertyName("Коэффициент констистенции материала при температуре приведения", MatModel).Value1 = value;
                OnPropertyChanged();
            }
        }
        public double? TempСoef
        {
            get
            {
                return GetMatValueByPropertyName("Температурный коэффициент вязкости материала", MatModel).Value1;
            }
            set
            {
                GetMatValueByPropertyName("Температурный коэффициент вязкости материала", MatModel).Value1 = value;
                OnPropertyChanged();
            }
        }
        public double? RefTemp
        {
            get
            {
                return GetMatValueByPropertyName("Температура приведения", MatModel).Value1;
            }
            set
            {
                GetMatValueByPropertyName("Температура приведения", MatModel).Value1 = value;
                OnPropertyChanged();
            }
        }
        public double? MatFlowIndex
        {
            get
            {
                return GetMatValueByPropertyName("Индекс течения материала", MatModel).Value1;
            }
            set
            {
                GetMatValueByPropertyName("Индекс течения материала", MatModel).Value1 = value;
                OnPropertyChanged();
            }
        }
        public double? HeatCoef
        {
            get
            {
                return GetMatValueByPropertyName("Коэффициеент теплоотдачи от крышки канала к материалу", MatModel).Value1;
            }
            set
            {
                GetMatValueByPropertyName("Коэффициеент теплоотдачи от крышки канала к материалу", MatModel).Value1 = value;
                OnPropertyChanged();
            }
        }

        #endregion

        private CalculationResults results;

        public CalculationResults Results
        {
            get
            {
                return results;
            }
            set
            {
                results = value;
                OnPropertyChanged();
            }
        }

        public double? Step
        {
            get;
            set;
        }


        #endregion

        #region Commands

        private RelayCommand _calcCommand;

        public RelayCommand CalcCommand
        {
            get { return _calcCommand ?? (_calcCommand = new RelayCommand(o =>
            {
                var cp = new CalculationParameters()
                {
                    W = (double) Width,
                    H = (double) Depth,
                    L = (double) Length,
                    p = (double) Density,
                    c = (double) SpecifiсHeatCapacity,
                    T0 = (double) MeltingTemperature,
                    Vu = (double) CapSpeed,
                    Tu = (double) CapTemperature,
                    u0 = (double) СonsСoef,
                    b = (double) TempСoef,
                    Tr = (double) RefTemp,
                    n = (double) MatFlowIndex,
                    au = (double) HeatCoef,
                    step = (double) Step
                };
                var mc = new MathClass(cp);
                Results = mc.calculate();
            })); }
        }



        #endregion
    }
}
