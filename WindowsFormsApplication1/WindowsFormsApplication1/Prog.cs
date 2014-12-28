using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
   static class Prog
    {
        public static void main(List<command> commands)
        {
            assembler a = new assembler();
            foreach (command c in commands)
            {
                a.driver(c.label, c.comnd, c.operand);
            }
            writerecord r = new writerecord();
            r.write(a.obs);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"objcode", false))
            {
                foreach (record x in r.records)
                {
                    file.Write("T{0:x5}",x.address);
                    file.Write("{0:x2}",x.size);
                    foreach (obcode o in x.rec)
                    {
                        if (o.type == 0)
                        {
                            if (o.operand != null)
                            {
                                if (a.symtable.ContainsKey(o.operand))
                                {
                                    if (o.index)
                                        file.Write("{0:x6}", o.opcode * 0x10000 + 0x8000 + a.symtable[o.operand]);
                                    else
                                        file.Write("{0:x6}", o.opcode * 0x10000 + a.symtable[o.operand]);
                                }
                            }
                            else
                            {
                                if (o.index)
                                    file.Write("{0:x6}", o.opcode * 0x10000 + 0x8000 + o.operandcode);
                                else
                                    file.Write("{0:x6}", o.opcode * 0x10000 + o.operandcode);
                            }
                            
                        }
                        else if (o.type == 3)
                        {
                            file.Write("{0:x6}", o.numericaloperand);
                        }
                        else if (o.type == 40)
                        {
                            string s = "{0:x";
                            s += o.size.ToString() + "}";
                            file.Write(s, o.badbigfatoperand);
                        }
                        else if (o.type == 41)
                        {
                            foreach (char c in o.bytes)
                                file.Write("{0:x2}", (int)c);
                        }
                        else if (o.type == 6)
                        {
                            file.Write("4c0000");
                        }

                    }
                    file.WriteLine();
                }
            }
            
            Console.ReadLine();
        }
    }
}
