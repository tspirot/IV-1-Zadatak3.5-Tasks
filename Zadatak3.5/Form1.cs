using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zadatak3._5
{
    public partial class Form1 : Form
    {
        bool stop = false;//animacija zaustavljena
        int x1, y1, o1; // koordinate i dimenz. trougla
        Color boja1; // boja trougla
        Random r = new Random(); // gen sluc boje
        Thread t1; // nit za metodu
        public Form1()
        {
            InitializeComponent();
            t1 = new Thread(new ThreadStart(Run1));
            t1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            x1 = 0;
            y1 = ClientRectangle.Height / 2;
            o1 = 120;
            //nijansa zelene boje - malo crvene i plave, puno zelene
            boja1 = Color.FromArgb(r.Next(160), r.Next(170, 256), r.Next(160));
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            //if (!stop) return;
            try
            {
                // pokretanje niti koristeci metodu Start()
                t1 = new Thread(new ThreadStart(Run1));
                t1.Start();
                stop = false;
            }
            catch (ThreadStateException ex)
            {
                MessageBox.Show("Nit nije prekinuta");
            }
        }

        private void buttonSuspend_Click(object sender, EventArgs e)
        {
            try
            {
                // zaustavljanje iyvrsavanja niti koristeci metodu Suspend()
                t1.Suspend();
            }
            catch (ThreadStateException ex)
            {
                MessageBox.Show("Nit nije pokrenuta");
            }
        }

        private void buttonResume_Click(object sender, EventArgs e)
        {
            try
            {
                // nastavak izvrsavanja niti koristeci metodu Resume()
                t1.Resume();
            }
            catch (ThreadStateException ex)
            {
                MessageBox.Show("Nit nije pauzirana");
            }
        }

        private void buttonAbort_Click(object sender, EventArgs e)
        {
            try
            {
                // zaustavljanje niti koristeci metodu Abort()
                t1.Abort();
                stop = true;
            }
            catch (ThreadStateException ex)
            {
                MessageBox.Show("Nit je zaustavljena");
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen olovka1 = new Pen(Color.Green, 5);
            SolidBrush cetka1 = new SolidBrush(boja1);
            Point p1 = new Point(x1, y1 - o1 / 2);
            Point p2 = new Point(x1 + o1 / 2, y1);
            Point p3 = new Point(x1, y1 + o1 / 2);
            Point[] p = { p1, p2, p3 };
            g.FillPolygon(cetka1, p);
            g.DrawPolygon(olovka1, p);
            base.OnPaint(e);
        }
        public void Run1()
        {
            x1 = 0;
            while (!stop)
            {
                if (x1 < ClientRectangle.Width)
                    x1 += 5;
                else
                {
                    x1 = 0;
                    boja1 = Color.FromArgb(r.Next(160), r.Next(170, 256), r.Next(160));
                }
                Invalidate(); // Poništava celu površinu kontrole i dovodi do ponovnog
                Thread.Sleep(30);
            }
        }
    }
}
