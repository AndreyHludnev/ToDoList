using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Tulpep.NotificationWindow;
using System.Net.Mail;
using System.Net;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private PopupNotifier notifier = null;
        bool havemail = false;
        string email = null;
        public Form1()
        {
            InitializeComponent();
            timer1.Start();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadRes(listBox1);
            notifier = new PopupNotifier();
            notifier.TitleColor = Color.DarkRed;
            notifier.TitleText = "ToDoList";
            notifier.HeaderColor = Color.Gray;
            notifier.BodyColor = Color.White;
            notifier.ContentColor = Color.DarkRed;

            textBox1.Text = listBox1.Items.Count.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Text = "ToDoList";
            form2.dateTimePicker1.Value = DateTime.Now;
            if (form2.ShowDialog() == DialogResult.OK)
            {
                Deal deal = new Deal();
                deal.Text = form2.textBox1.Text;
                deal.Description = form2.textBox2.Text;
                if (form2.checkBox1.Checked)
                    deal.IsData = false;
                else
                {
                    deal.IsData = true;
                    deal.Term = form2.dateTimePicker1.Value;
                }
                listBox1.Items.Add(deal);
            }
            textBox1.Text = listBox1.Items.Count.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                MessageBox.Show("Поздравляем! Вы выполнили задачу!", "Информация");
                textBox1.Text = listBox1.Items.Count.ToString();
            }
            else
                MessageBox.Show("Не выбран элемент в списке", "Информация");

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                Deal deal = listBox1.Items[i] as Deal;
                if (deal.Term.Year == DateTime.Now.Year && deal.Term.Month == DateTime.Now.Month && deal.Term.Day == DateTime.Now.Day)
                {
                    if (havemail)
                    {
                        SendMail mail = new SendMail(deal, email);
                        mail.SendMessage();
                    }
                    SendPopit pop = new SendPopit(notifier, deal);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                Deal deal = listBox1.Items[listBox1.SelectedIndex] as Deal;
                Form3 form3 = new Form3();
                form3.Text = "ToDoList";
                form3.textBox1.Text = deal.Description;
                if (deal.IsData)
                    form3.textBox2.Text = deal.Term.ToLongDateString().ToString();
                else
                {
                    form3.label2.Visible = false;
                    form3.textBox2.Visible = false;
                }
                form3.ShowDialog();

            }
            else
                MessageBox.Show("Не выбран элемент в списке", "Информация");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                Deal deal = listBox1.SelectedItem as Deal;
                Form2 form2 = new Form2();
                form2.Text = "ToDoList";
                form2.button1.Text = "Изменить";
                form2.textBox1.Text = deal.Text;
                form2.textBox2.Text = deal.Description;
                if (deal.IsData)
                    form2.dateTimePicker1.Value = deal.Term;
                else
                    form2.checkBox1.Checked = true;
                if (form2.ShowDialog() == DialogResult.OK)
                {
                    deal.Text = form2.textBox1.Text;
                    deal.Description = form2.textBox2.Text;
                    if (form2.checkBox1.Checked)
                        deal.IsData = false;
                    else
                    {
                        deal.IsData = true;
                        deal.Term = form2.dateTimePicker1.Value;
                    }
                    listBox1.Items[listBox1.SelectedIndex] = deal;
                }

            }
            else
                MessageBox.Show("Не выбран элемент в списке", "Информация");
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
                notifyIcon1.Visible = false;
            }
        }

        static void SaveRes(ListBox listbox)
        {
            if (listbox.Items.Count > 0)
            {
                using (StreamWriter sw = new StreamWriter(File.Open(@"C:\Temp\savetodo.txt", FileMode.Create)))
                {
                    for (int i = 0; i < listbox.Items.Count; i++)
                    {
                        Deal deal = listbox.Items[i] as Deal;
                        sw.WriteLine(deal.Text);
                        sw.WriteLine(deal.IsData);
                        if (deal.IsData)
                            sw.WriteLine(deal.Term.ToString());
                        sw.WriteLine(deal.Description);
                        
                    }
                }
            }
            else
            {
                File.Delete(@"C:\Temp\savetodo.txt");
            }
        }
        static void LoadRes(ListBox listbox)
        {
            if (File.Exists(@"C:\Temp\savetodo.txt"))
            {
                using (StreamReader sr = new StreamReader(File.Open(@"C:\Temp\savetodo.txt", FileMode.Open)))
                {
                    while (!sr.EndOfStream)
                    {
                        Deal deal = new Deal();

                        deal.Text = sr.ReadLine();
                        deal.IsData = Convert.ToBoolean(sr.ReadLine());
                        if (deal.IsData)
                            deal.Term = Convert.ToDateTime(sr.ReadLine().Trim());
                        deal.Description = sr.ReadToEnd();
                        listbox.Items.Add(deal);
                    }
                }
            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveRes(listBox1);
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
