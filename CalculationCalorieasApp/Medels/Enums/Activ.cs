using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationCalorieasApp.Medels.Enums
{
    public enum Activ
    {
        [Description("Низкая активность, полное отсутствие спорта")]
        First,
        [Description("Малоподвижный образ жизни, физ. активность 1-2 раза в неделю")]
        Second,
        [Description("Средняя активность, физ. активность 2-4 раза в неделю")]
        Third,
        [Description("Активный образ жизни, подвижная работа, активность 5 раз в неделю")]
        Fourth,
        [Description("Высокая активность, подвижная работа, ежедневные тренировки")]
        Fifth
    }
}
