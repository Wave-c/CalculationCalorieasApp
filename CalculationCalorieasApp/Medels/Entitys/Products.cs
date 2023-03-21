using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CalculationCalorieasApp.Medels.Entitys
{
    public class Products:Entity
    {
      
        public string Name;
        public int Calories;
        

        public Products(Guid id, string name, int calories)
        {
            Id=id;
            Name = name;
            Calories = calories;
            
        }

        public override string ToString()
        {
            return $"{Name} - {Calories}kcal";
        }
    }
}
