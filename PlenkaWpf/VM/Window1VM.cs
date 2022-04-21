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

            tempLineSerie = new LineSeries(){Title = "Температура, °С" };
            tempLineSerie.Fill = System.Windows.Media.Brushes.Transparent;
            TempSeries = new SeriesCollection() {tempLineSerie};

            nLineSerie = new LineSeries(){ Title = "Вязкость, Па·с" };
            nLineSerie.Fill = System.Windows.Media.Brushes.Transparent;
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

        //private bool isValueBetweenBounds(double value, double leftBound, double rightBound)
        //{
        //    if (value >= leftBound && value <= rightBound)
        //    {
        //        return true;
        //    }

        //    return false;
        //}
        
        //private double getStepByValueRange(double valueRange, int countOfSteps)
        //{
        //    var estimatedStep = valueRange / countOfSteps;
        //    var normalizedStep = estimatedStep;
        //    var p = 1;

        //    //получаем мантиссу и порядок
        //    while (normalizedStep<1)
        //    {
        //        normalizedStep *= 10;
        //        p--;
        //    }
        //    while (normalizedStep > 10)
        //    {
        //        normalizedStep /= 10;
        //        p++;
        //    }

        //    double roundRule = 0;

        //    for (int i = 0; i <= roundRuleDigits.Length-2; i++)
        //    {
        //        if (isValueBetweenBounds(normalizedStep,roundRuleDigits[i],roundRuleDigits[i+1]))
        //        {
        //            if (normalizedStep - roundRuleDigits[i] < roundRuleDigits[i + 1] - normalizedStep)
        //            {
        //                roundRule=roundRuleDigits[i];
        //            }
        //            else
        //            {
        //                roundRule = roundRuleDigits[i + 1];
        //            }
        //            break;
        //        }
        //    }

        //    return roundRule * Math.Pow(10,p);
        //}

        //private double stepByDictionary(List<double> points)
        //{
        //    var range = points.Max() - points.Min();
        //    var step = getStepByValueRange(range, 10);
        //    return step;
        //}

        #endregion

        #region Properties

        private static readonly double[] roundRuleDigits = new[] {1, 2.5, 5,10};

        public List<MembraneObject> Materials { get; set; }

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

        private object _null;

        public object Null // Костыль для того чтобы ErrorStr в TextBox всегда был пустым
        {
            get { return _null; }
            set
            {
                _null = null;
                OnPropertyChanged();
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

        private double _tempAxisXStep;

        //public double TempAxisXStep
        //{
        //    get { return _tempAxisXStep; }
        //    set
        //    {
        //        _tempAxisXStep = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private double _tempAxisYStep;

        //public double TempAxisYStep
        //{
        //    get { return _tempAxisYStep; }
        //    set
        //    {
        //        _tempAxisYStep = value;
        //        OnPropertyChanged();
        //    }
        //}

        private LineSeries nLineSerie { get; set; }

        public SeriesCollection NSeries { get; set; }
        private double _nAxisXStep;

        //public double NAxisXStep
        //{
        //    get { return _nAxisXStep; }
        //    set
        //    {
        //        _nAxisXStep = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private double _nAxisYStep;

        //public double NAxisYStep
        //{
        //    get { return _nAxisYStep; }
        //    set
        //    {
        //        _nAxisYStep = value;
        //        OnPropertyChanged();
        //    }
        //}

        #endregion

        #region Timers

        public Stopwatch MathTimer { get; set; } = new();

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

                //TempAxisXStep = stepByDictionary(results.Ni.Keys.ToList());
                //TempAxisYStep = stepByDictionary( results.Ni.Values.ToList());
                //NAxisXStep = stepByDictionary( results.Ni.Keys.ToList());
                //NAxisYStep = stepByDictionary( results.Ni.Values.ToList());
                updateLineSeriesByDictionary(tempLineSerie,Results.Ti);
                updateLineSeriesByDictionary(nLineSerie, Results.Ni);
                
                OnPropertyChanged(nameof(TempSeries));
                OnPropertyChanged(nameof(NSeries));

                OnPropertyChanged(nameof(CordTempNs));
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