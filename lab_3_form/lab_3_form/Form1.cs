using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Imaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace lab_3_form
{
    public partial class Form1 : Form
    {
        
        private int MaxCount = 10;
        private int countDown = 3;
        private int tabCount = 1;
        private int onScreenCount = 0;
        private Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            this.startButton.Visible = false;
            this.countDownLabel.Visible = true;
            this.countDownLabel.Text = countDown.ToString();
            this.timer1.Start();
          
        }

        private void timer_Tick(object sender, EventArgs e)
        { 
            this.countDown--;
            if (countDown > 0)
            {
                this.countDownLabel.Text = countDown.ToString();
               
            } else if (countDown == 0)
            {
                this.countDownLabel.Text = "Cтарт";
                
               
                //this.timer1.Stop();
            } else
            { 
                this.countDownLabel.Visible = false;
                this.timer1.Stop();
                this.timer2.Start();
            }
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //this.startButton.Visible = true;
            Panel panel = new Panel();
            if(rand.Next()%2 == 0)
            {
                panel.Size = new Size(200, 50);
            }
            else
            {
                panel.Size = new Size(50, 200);
            }
            int x = rand.Next() % (this.Width - panel.Width);
            int y = rand.Next() % (this.Height - panel.Height);
            panel.Location = new Point(x, y);
            panel.BackColor = Color.FromArgb(rand.Next());
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.TabIndex = tabCount;
            panel.MouseClick += panel_Click;
            
            this.Controls.Add(panel);
            tabCount++;
            onScreenCount++;
            if (onScreenCount == MaxCount*2)
            {
                Label lose = new Label();
                lose.Text = "Вы проиграли";
                lose.Font = new Font(lose.Font.FontFamily, 30);
                lose.AutoSize = true;

                this.Controls.Clear();
                this.Controls.Add(lose);
                this.timer2.Stop();
            }
        }
    
        private void panel_Click(object sender, EventArgs e)
        {
            int controllsCount = this.Controls.Count;
            for(int i =0; i < controllsCount; i ++)
            {
                if (((Panel)sender).Bounds.IntersectsWith(this.Controls[i].Bounds) &&
                    (((Panel)sender).TabIndex > this.Controls[i].TabIndex) && 
                    this.Controls[i].Visible)  return;
            }
            ((Panel)sender).Visible = false;
            onScreenCount--;
            if (onScreenCount == 0)
            {
                this.timer2.Stop();
                Label win = new Label();
                win.Text = "Вы победили!";
                win.Font = new Font(win.Font.FontFamily, 30);
                win.Location = new Point(100, 175);
                win.AutoSize = true;
                win.BackColor = Color.AliceBlue;
                this.Controls.Add(win);
            }
        }
    }

}
