using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace praktika_dot
{
    public partial class Form1 : Form
    {
        int width, height; 
        static Graphics g;
        static Timer moveTimer;
        int dotX, dotY;   
        int dotR = 10;
        int deltaX = 5;
        int deltaY = 5;
        Color dotColor = Color.Black;
        Color foreColor = Color.AliceBlue;
        SolidBrush dotBrush;     
        SolidBrush foreBrush;      
        bool animate = false;
        bool paint = false;
        int timerInterval = 20;

        public Form1()
        {
            InitializeComponent();

            width = Canvas.Width;
            height = Canvas.Height;
            Canvas.Image = new Bitmap(width, height);
            g = Graphics.FromImage(Canvas.Image);
            dotX = width / 30;
            dotY = height / 2;
            dotBrush = new SolidBrush(dotColor);
            foreBrush = new SolidBrush(foreColor);
            g.FillRectangle(foreBrush, 0, 0, width, height);
            moveTimer = new Timer();        
            moveTimer.Interval = timerInterval;     
            moveTimer.Tick += MoveTimer_Tick;                      
        }

        private void PaintDot()       
        {
            g.FillEllipse(dotBrush, dotX - dotR, dotY - dotR, dotR * 2, dotR * 2);
        }

        private void ClearDot()        
        {
            g.FillEllipse(foreBrush, dotX - dotR - 1, dotY - dotR - 1, dotR * 2 + 2, dotR * 2 + 2);
        }

        private void MoveTimer_Tick(object sender, EventArgs e)
        {
            ClearDot();   

            if (dotX + deltaX >= width - dotR || dotX + deltaX <= dotR)            
                deltaX *= -1;
            if (dotY + deltaY >= height - dotR || dotY + deltaY <= dotR)
                deltaY *= -1;

            dotX += deltaX;
            dotY += deltaY;

            PaintDot();

            Canvas.Refresh();
        }

        private void btnPaint_Click(object sender, EventArgs e)
        {
            if (paint) return; 
            
            PaintDot();
            Canvas.Refresh();
            paint = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (!paint) return;   

            if (animate)
            {
                MessageBox.Show("Сначала остановите точку!");
                return;
            }

            ClearDot();
            Canvas.Refresh();
            paint = false;
        }

        private void btnAnimate_Click(object sender, EventArgs e)
        {
            if (!paint)
            {
                MessageBox.Show("Для анимации точка должна быть отрисована!");
                return;
            }

            if (!animate)
            {
                moveTimer.Start();     
                animate = true;
                btnAnimate.Text = "Остановить";
            }
            else
            {
                moveTimer.Stop();
                animate = false;
                btnAnimate.Text = "Оживить";
            }
        }
    }
}
