using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class writerecord
    {
        public List<record> records = new List<record>();
        private List<obcode> obcodes;
        private bool endofrecord;
        private bool resw;
        private bool condition;
        private bool endofprogram;
        public void write(List<obcode> obcodes)
        {
            int index = 0;
            this.obcodes = obcodes;
            ushort nxtaddress = global.startaddress;
            while (index < obcodes.Count)
            {
                record r = new record();
                r.address = nxtaddress;
                r.size = 0;
                int counter = 30;
                evalute(obcodes[index].size, obcodes[index].type, 30, index);
                while (!condition)
                {
                    r.rec.Add(obcodes[index]);
                    r.size += obcodes[index].size;
                    counter -= obcodes[index].size;
                    index++;
                    if (!endofprogram)
                        evalute(obcodes[index].size, obcodes[index].type, counter, index);
                    else
                        break;
                }
                if (r.size > 0)
                    records.Add(r);
                if (condition)
                {
                    nxtaddress = Convert.ToUInt16(r.address + r.size + (resw ? obcodes[index].size : 0));
                    index += resw ? 1 : 0;

                }
                

            }

        }
        public void evalute(int size, int type, int counter, int index)
        {
            //termination conditions
            endofprogram = index + 1 >= obcodes.Count;
            resw = type == 5;
            endofrecord = size > counter;
            condition = resw || endofrecord;
        }
        //private record record()
        //{
        //    record r = new record();
        //    int counter = 30;
        //    if (this.records.Capacity == 0)
        //        r.address = obcodes[index].location;
        //    else
        //        r.address = records.Last().nxtaddress;
        //    int i = index;
        //    if (i >= obcodes.Capacity - 1)
        //    for (i = index; i< obcodes.Capacity-1 &&obcodes[i].size <= counter && obcodes[i].type != 5; r.size += obcodes[i].size, counter -= obcodes[i++].size)
        //    {
        //        if (obcodes[i].type !=1 && obcodes[i].type!= 2 )
        //            r.rec.Add(obcodes[i]);
        //    }

        //    if (obcodes[i].type == 5)//i out of range 
        //        r.nxtaddress = r.address + r.size + obcodes[i].size;
        //    else
        //        r.nxtaddress = r.address + r.size;
        //    return r;
        //}
    }

}
