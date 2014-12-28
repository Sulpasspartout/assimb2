using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class assembler
    {
        private string[] directives = { "start", "end", "word", "byte", "resw", "resb","ltorg", "equ", "org" };
        public Dictionary<string, ushort> symtable = new Dictionary<string, ushort>();
        Dictionary<string, byte> optable = new Dictionary<string, byte>();
        public List<obcode> obs = new List<obcode>();
        private List<obcode> literals = new List<obcode>();
        private List<string> locations = new List<string>();
        private Stack<ushort> orgstack = new Stack<ushort>();
        private ushort LiteralCounter =0 ;
        public bool endofassembling = false;
        public assembler()
        {
            initialize();
        }
        public void driver(string label, string command, string operand)
        {
            List<string> error = new List<string>();
            if (label != null)
            {
                try
                {
                    if (validlabel(label))
                        symtable.Add(label, global.locationcounter);
                    else
                    {
                        error.Add("illigal label");
                    }

                }
                catch (ArgumentException)
                {
                    error.Add("duplicate labels");
                }
            }
            if (isdirective(command))
            {
                obcode o = assembledirective(command, operand, error, true, label);
                if (o!=null)
                    obs.Add(o);
            }
                
            else
                obs.Add(assemble(command, operand, error));
        }
        private obcode assemble(string command, string operand, List<string> error)
        {
            if (!global.exe)
            {
                global.first = global.locationcounter;
                global.exe = true;
            }
            obcode o = new obcode();
            o.line = global.line;
            o.type = 0;
            o.location = global.locationcounter;
            if (command.Equals("rsub"))
            {
                o.type = 6;
                o.opcode = 76;
                goto koko;
            }
            if (optable.ContainsKey(command))
                o.opcode = optable[command];
            else
                o.error.Add("not found command");
            if (operand != null)
            {
                string[] x;
                switch (operandIdentify(operand))
                {
                    case 0:
                        o.error.Add("illigal label");
                        o.index = false;
                        break;
                    case 1:
                        x = operand.Split(',');
                        o.operand = x[0];
                        o.index = true;
                        break;
                    case 3:
                        x = operand.Split(',');
                        o.operandcode = UInt16.Parse(x[0], System.Globalization.NumberStyles.HexNumber);
                        operand = null;
                        o.index = true;
                        break;
                    case 2:
                        o.operand = operand;
                        o.index = false;
                        break;
                    case 4:
                        
                        o.operandcode = UInt16.Parse(operand, System.Globalization.NumberStyles.HexNumber);
                        operand = null;
                        o.index = false;
                        break;
                    default:
                        break;
                }
            }
            if (error.Count > 0)
                o.error.AddRange(error);
        koko: 
            o.size = 3;
        
            global.locationcounter += 3;
            return o;
            

        }
        private obcode assembledirective(string command, string operand, List<string> error, bool loc, string label)
        {
            obcode o = new obcode();
            o.line = global.line;
            o.index = false;
            if (command.Equals("start"))
            {
                o.type = 1;

                if (validaddress(operand))
                {
                    if (!global.start)
                    {

                        global.locationcounter = global.startaddress = UInt16.Parse(operand, System.Globalization.NumberStyles.HexNumber);
                        global.start = true;
                    }

                    else
                        global.GeneralErrors.Add("duplicate start at line "+global.line);
                }

                else
                   global.GeneralErrors.Add("illigal operand at line"+ global.line);
                o.size = 0;

            }
            else if (command.Equals("end"))
            {
                o.type = 2;
                endofassembling = true;
                if (literals.Count > 0)
                {
                    foreach (string item in locations)
                    {
                        symtable[item] += global.locationcounter;
                    }
                    global.locationcounter = Convert.ToUInt16(symtable[locations.Last()] + literals.Last().size);
                    obs.AddRange(literals);
                    literals = new List<obcode>();
                    locations = new List<string>();
                    LiteralCounter = 0;
                }
            }
            else if (command.Equals("ltorg"))
            {
                if (literals.Count > 0)
                {
                    foreach (string item in locations)
                    {
                        symtable[item] += global.locationcounter;
                    }
                    global.locationcounter = Convert.ToUInt16(symtable[locations.Last()] + literals.Last().size);
                    obs.AddRange(literals);
                    literals = new List<obcode>();
                    locations = new List<string>();
                    LiteralCounter = 0;
                }
                
                return null;
            }
            else if (command.Equals("equ"))
            {
                ushort number;
                if (UInt16.TryParse(operand,out number))
                    symtable[label] = number;
                else if (operand == "*")
                {
                    symtable[label] = global.locationcounter;
                }
                else if (symtable.ContainsKey(operand))
                        symtable[label] = symtable[operand];
                    else
                        global.GeneralErrors.Add("illigal operand at equ at line " + global.line.ToString());
                return null;
            }
            else if (command.Equals("org"))
            {
                ushort number;
                if (UInt16.TryParse(operand,out number))
                {
                    orgstack.Push(global.locationcounter);
                    global.locationcounter = UInt16.Parse(operand, System.Globalization.NumberStyles.HexNumber);
                }
                else if (operand == "")
                {
                    if (orgstack.Count != 0)
                        global.locationcounter = orgstack.Pop();
                    else
                        global.GeneralErrors.Add("Invalid operand to org at line " + global.line.ToString());
                }
                else if (symtable.ContainsKey(operand))
                {
                    orgstack.Push(global.locationcounter);
                    global.locationcounter = symtable[operand];
                }
                    
                else
                    global.GeneralErrors.Add("illigal operand at equ at line " + global.line.ToString());
                return null;
            }
            else if (command.Equals("byte"))
            {
                if (operand[0] == 'c' || operand[0] == 'C')
                {
                    o.type = 41;
                    operand = operand.Trim(operand[0]);
                    operand = operand.Trim('\'');
                    foreach (char c in operand)
                    {
                        o.bytes.Add(c);
                    }
                    o.size = Convert.ToUInt16(operand.Length);

                }
                else if (operand[0] == 'x' || operand[0] == 'X')
                {
                    o.type = 40;
                    operand = operand.Trim(operand[0], '\'');
                    if (validaddress(operand))
                    {
                        o.badbigfatoperand = UInt64.Parse(operand, System.Globalization.NumberStyles.HexNumber);
                        o.size = Convert.ToUInt16(Math.Ceiling(operand.Length / 2.0));
                    }
                    else
                    {
                        o.error.Add("wrong operand");
                        o.size = 1;
                    }


                }
                else
                {
                    o.type = 40;
                    o.error.Add("wrong operand");
                }
                if (loc)
                    global.locationcounter += o.size;
                else
                    LiteralCounter += o.size;
            }
            else if (command.Equals("word"))
            {
                o.type = 3;
                if (operand[0] == '-')
                {
                    operand.Trim('-');
                    int i = 0;
                    try
                    {
                        i = Int32.Parse(operand);
                    }
                    catch (ArgumentException)
                    {
                        o.error.Add("wrong operand");
                    }
                    if (i < 16777216)
                        o.numericaloperand = 16777216 - i;
                    else
                        o.error.Add("too small operand ");


                }
                else if (Char.IsDigit(operand[0]))
                {
                    int i = 0;
                    try
                    {
                        i = Int32.Parse(operand);
                    }
                    catch (ArgumentException)
                    {
                        o.error.Add("wrong operand");
                    }
                    if (i < 16777216)
                        o.numericaloperand = i;
                    else
                        o.error.Add("too big operand");
                }
                else
                    o.error.Add("expected number");
                if (loc)
                    o.location = global.locationcounter;
                else
                    o.location = LiteralCounter;
                o.size = 3;
                if (loc)
                    global.locationcounter += o.size;
                else
                    LiteralCounter += o.size;
            }
            else if (command.Equals("resw") || command.Equals("resb"))
            {
                o.type = 5;
                int i = 0;
                try
                {
                    i = Int32.Parse(operand);
                }
                catch (ArgumentException)
                {
                    o.error.Add("wrong operand");
                }
                o.location = global.locationcounter;
                if (command.Equals("resw"))
                {
                    o.size = Convert.ToUInt16(3 * i);
                    global.locationcounter += o.size;
                }

                else
                {
                    o.size = Convert.ToUInt16(i);
                    global.locationcounter += o.size;
                }


            }


            if (error!=null)
                if (error.Count > 0)
                    o.error.AddRange(error);
            if (o.type != 1 && o.type != 2)
                return o;
            return null;
        }
        private byte operandIdentify(string operand)
        {
           
            if (operand[0] == '=')
            {
                if (!symtable.ContainsKey(operand))
                { 
                    symtable.Add(operand, LiteralCounter);
                    locations.Add(operand);
                    operand = operand.Trim('=');
                    if (CharOrHex(operand))
                        literals.Add(assembledirective("byte", operand, null, false, ""));
                    else if (Char.IsDigit(operand[0]))
                        literals.Add(assembledirective("word", operand, null, false, ""));
                    else
                    {
                        obcode o = new obcode();
                        o.type = 40;
                        o.error.Add("invalid literal");
                        literals.Add(o);
                    }
                }
              
                return 2;
            }
            else if (!CharOrHex(operand))
            {
                if (operand.Contains(','))
                {
                    string[] x = operand.Split(',');
                    if (x.Length == 2 && (x[1].Equals("X") || x[1].Equals("x")))
                    {
                        if (validlabel(x[0]))
                            return 1;
                        else if (validaddress(x[0]))
                            return 3;
                        else
                            return 0;
                    }
                }
                else
                {
                    if (validlabel(operand))
                        return 2;
                    else if (validaddress(operand))
                        return 4;
                    else
                        return 0;
                }
            }
            else
                return 0;
            return 0;
        }
        private bool CharOrHex(string operand)
        {
            return operand.StartsWith("c'") || operand.StartsWith("C'") || operand.StartsWith("x'") || operand.StartsWith("X'");
        }
        private bool validlabel(string operand)
        {
            if (!Char.IsLetter(operand[0]))
                return false;
            foreach (char h in operand)
            {
                if (!Char.IsLetterOrDigit(h))
                    return false;
            }
            return true;
        }
        private bool validaddress(string operand)
        {

            return System.Text.RegularExpressions.Regex.IsMatch(operand, @"\A\b[0-9a-fA-F]+\b\Z");

        }
        private bool isdirective(string command)
        {
            foreach (string item in directives)
            {
                if (command.Equals(item))
                    return true;
            }
            return false;
        }
        private void initialize()
        {
            optable.Add("add", 24);
            optable.Add("and", 64);
            optable.Add("comp", 40);
            optable.Add("div", 36);
            optable.Add("j", 60);
            optable.Add("jeq", 48);
            optable.Add("jgt", 52);
            optable.Add("jlt", 56);
            optable.Add("jsub", 72);
            optable.Add("lda", 0);
            optable.Add("ldch", 80);
            optable.Add("ldl", 8);
            optable.Add("ldx", 4);
            optable.Add("mul", 32);
            optable.Add("or", 68);
            optable.Add("rd", 216);
            optable.Add("sta", 12);
            optable.Add("stch", 84);
            optable.Add("stl", 20);
            optable.Add("stx", 16);
            optable.Add("sub", 28);
            optable.Add("td", 224);
            optable.Add("tix", 44);
            optable.Add("wd", 220);
        }
    }
}
