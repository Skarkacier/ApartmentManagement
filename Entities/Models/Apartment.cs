using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Apartment
    {
        public int ApartmentID { get; set; }
        public string Block { get; set; }
        public string Floor { get; set; }
        public string Number { get; set; }
        public bool Status { get; set; }
        public int UserID { get; set; }
    }
}
