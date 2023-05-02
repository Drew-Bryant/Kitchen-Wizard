using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Data_Objects
{
    public enum Unit
    {
        //Group A
        tsp,
        tbsp,
        cups,
        cup,
        fl_oz,
        oz,
        pints,
        quarts,
        gallons,
        liters,
        ml,
        lbs,
        lb,
        mg,
        grams,
        kgs,
        F,
        C
    }
    public partial class Food : ObservableObject
    {

        [ObservableProperty]
        int unitIndex;
        [ObservableProperty]
        double quantityValue;

        public string QuantityString { get; set; }

        [ObservableProperty]
        Unit units;
        
        public string Name { get; set; }
        public bool IsSpice { get; set; }
        public int ID { get; set; }
        public ObservableCollection<Unit> MyUnits { get; set; } = new ();


        public ObservableCollection<Unit> VolumeUnits { get; set; } = new()
        { Unit.tsp, Unit.tbsp, Unit.cups, Unit.cup, Unit.fl_oz, Unit.pints, Unit.quarts, Unit.gallons, Unit.liters, Unit.ml};

        public ObservableCollection<Unit> MassUnits { get; set; } = new()
        {Unit.lbs, Unit.lb, Unit.mg, Unit.grams, Unit.kgs, Unit.oz};

        public ObservableCollection<Unit> HeatUnits { get; set; } = new()
        { Unit.C, Unit.F };


    }
}
