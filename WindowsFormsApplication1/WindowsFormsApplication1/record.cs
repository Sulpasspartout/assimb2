using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class record
    {
        public ushort address { get; set; }
        public ushort size { get; set; }
        public List<obcode> rec = new List<obcode>();
    }
}
