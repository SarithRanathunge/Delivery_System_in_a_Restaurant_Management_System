using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;

namespace PizzaMaster_GADProject_
{
    public partial class Map : Form
    {
        private WebBrowser webBrowser;

        public Map()
        {
            InitializeComponent();
            /*webBrowser = new WebBrowser();

            // Set the web browser's size.
            webBrowser.Size = new Size(300, 200);

            // Anchor the web browser to the top-left corner of the form.
            webBrowser.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            Map map = new Map();
            // Add the web browser to the form.
            map.Controls.Add(webBrowser);*/
            /*OnlineDelivery Delivery = new OnlineDelivery();
            Delivery.datavalue();*/

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("T");
            lblDate.Text = DateTime.Now.ToString("MMM dd yyyy");
            lblToday.Text = DateTime.Now.ToString("dddd");
        }

        private void Map_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void pictureBoxLogOut_Click(object sender, EventArgs e)
        {
            OnlineDelivery previours = new OnlineDelivery();
            previours.Show();
            this.Close();
        }
    }
}
