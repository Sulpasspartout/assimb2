using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Parse
    {
        // ana kareem awel commit mn al github
        public static List<command> Matrix = new List<command>();
        command l = new command();
        
        StringBuilder Block = new StringBuilder();
        int i = 0;
        public Parse(String line)
        {
           // l.comment = "";
           // l.comnd = "";
            l.label = "";
            //l.operand = "";
            if (line.Length > i && line[0] == ' ')
            {
                while (line.Length > i && line[i] == ' ')
                {
                    i++;
                }
                if (line.Length > i && line[0] == '.')
                {
                    l.comment = (line);
                }
                else
                {
                    // مفيش ليبول اشتغل
                    while (i < line.Length && (line[i] != ' ' && line[i] != '\n'))
                    {
                        Block.Append(line[i]);
                        i++;
                    }
                    l.comnd = (Block.ToString());
                    Block.Remove(0, Block.Length);
                    if (line.Length > i && line[i] == '\n')
                    {
                        Matrix.Add(l);
                        return;
                    }
                    while (line.Length > i && line[i] == ' ')
                    {
                        i++;
                    }
                    while (i < line.Length && (line[i] != ' ' && line[i] != '\n'))
                    {
                        Block.Append(line[i]);
                        i++;
                    }
                    l.operand = (Block.ToString());
                    Block.Remove(0, Block.Length);
                    if (line.Length > i && line[i] == '\n')
                    {
                        Matrix.Add(l);
                        return;
                    }
                    while (line.Length > i && line[i] == ' ')
                    {
                        i++;
                    }
                    while (line.Length > i && line[i] != '\n')
                    {
                        Block.Append(line[i]);
                        i++;
                    }
                    l.comment = (Block.ToString());
                    Block.Remove(0, Block.Length);
                }
            }
            else if (line.Length > i && line[0] == '.')
            {
                l.comment = (line);
            }
            else if (line.Length > i)
            {
                // في ليبول
                while (i < line.Length && (line[i] != ' ' && line[i] != '\n'))
                {
                    Block.Append(line[i]);
                    i++;
                }
                l.label = (Block.ToString());
                Block.Remove(0, Block.Length);
                if (line.Length <= i || line[i] == '\n')
                {
                    Matrix.Add(l);
                    return;
                }
                while (line.Length > i && line[i] == ' ')
                {
                    i++;
                }
                while (i < line.Length && (line[i] != ' ' && line[i] != '\n'))
                {
                    Block.Append(line[i]);
                    i++;
                }
                l.comnd = (Block.ToString());
                Block.Remove(0, Block.Length);
                if (line.Length <= i || line[i] == '\n')
                {
                    Matrix.Add(l);
                    return;
                }
                while (line.Length > i && line[i] == ' ')
                {
                    i++;
                }
                while (i < line.Length && (line[i] != ' ' && line[i] != '\n'))
                {
                    Block.Append(line[i]);
                    i++;
                }
                l.operand = (Block.ToString());
                Block.Remove(0, Block.Length);
                if (line.Length <= i || line[i] == '\n')
                {
                    Matrix.Add(l);
                    return;
                }
                while (line[i] == ' ')
                {
                    i++;
                }
                while (line.Length > i && line[i] != '\n')
                {
                    Block.Append(line[i]);
                    i++;
                }
                l.comment = (Block.ToString());
                Block.Remove(0, Block.Length);

            }

            Matrix.Add(l);
        }
    }
}
