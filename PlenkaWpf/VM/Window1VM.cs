using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using PlenkaAPI;
using PlenkaAPI.Data;
using PlenkaAPI.Models;
using PlenkaWpf.Utils;

namespace PlenkaWpf.VM
{
    public struct CordTempN
    {
        public double cord { get; set; }
        public double temp { get; set; }
        public double n { get; set; }
    }

    internal class Window1VM : ViewModelBase

    {
        #region Functions

        #region Constructors

        public Window1VM()
        {
            Materials = DbContextSingleton.GetInstance().MembraneObjects.Where(o=> o.Type.TypeName=="Материал").ToList();

            Material = DbContextSingleton.GetInstance().MembraneObjects.First(v => v.ObName == "НашМатериал");
            Canal = DbContextSingleton.GetInstance().MembraneObjects.First(v => v.ObName == "Канал");
            MatModel = DbContextSingleton.GetInstance().MembraneObjects.First(v => v.ObName == "Стандартная модель");

            tempLineSerie = new LineSeries(){Title = "Температура"};
            TempSeries = new SeriesCollection() {tempLineSerie};

            nLineSerie = new LineSeries(){ Title = "Вязкость"};
            NSeries = new SeriesCollection() { nLineSerie };
        }

        #endregion

        private Value GetMatValueByPropertyName(string propName, MembraneObject mat)
        {
            return mat.Values.First(v => v.Prop.PropertyName == propName);
        }


        private void updateLineSeriesByDictionary(LineSeries ls, Dictionary<double, double> points)
        {
            var newValues = new ChartValues<ObservablePoint>();
            foreach (var point in points)
            {
                newValues.Add(new ObservablePoint(point.Key, point.Value));
            }

            ls.Values = newValues;
        }

        private List<CordTempN> cordTempNsByDictionaries(Dictionary<double, double> ti, Dictionary<double, double> ni)
        {
            var l = new List<CordTempN>();
            foreach (var d in ni)
            {
                l.Add(new CordTempN() {cord = d.Key, temp = ti[d.Key], n = ni[d.Key]});
            }

            return l;
        }

        #endregion

        #region Properties

        public  List<MembraneObject> Materials { get; set; }

        #region CanalProps

        public MembraneObject Canal { get; set; }

        public double? Length
        {
            get { return GetMatValueByPropertyName("Длина", Canal).Value1; }
            set
            {
                GetMatValueByPropertyName("Длина", Canal).Value1 = value;
                OnPropertyChanged();
            }
        }

        public double? Width
        {
            get { return GetMatValueByPropertyName("Ширина", Canal).Value1; }
            set
            {
                GetMatValueByPropertyName("Ширина", Canal).Value1 = value;
                OnPropertyChanged();
            }
        }

        public double? Depth
        {
            get { return GetMatValueByPropertyName("Глубина", Canal).Value1; }
            set
            {
                GetMatValueByPropertyName("Глубина", Canal).Value1 = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region MaterialProps

        private MembraneObject material;
        public MembraneObject Material
        {
            get
            {
                return material;

            }
            set
            {
                material = value;

                OnPropertyChanged(nameof(Density));
                OnPropertyChanged(nameof(SpecifiсHeatCapacity));
                OnPropertyChanged(nameof(MeltingTemperature));
            }
        }

        public double? Density
        {
            get { return GetMatValueByPropertyName("Плотность", Material).Value1; }
            set
            {
                GetMatValueByPropertyName("Плотность", Material).Value1 = value;
                OnPropertyChanged();
            }
        }

        public double? SpecifiсHeatCapacity
        {
            get { return GetMatValueByPropertyName("Удельная теплоемкость", Material).Value1; }
            set
            {
                GetMatValueByPropertyName("Удельная теплоемкость", Material).Value1 = value;
                OnPropertyChanged();
            }
        }

        public double? MeltingTemperature
        {
            get { return GetMatValueByPropertyName("Температура плавления", Material).Value1; }
            set
            {
                GetMatValueByPropertyName("Температура плавления", Material).Value1 = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region VarProps

        public double? CapSpeed { get; set; } = 1;

        public double? CapTemperature { get; set; } = 1;

        #endregion

        #region MatModelProps

        public MembraneObject MatModel { get; set; }

        public double? СonsСoef
        {
            get
            {
                return GetMatValueByPropertyName("Коэффициент констистенции материала при температуре приведения",
                    MatModel).Value1;
            }
            set
            {
                GetMatValueByPropertyName("Коэффициент констистенции материала при температуре приведения", MatModel)
                    .Value1 = value;
                OnPropertyChanged();
            }
        }

        public double? TempСoef
        {
            get { return GetMatValueByPropertyName("Температурный коэффициент вязкости материала", MatModel).Value1; }
            set
            {
                GetMatValueByPropertyName("Температурный коэффициент вязкости материала", MatModel).Value1 = value;
                OnPropertyChanged();
            }
        }

        public double? RefTemp
        {
            get { return GetMatValueByPropertyName("Температура приведения", MatModel).Value1; }
            set
            {
                GetMatValueByPropertyName("Температура приведения", MatModel).Value1 = value;
                OnPropertyChanged();
            }
        }

        public double? MatFlowIndex
        {
            get { return GetMatValueByPropertyName("Индекс течения материала", MatModel).Value1; }
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
                return GetMatValueByPropertyName("Коэффициеент теплоотдачи от крышки канала к материалу", MatModel)
                    .Value1;
            }
            set
            {
                GetMatValueByPropertyName("Коэффициеент теплоотдачи от крышки канала к материалу", MatModel).Value1 =
                    value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Graphics

        public List<CordTempN> CordTempNs
        {
            get
            {
                if (Results.Ni != null)
                {
                    return cordTempNsByDictionaries(Results.Ti, Results.Ni);
                }

                return null;
            }
        }

        private LineSeries tempLineSerie { get; set; }

        public SeriesCollection TempSeries { get; set; }

        private LineSeries nLineSerie { get; set; }

        public SeriesCollection NSeries { get; set; }

        #endregion

        #region Timers

        public Stopwatch MathTimer { get; set; } = new();
        public Stopwatch VisualTimer { get; set; } = new();

        #endregion

        
        public long TotalMemory
        {
            get
            {
                Process currentProcess = Process.GetCurrentProcess();
                return currentProcess.WorkingSet64 /(1024*1024);
            }
        }

        private CalculationResults results;

        public CalculationResults Results
        {
            get { return results; }
            set
            {
                results = value;
                OnPropertyChanged();
                VisualTimer.Reset();
                VisualTimer.Start();
                updateLineSeriesByDictionary(tempLineSerie,Results.Ti);
                updateLineSeriesByDictionary(nLineSerie, Results.Ni);
                OnPropertyChanged(nameof(TempSeries));
                OnPropertyChanged(nameof(NSeries));
                OnPropertyChanged(nameof(CordTempNs));
                VisualTimer.Stop();
                OnPropertyChanged(nameof(VisualTimer));
                OnPropertyChanged(nameof(MathTimer));
                OnPropertyChanged(nameof(TotalMemory));
            }
        }

        public double? Step { get; set; } = 0.1;

        public bool IsCalculated = false;

        #endregion

        #region Commands

        private RelayCommand _calcCommand;

        public RelayCommand CalcCommand
        {
            get
            {
                return _calcCommand ?? (_calcCommand = new RelayCommand(o =>
                {
                    IsCalculated = true;
                    MathTimer.Reset();
                    MathTimer.Start();
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
                    MathTimer.Stop();
                    OnPropertyChanged(nameof(MathTimer));
                    
                }));
            }
        }

        #endregion
    }
}