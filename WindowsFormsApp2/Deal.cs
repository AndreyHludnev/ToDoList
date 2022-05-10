using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    internal class Deal
    {
        public Deal ()
        {
            rd++;
        }
        public string Text { get; set; }
        public string Description { get; set; }
        public DateTime Term { get; set; } = DateTime.MinValue;
        public bool IsData { get; set; }
        public override string ToString()
        {
                return "Дело: " + Text;
        }
        public static int rd = 0;
    }
}
