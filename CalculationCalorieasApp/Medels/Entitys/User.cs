using CalculationCalorieasApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationCalorieasApp.Medels.Entitys
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public byte[] Image { get; set; }
        public Gender Gender { get; set; }
        public StatusUser Status { get; set; }
        public Goal Goal { get; set; }
        public Gender Gender { get; set; }
        public Activ Activ { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public int Age { get; set; }    

        public int CalPerDay { get; set; }
    }

    public enum StatusUser { ADMIN, USER }
}
