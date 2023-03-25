using CalculationCalorieasApp.Medels.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CalculationCalorieasApp.Medels.Entitys
{
    public class Product : Entity
    {
      
        public string Name { get; set; }
        public int Calories { get; set; }

        [NotMapped]
        public Eating Eating { get; set; }

        public Product(Guid id, string name, int calories)
        {
            Id=id;
            Name = name;
            Calories = calories;
            
        }

        public override string ToString()
        {
            return $"{Name} - {Calories}ккал";
        }
    }
}
