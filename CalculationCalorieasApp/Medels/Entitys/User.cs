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
        public StatusUser Status { get; set; }
    }

    public enum StatusUser { ADMIN, USER }
}
