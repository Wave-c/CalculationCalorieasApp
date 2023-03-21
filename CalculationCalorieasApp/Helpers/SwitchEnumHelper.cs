using CalculationCalorieasApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationCalorieasApp.Helpers
{
    public class SwitchEnumHelper
    {
        public static double EnumConverter(Activ selectedActive)
        {
            switch (selectedActive)
            {
                case Activ.First:
                    return 1.2;
                    break;
                case Activ.Second:
                    return 1.375;
                    break;
                case Activ.Third:
                    return 1.55;
                    break;
                case Activ.Fourth:
                    return 1.725;
                    break;
                case Activ.Fifth:
                    return 1.725;
                    break;
            }
            return 0;
        }

    }
}
