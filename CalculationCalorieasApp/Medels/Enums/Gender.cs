using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationCalorieasApp.Medels.Enums
{
    public enum Gender
    {
        [Description("Мужчина")]
        Man,
        [Description("Женщина")]
        Woman,
        [Description("Пол не указан")]
        No
    }
}
