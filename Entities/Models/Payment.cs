using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public int ApartmentID { get; set; }
        public int UserID { get; set; }
        public bool IsPayed { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
