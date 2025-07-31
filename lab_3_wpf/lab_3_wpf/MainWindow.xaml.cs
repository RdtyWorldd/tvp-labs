using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Timers;
using System.Windows.Threading;

namespace lab_3_wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int countDown = 3;
        private int maxCount = 20;
        private Random random = new Random();
        private DispatcherTimer timer1 = new DispatcherTimer();
        private DispatcherTimer timer2 = new DispatcherTimer();


        public MainWindow()
        {
            InitializeComponent();
            timer1.Interval = TimeSpan.FromMilliseconds(1000);
            timer2.Interval = TimeSpan.FromMilliseconds(500);
            timer1.Tick += Timer_Tick;
            timer2.Tick += Timer2_Tick;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Children.Remove((Button)sender);
            countDownLabel.Content = countDown;
            countDownLabel.Visibility = Visibility.Visible;
            timer1.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.countDown--;
            if (countDown > 0)
            {
                this.countDownLabel.Content = countDown.ToString();

            }
            else if (countDown == 0)
            {
                this.countDownLabel.Content = "Cтарт";
            }
            else
            {
                countDownLabel.Visibility = Visibility.Hidden;
                this.timer1.Stop();
                this.timer2.Start();
            }

        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            CreateRectangles(1);
            if(mainGrid.Children.Count == 2 * maxCount+1)
            {
                countDownLabel.Content = "Вы проиграли";
                countDownLabel.Background = new SolidColorBrush(Colors.AliceBlue);
                countDownLabel.Visibility = Visibility.Visible;
                timer2.Stop();
            }
        }

        private void CreateRectangles(int nRect)
        {
            for (int i = 0; i < nRect; i++)
            {
                Rectangle rect = new Rectangle();
                rect.Width = 200;
                rect.Height = 50;
                rect.HorizontalAlignment = HorizontalAlignment.Left;
                rect.VerticalAlignment = VerticalAlignment.Top;

                int x = random.Next((int)(this.Width - rect.Width));
                int y = random.Next((int)(this.Height - rect.Height));
                rect.Margin = new Thickness(x, y, 0, 0);
                rect.Stroke = Brushes.Black;
                byte r = (byte)random.Next(255);
                byte g = (byte)random.Next(255);
                byte b = (byte)random.Next(255);

                rect.Fill = new SolidColorBrush(Color.FromRgb(r, g, b));
                rect.MouseDown += Rect_MouseDown;
                this.
                mainGrid.Children.Add(rect);
            }
        }

        private void Rect_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle r = (Rectangle)sender;
            Rect rect = new Rect(r.Margin.Left, r.Margin.Top, r.Width, r.Height);
            for(int i =1; i < mainGrid.Children.Count; i++)
            {
                Rectangle rTmp = (Rectangle)mainGrid.Children[i];
                Rect tmp = new Rect(rTmp.Margin.Left, rTmp.Margin.Top, rTmp.MaxWidth, rTmp.Height);
                if (rect.IntersectsWith(tmp) && mainGrid.Children.IndexOf(r) < i) return;
            }
            mainGrid.Children.Remove(r);

            if(mainGrid.Children.Count == 1) {
                timer2.Stop();
                countDownLabel.Content = "Вы победили";
                countDownLabel.Background = new SolidColorBrush(Colors.AliceBlue);
                countDownLabel.Visibility = Visibility.Visible;
            }
        }
    }
}

