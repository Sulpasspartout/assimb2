using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Assimbio : Form
    {
        public static String findtext;
        public static String Replacetxt;
        public static String path = "";
        public Assimbio()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (path == "" && richTextBox1.Text.Length > 0)
            {
                var d=MessageBox.Show("Do you want to save ?","lol",MessageBoxButtons.YesNoCancel);
                if(d== System.Windows.Forms.DialogResult.Yes)
                {
                    saveAsToolStripMenuItem_Click(sender, e);
                }
                else if (d== System.Windows.Forms.DialogResult.No)
                {
                    richTextBox1.Clear();
                }
                else { }
            }
            else
                richTextBox1.Clear();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "all text (*.K2m)|*.K2m";
            openFileDialog1.FileName = "";
            var d= openFileDialog1.ShowDialog();
            if(d == System.Windows.Forms.DialogResult.OK)
            {
                richTextBox1.LoadFile(openFileDialog1.FileName);
                path = openFileDialog1.FileName;
            }
            
        }
        public void wri()
        {
            char[] x = { 'K' ,'2','m'};
            path=path.Trim(x);
            StreamWriter sw = File.CreateText(path + "txt");
            foreach (String s in richTextBox1.Lines)
            {
                sw.WriteLine(s);
            }
            sw.Flush();
            sw.Close();
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (path != "")
            {
                richTextBox1.SaveFile(path);
                wri();
            }
            else
            {


                saveFileDialog1.Filter = "all text (*.K2m)|*.K2m";
                saveFileDialog1.FileName = "";
                var d = saveFileDialog1.ShowDialog();
                if (d == System.Windows.Forms.DialogResult.OK)
                {
                    richTextBox1.SaveFile(saveFileDialog1.FileName);
                    path = saveFileDialog1.FileName;
                }

                wri();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "all text (*.K2m)|*.K2m";
            saveFileDialog1.FileName = "";
            var d = saveFileDialog1.ShowDialog();
            if (d == System.Windows.Forms.DialogResult.OK)
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName);
            }

            wri();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (path == "" && richTextBox1.Text.Length > 0)
            {
                var d = MessageBox.Show("Do you want to save before exite ?", "lol", MessageBoxButtons.YesNoCancel);
                if (d == System.Windows.Forms.DialogResult.Yes)
                {
                    saveAsToolStripMenuItem_Click(sender, e);
                }
                else if (d == System.Windows.Forms.DialogResult.No)
                {
                    this.Close();
                }
                else { }
            }
            else
            this.Close();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = "";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Find r = new Find();
            r.ShowDialog();
            if(findtext!="")
                richTextBox1.Find(findtext);
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Replace f = new Replace();
            f.ShowDialog();
            if (findtext != "")
                richTextBox1.Find(findtext);
            richTextBox1.Find(findtext);
            richTextBox1.SelectedText = Replacetxt;
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            richTextBox1.Font = fontDialog1.Font;
        }

        private void fontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            richTextBox1.ForeColor = colorDialog1.Color;
        }

        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            richTextBox1.BackColor = colorDialog1.Color;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(sender, e);
            ReadFile n = new ReadFile();
            Prog.main(Parse.Matrix);
            tabControl2.Visible = true;
            richTextBox2.Clear();
            for (int i = 0; i < n.counter; i++)
            {
                richTextBox2.AppendText(Parse.Matrix[i].label.ToString());
                richTextBox2.AppendText("\n");
            }
        }

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {
            var lineCount = richTextBox1.Lines.Count();
            //numberLabel.Text = lineCount.ToString();
            if (richTextBox1.Text.Length > 0 /*&& richTextBox1.SelectedText.Length>0*/)
            {
                undoToolStripMenuItem.Enabled = true;
                redoToolStripMenuItem.Enabled = true;
                cutToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
                deleteToolStripMenuItem.Enabled = true;
            }
            else
            {
                undoToolStripMenuItem.Enabled = false;
                redoToolStripMenuItem.Enabled = false;
                cutToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
            }
            if (richTextBox1.Text.Length > 0)
            {
                findToolStripMenuItem.Enabled = true;

            }
            else
            {
                findToolStripMenuItem.Enabled = false;

            }
        }
    }
}
