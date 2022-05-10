using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                try
                {
                    if (textBox1.Text.Trim() == "")
                    {
                        throw new Exception("Напишите вашу задачу!");
                    }
                    if (textBox2.Text.Trim() == "")
                    {
                        textBox2.Text = "Нет описания";
                    }
                }
                catch (Exception ex)
                {
                    e.Cancel = true;
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dateTimePicker1_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                label2.Visible = false;
                dateTimePicker1.Visible = false;
            }
            else
            {
                label2.Visible = true;
                dateTimePicker1.Visible = true;
            }
        }
    }
}
