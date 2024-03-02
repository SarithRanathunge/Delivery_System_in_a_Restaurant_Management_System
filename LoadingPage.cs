using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PizzaMaster_GADProject_
{
    public partial class LoadingPage : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
            (
                int nLeftRect,
                int nTopRect,
                int RightRect,
                int nBottomRect,
                int nWidthEllipse,
                int nHeightEllipse
            );
        public LoadingPage()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            ProgressBar.Value = 0;
        }

        public void LoadingFormClose()
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ProgressBar.Value += 1;
            lblPresentage.Text = ProgressBar.Value.ToString() + "%";

            if (ProgressBar.Value == 100)
            {
                timer1.Enabled = false;
                this.Hide();
                LoginPage log = new LoginPage();
                log.Show();
            }
        }

    }
}
