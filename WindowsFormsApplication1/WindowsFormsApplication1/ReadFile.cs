using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class ReadFile
    {
        public int counter = 0;
        public ReadFile()
        {
            
            string line;
            char[] x = { 'K', '2', 'm' };
            Assimbio.path =Assimbio.path.Trim(x);
            // Read the file and display it line by line.
            System.IO.StreamReader file =
               new System.IO.StreamReader(Assimbio.path+"txt");
            while ((line = file.ReadLine()) != null)
            {
                new Parse(line);
                System.Diagnostics.Debug.Write(line);
                counter++;
            }

            file.Close();

            
        }
    }
}
