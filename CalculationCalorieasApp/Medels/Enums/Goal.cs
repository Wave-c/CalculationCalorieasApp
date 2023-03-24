using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationCalorieasApp.Medels.Enums
{
    public enum Goal
    {
        [Description("Увеличение веса")]
        Increase,
        [Description("Уменьшение веса")]
        Decrease,
        [Description("Сохранение веса")]
        Save
    }
}
