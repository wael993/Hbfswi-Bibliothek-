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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HBSFWI_Bibliotek
{
    /// <summary>
    /// Interaktionslogik für Willkommen.xaml
    /// </summary>
    public partial class Willkommen : Window
    {
        int startPos = 0;
        Login login = new Login();
        DispatcherTimer timer = new DispatcherTimer();
        public Willkommen()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Interval = TimeSpan.FromSeconds(0.007);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            startPos += 1;
            pro.Value = startPos;
            Timer1.Text = startPos + " %";
            if (startPos == 100)
            {
                timer.Stop();
                this.Close();

                login.Show();

            }


        }

    }
}