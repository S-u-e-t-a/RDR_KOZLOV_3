using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlenkaWpf.VM
{
    public class PropertyGridDemoModel
    {
        [Category("Входные параметры")]
        [DisplayName("Длина")]
        public int Length { get; set; }
        
        [Category("Входные параметры")]
        [DisplayName("Ширина")]
        public int Width { get; set; }

        [Category("Входные параметры")]
        [DisplayName("Глубина")]
        public int Depth { get; set; }

        [Category("Входные параметры")]
        [DisplayName("Тип материала")]
        public int matType { get; set; } // ТУТ НЕ ИНТ А ВЫБОР ТИПО

        [Category("Входные параметры")]
        [DisplayName("Плотность")]
        public int Density { get; set; }

        [Category("Входные параметры")]
        [DisplayName("Удельная теплоёмкость")]
        public int SpecifiсHeatCapacity { get; set; }

        [Category("Входные параметры")]
        [DisplayName("Температура плавления")]
        public int MeltingTemperature { get; set; }

        [Category("Варьируемые параметры")]
        [DisplayName("Скорость крышки")]
        public int CapSpeed { get; set; }

        [Category("Варьируемые параметры")]
        [DisplayName("Температура крышки")]
        public int CapTemperature { get; set; }

        [Category("Параметры математической модели")]
        [DisplayName("Шаг расчёта по длине канала")]
        public int Step { get; set; }

        [Category("Параметры математической модели")]
        [DisplayName("Коэффициент консистенции при температуре приведения")]
        public int СonsСoef { get; set; }

        [Category("Параметры математической модели")]
        [DisplayName("Температурный коэффициент вязкости материала")]
        public int TempСoef { get; set; }

        [Category("Параметры математической модели")]
        [DisplayName("Температура приведения")]
        public int RefTemp { get; set; }

        [Category("Параметры математической модели")]
        [DisplayName("Индекс течения материала")]
        public int MatFlowIndex { get; set; }
        
        [Category("Параметры математической модели")]     
        [DisplayName("Коэффициент теплоотдачи от крышки канала к материалу")]
        public int HeatCoef { get; set; }
    }



    internal class Window1VM : ViewModelBase

    {
        #region Functions

        #region Constructors

        public Window1VM()
        {
            DemoModel = new PropertyGridDemoModel
            {
                Length = 1,
                Width = 1,
            };
        }

        #endregion

        #endregion

        #region Properties

        public PropertyGridDemoModel DemoModel { get; set; } 

        #endregion

        #region Commands



        #endregion
    }
}
