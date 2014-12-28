using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class obcode
    {
        public byte type { get; set; }
        public byte opcode { get; set; }
        public string operand { get; set; }
        public bool index { get; set; }
        public ushort operandcode { get; set; }
        public ushort location { get; set; }
        public ushort line { get; set; }
        public List<string> error { get; set; }
        public List<char> bytes { get; set; }
        public ulong badbigfatoperand { get; set; }
        public int numericaloperand { get; set; }
        public ushort size { get; set; }
        public obcode()
        {
            error = new List<string>();
            bytes = new List<char>();
        }

    }
}
