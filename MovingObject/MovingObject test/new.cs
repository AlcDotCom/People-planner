using MovingObject_test.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovingObject_test
{
    public partial class @new : Form
    {
        public @new()
        {
            InitializeComponent();
            textBox1.Select();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 nextForm = new Form1();
            Hide();
            nextForm.ShowDialog();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           Settings.Default.pridavany =  textBox1.Text;
           Settings.Default.Save();

            if(Settings.Default.meno1 == "")  //////////////////////////////////////pre všetky
            {
                Settings.Default.meno1 = Settings.Default.pridavany;
                Settings.Default.X = "71";
                Settings.Default.Y = "6";
                Settings.Default.Save();

                var w = new Form() { Size = new Size(0, 0) };
                Task.Delay(TimeSpan.FromSeconds(1))
                .ContinueWith((t) => w.Close(), TaskScheduler.FromCurrentSynchronizationContext());
                MessageBox.Show(w, "Pracovník: " + Settings.Default.pridavany + " bol pridaný");

                textBox1.Text = "";
            }
            else if (Settings.Default.meno2 == "")
            {
                Settings.Default.meno2 = Settings.Default.pridavany;
                Settings.Default.X2 = "71";
                Settings.Default.Y2 = "6";
                Settings.Default.Save();

                var w = new Form() { Size = new Size(0, 0) };
                Task.Delay(TimeSpan.FromSeconds(1))
                .ContinueWith((t) => w.Close(), TaskScheduler.FromCurrentSynchronizationContext());
                MessageBox.Show(w, "Pracovník: " + Settings.Default.pridavany + " bol pridaný");

                textBox1.Text = "";
            }
            else if (Settings.Default.meno3 == "")
            {
                Settings.Default.meno3 = Settings.Default.pridavany;
                Settings.Default.X3 = "71";
                Settings.Default.Y3 = "6";
                Settings.Default.Save();

                var w = new Form() { Size = new Size(0, 0) };
                Task.Delay(TimeSpan.FromSeconds(1))
                .ContinueWith((t) => w.Close(), TaskScheduler.FromCurrentSynchronizationContext());
                MessageBox.Show(w, "Pracovník: " + Settings.Default.pridavany + " bol pridaný");

                textBox1.Text = "";
            }
            else if (Settings.Default.meno4 == "")  //
            {
                Settings.Default.meno4 = Settings.Default.pridavany;  //
                Settings.Default.X4 = "71";  //
                Settings.Default.Y4 = "6";  //
                Settings.Default.Save();

                var w = new Form() { Size = new Size(0, 0) };
                Task.Delay(TimeSpan.FromSeconds(1))
                .ContinueWith((t) => w.Close(), TaskScheduler.FromCurrentSynchronizationContext());
                MessageBox.Show(w, "Pracovník: " + Settings.Default.pridavany + " bol pridaný");

                textBox1.Text = "";
            }




            else
            {
                MessageBox.Show("Bol dosiahnutý maximálny počet pracovníkov");
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.TextLength > 0)
            { button1.Visible = true; }
            else
            { button1.Visible = false; }
        }
    }
}
