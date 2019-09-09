using System;
using System.Windows.Forms;
using static System.DateTime;

namespace _6._2
{
    public partial class Clock : Form
    {
        public Clock()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Function that shows current time
        /// </summary>
        private void TimeTick(object sender, EventArgs e)
        {
            label1.Text = Now.Hour.ToString("00") + ":" + Now.Minute.ToString("00") + ":" + Now.Second.ToString("00");
        }
    }
}
