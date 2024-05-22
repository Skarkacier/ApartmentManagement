using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Message
    {
        public int MessageID { get; set; }
        public string MessageText { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public bool IsRead { get; set; }
        public DateTime MessageDate { get; set; }
    }
}
