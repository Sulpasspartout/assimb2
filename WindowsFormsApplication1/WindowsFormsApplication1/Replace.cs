using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Replace : Form
    {
        public Replace()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if(textBox2.Text.Length>0)
            {
                button2.Enabled = true;
                
            }
            else
            {
                button2.Enabled = false;
                
            }
        }

      

        private void button4_Click(object sender, EventArgs e)
        {
            Assimbio.findtext = "";
            Assimbio.Replacetxt = "";
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Assimbio.findtext = textBox1.Text;
            Assimbio.Replacetxt = textBox2.Text;
            this.Close();
        }
    }
}
