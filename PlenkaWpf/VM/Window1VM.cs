using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlenkaAPI.Data;
using PlenkaAPI.Models;

namespace PlenkaWpf.VM
{

    internal class Window1VM : ViewModelBase

    {
        #region Functions

        #region Constructors

        public Window1VM()
        {
            Material = DbContextSingleton.GetInstance().MembraneObjects.First(v => v.ObName == "Пластик");
        }

        #endregion

        #endregion


        private Value GetMatValueByPropertyName(string propName,MembraneObject mat)
        {
            return mat.Values.First(v => v.Prop.PropertyName == propName);
        }
        #region Properties

        public double? Length
        {
            get
            {
                return GetMatValueByPropertyName("Длина", Material).Value1;
            }
            set
            {
                GetMatValueByPropertyName("Длина",Material).Value1 = value;
                OnPropertyChanged();
            }
        }

        public double? Width
        {
            get
            {
                return GetMatValueByPropertyName("Ширина", Material).Value1;
            }
            set
            {
                GetMatValueByPropertyName("Ширина", Material).Value1 = value;
                OnPropertyChanged();
            }
        }
        public double? Depth
        {
            get
            {
                return GetMatValueByPropertyName("Глубина", Material).Value1;
            }
            set
            {
                GetMatValueByPropertyName("Глубина", Material).Value1 = value;
                OnPropertyChanged();
            }
        }
        public MembraneObject Material { get; set; } 
        public int Density { get; set; }
        public int SpecifiсHeatCapacity { get; set; }
        public int MeltingTemperature { get; set; }
        public int CapSpeed { get; set; }
        public int CapTemperature { get; set; }
        public int Step { get; set; }
        public int СonsСoef { get; set; }
        public int TempСoef { get; set; }
        public int RefTemp { get; set; }
        public int MatFlowIndex { get; set; }
        public int HeatCoef { get; set; }
 
        #endregion

        #region Commands



        #endregion
    }
}
