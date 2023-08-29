using MovingObject_test.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Threading;

namespace MovingObject_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox4.Visible = false;
            pictureBox3.Visible = true;
            pictureBox1.Image = Resources.pozadie; 

            if (Settings.Default.meno1.Length > 0) // 1. 
            { pictureBox2.Visible = true; }

            if (Settings.Default.meno2.Length > 0)  // 2.
            { pictureBox5.Visible = true; }

            if (Settings.Default.meno3.Length > 0)  // 3.
            { pictureBox6.Visible = true; }

            if (Settings.Default.meno4.Length > 0)  // 4.
            { pictureBox7.Visible = true; }

            try   
            {
                pictureBox2.Left = Int32.Parse(Settings.Default.X);  //1.
                pictureBox2.Top = Int32.Parse(Settings.Default.Y);
                pictureBox5.Left = Int32.Parse(Settings.Default.X2);  //2.
                pictureBox5.Top = Int32.Parse(Settings.Default.Y2);
                pictureBox6.Left = Int32.Parse(Settings.Default.X3);  //3.
                pictureBox6.Top = Int32.Parse(Settings.Default.Y3);
                pictureBox7.Left = Int32.Parse(Settings.Default.X4);  //4.
                pictureBox7.Top = Int32.Parse(Settings.Default.Y4);

            }
            catch (Exception) { }
        }
        private Point MouseDownLocation;

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox3.Visible = false;
            pictureBox4.Visible = true;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pictureBox4.Visible = false;
            pictureBox3.Visible = true;
        }
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Dispose();
            @new nextForm = new @new();
            Hide();
            nextForm.ShowDialog();
            Close();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Dispose();
            delete nextForm = new delete();
            Hide();
            nextForm.ShowDialog();
            Close();
        }

        public new void Dispose()
        {
            pictureBox1.Image.Dispose();
            pictureBox1.Image = null;
            pictureBox3.Image.Dispose();
            pictureBox3.Image = null;
            pictureBox4.Image.Dispose();
            pictureBox4.Image = null;
            pictureBox8.Image.Dispose();
            pictureBox8.Image = null;
            pictureBox9.Image.Dispose();
            pictureBox9.Image = null;
            pictureBox10.Image.Dispose();
            pictureBox10.Image = null;
        }
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            CaptureMyScreen();
            var w = new Form() { Size = new Size(0, 0) };
            Task.Delay(TimeSpan.FromSeconds(2)).ContinueWith((t) => w.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            MessageBox.Show(w, "Záloha uložená (fotka obrazovky) a uložená na vašu pracovnú plochu");
        }
        private void CaptureMyScreen()
        {
            try
            {
                Bitmap captureBitmap = new Bitmap(1920, 1080, PixelFormat.Format32bppArgb);                                                           
                Rectangle captureRectangle = Screen.AllScreens[0].Bounds;  //Creating a Rectangle object which will capture our Current Screen
                Graphics captureGraphics = Graphics.FromImage(captureBitmap);  //Creating a New Graphics Object
                captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);  //Copying Image from The Screen

                string datum = DateTime.Now.ToString("yyyy MM dd _ HH mm ss");
                string ulozisko = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                captureBitmap.Save(ulozisko + "/" + "Záloha rozloženia operátorov " + datum + ".jpg", ImageFormat.Jpeg);                             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //////////////////////////////////////////////////////////////// 1.
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e) //pociatocna pozícia 1.
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }
        private void pictureBox2_MouseMove(object sender, MouseEventArgs e) //pozícia v pohybe 1.
        {

            if (e.Button == MouseButtons.Left)
            {
                pictureBox2.Left = e.X + pictureBox2.Left - MouseDownLocation.X;
                pictureBox2.Top = e.Y + pictureBox2.Top - MouseDownLocation.Y;
            }

        }
        private void pictureBox2_MouseCaptureChanged(object sender, EventArgs e) //ulozi zmenenu poziciu 1. po uvoľnení lavého tlačidla myši
        {
            if (pictureBox2.Left > 70 && pictureBox2.Left < 880 && pictureBox2.Top > 5 && pictureBox2.Top < 500)   //Upraviť limity podľa rozlíšenia!!!
            {
                Settings.Default.X = pictureBox2.Left.ToString();
                Settings.Default.Y = pictureBox2.Top.ToString();
                Settings.Default.Save();
            }
            else
            {
                pictureBox2.Left = Convert.ToInt32(Settings.Default.X);
                pictureBox2.Top = Convert.ToInt32(Settings.Default.Y);
            }
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)   //meno 1.
        {
            //Popis do Picture box
            using (Font myFont = new Font("Arial", 10))
            {
                int DlzkaStringVpixeloch = TextRenderer.MeasureText(Settings.Default.meno1, myFont).Width;
                pictureBox2.Size = new Size(DlzkaStringVpixeloch, 22);  //dĺžka picturebox a výška
                e.Graphics.DrawString(Settings.Default.meno1, myFont, Brushes.White, new Point(2, 2));  //lokalita začiatku textu v picturebox
            }
            //sprav ovál z picture box
            Rectangle r = new Rectangle(0, 0, pictureBox2.Width, pictureBox2.Height);
            GraphicsPath gp = new GraphicsPath();
            int d = 18;  //hodnota skosenia hrán
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            pictureBox2.Region = new Region(gp);
        }

        private void pictureBox5_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }

        private void pictureBox5_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pictureBox5.Left = e.X + pictureBox5.Left - MouseDownLocation.X;
                pictureBox5.Top = e.Y + pictureBox5.Top - MouseDownLocation.Y;
            }
        }

        private void pictureBox5_MouseCaptureChanged(object sender, EventArgs e)
        {
            if (pictureBox5.Left > 70 && pictureBox5.Left < 880 && pictureBox5.Top > 5 && pictureBox5.Top < 500)   //Upraviť limity podľa rozlíšenia!!!
            {
                Settings.Default.X2 = pictureBox5.Left.ToString();
                Settings.Default.Y2 = pictureBox5.Top.ToString();
                Settings.Default.Save();
            }
            else
            {
                pictureBox5.Left = Convert.ToInt32(Settings.Default.X2);
                pictureBox5.Top = Convert.ToInt32(Settings.Default.Y2);
            }
        }

        private void pictureBox5_Paint(object sender, PaintEventArgs e)
        {
            using (Font myFont = new Font("Arial", 10))
            {
                int DlzkaStringVpixeloch = TextRenderer.MeasureText(Settings.Default.meno2, myFont).Width;
                pictureBox5.Size = new Size(DlzkaStringVpixeloch, 22);
                e.Graphics.DrawString(Settings.Default.meno2, myFont, Brushes.White, new Point(2, 2));
            }
            Rectangle r = new Rectangle(0, 0, pictureBox5.Width, pictureBox5.Height);
            GraphicsPath gp = new GraphicsPath();
            int d = 18;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            pictureBox5.Region = new Region(gp);
        }

        //////////////////////////////////////////////////////////////// 3.
        private void pictureBox6_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }

        private void pictureBox6_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pictureBox6.Left = e.X + pictureBox6.Left - MouseDownLocation.X;  // //
                pictureBox6.Top = e.Y + pictureBox6.Top - MouseDownLocation.Y;   // //
            }
        }

        private void pictureBox6_MouseCaptureChanged(object sender, EventArgs e)
        {
            if (pictureBox6.Left > 70 && pictureBox6.Left < 880 && pictureBox6.Top > 5 && pictureBox6.Top < 500)   //Upraviť limity podľa rozlíšenia!!!
            {
                Settings.Default.X3 = pictureBox6.Left.ToString();  // //
                Settings.Default.Y3 = pictureBox6.Top.ToString();  // //
                Settings.Default.Save();
            }
            else
            {
                pictureBox6.Left = Convert.ToInt32(Settings.Default.X3);
                pictureBox6.Top = Convert.ToInt32(Settings.Default.Y3);
            }
        }

        private void pictureBox6_Paint(object sender, PaintEventArgs e)
        {
            using (Font myFont = new Font("Arial", 10))
            {
                int DlzkaStringVpixeloch = TextRenderer.MeasureText(Settings.Default.meno3, myFont).Width;
                pictureBox6.Size = new Size(DlzkaStringVpixeloch, 22); //
                e.Graphics.DrawString(Settings.Default.meno3, myFont, Brushes.White, new Point(2, 2)); //
            }
            Rectangle r = new Rectangle(0, 0, pictureBox6.Width, pictureBox6.Height); // //
            GraphicsPath gp = new GraphicsPath();
            int d = 18;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            pictureBox6.Region = new Region(gp);  //
        }
        //////////////////////////////////////////////////////////////// 4.
        private void pictureBox7_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }

        private void pictureBox7_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pictureBox7.Left = e.X + pictureBox7.Left - MouseDownLocation.X;  // //
                pictureBox7.Top = e.Y + pictureBox7.Top - MouseDownLocation.Y;   // //
            }
        }

        private void pictureBox7_MouseCaptureChanged(object sender, EventArgs e)
        {
            if (pictureBox7.Left > 70 && pictureBox7.Left < 880 && pictureBox7.Top > 5 && pictureBox7.Top < 500) // // // //
            {
                Settings.Default.X4 = pictureBox7.Left.ToString();  // //
                Settings.Default.Y4 = pictureBox7.Top.ToString();  // //
                Settings.Default.Save();
            }
            else
            {
                pictureBox7.Left = Convert.ToInt32(Settings.Default.X4);  // //
                pictureBox7.Top = Convert.ToInt32(Settings.Default.Y4);  // //
            }
        }

        private void pictureBox7_Paint(object sender, PaintEventArgs e)
        {
            using (Font myFont = new Font("Arial", 10))
            {
                int DlzkaStringVpixeloch = TextRenderer.MeasureText(Settings.Default.meno4, myFont).Width; //
                pictureBox7.Size = new Size(DlzkaStringVpixeloch, 22); //
                e.Graphics.DrawString(Settings.Default.meno4, myFont, Brushes.White, new Point(2, 2)); //
            }
            Rectangle r = new Rectangle(0, 0, pictureBox7.Width, pictureBox7.Height); // //
            GraphicsPath gp = new GraphicsPath();
            int d = 18;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            pictureBox7.Region = new Region(gp);  //
        }
    }
}
