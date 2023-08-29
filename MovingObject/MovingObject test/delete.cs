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
    public partial class delete : Form
    {
        public delete()
        {
            InitializeComponent();
            LoadCBX();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 nextForm = new Form1();
            Hide();
            nextForm.ShowDialog();
            Close();
        }
        private void cbxDesign_DrawItem(object sender, DrawItemEventArgs e)     ///formátovanie comboboxov - zarovnanie na stred
        {
            ComboBox cbx = sender as ComboBox;
            if (cbx != null)
            {
                e.DrawBackground();
                if (e.Index >= 0)
                {
                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;
                    Brush brush = new SolidBrush(cbx.ForeColor);
                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                        brush = SystemBrushes.HighlightText;
                    e.Graphics.DrawString(cbx.Items[e.Index].ToString(), cbx.Font, brush, e.Bounds, sf);
                }
            }
        }
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1)
            { button1.Visible = true; }
        }

        private void button1_Click(object sender, EventArgs e)  //Zmazať
        {
            Settings.Default.vymazavany = comboBox1.SelectedItem.ToString();
            Settings.Default.Save();

            if (comboBox1.SelectedItem.ToString() == Settings.Default.meno1) //////////////////////pre všetky
            {
                Settings.Default.meno1 = "";
                Settings.Default.Save();
            }
            if (comboBox1.SelectedItem.ToString() == Settings.Default.meno2)
            {
                Settings.Default.meno2 = "";
                Settings.Default.Save();
            }
            if (comboBox1.SelectedItem.ToString() == Settings.Default.meno3)
            {
                Settings.Default.meno3 = "";
                Settings.Default.Save();
            }
            if (comboBox1.SelectedItem.ToString() == Settings.Default.meno4)
            {
                Settings.Default.meno4 = "";
                Settings.Default.Save();
            }

            comboBox1.SelectedIndex = -1;
            button1.Visible = false;
            var w = new Form() { Size = new Size(0, 0) };
            Task.Delay(TimeSpan.FromSeconds(1))
            .ContinueWith((t) => w.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            MessageBox.Show(w, "Pracovník: " + Settings.Default.vymazavany + " bol vymazaný");
            comboBox1.Items.Clear();
            LoadCBX();
        }

        void LoadCBX()                                        //////////////////////pre všetky
        {
            if (Settings.Default.meno1.Length > 0)  //1.
            {
                comboBox1.Items.Add(Settings.Default.meno1);
            }
            if (Settings.Default.meno2.Length > 0)  //2.
            {
                comboBox1.Items.Add(Settings.Default.meno2);
            }
            if (Settings.Default.meno3.Length > 0)  //3.
            {
                comboBox1.Items.Add(Settings.Default.meno3);
            }
            if (Settings.Default.meno4.Length > 0)  //4.
            {
                comboBox1.Items.Add(Settings.Default.meno4);
            }


            comboBox1.IntegralHeight = false;   //max drop down items
            comboBox1.MaxDropDownItems = 15;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }
    }
}
