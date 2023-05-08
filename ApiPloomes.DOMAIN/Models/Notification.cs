using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPloomes.DOMAIN.Models
{
    public class Notification
    {
        public string Field { get; private set; }
        public string Message { get; private set; }

        public Notification(string field, string message)
        {
            Field = field;
            Message = message;
        }
    }
}
