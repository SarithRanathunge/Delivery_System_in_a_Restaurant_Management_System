using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System.Net.NetworkInformation;
using System.Web;
using System.Net;
using System.Net.Mail;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace PizzaMaster_GADProject_
{
    public partial class OnlineDelivery : Form
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
        
        public OnlineDelivery()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;

        void Fillcombo()
        {
            con = new SqlConnection("Data Source=LAPTOP-0NK52FLT;Initial Catalog=GAD;Integrated Security=True");

            cmd = new SqlCommand("select * from Orders where Order_type='Delivery'", con);
            SqlDataReader reader;
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    cmbxBillNo.Items.Add(name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        /*private WebBrowser webBrowser;*/

        /*public void datavalue()
        {
            string road1 = txtSearchRoad1.Text;
            string road2 = txtSearchRoad2.Text;
            string city = txtSearchCity.Text;
            if (road1 != string.Empty && road2 != string.Empty && city != string.Empty)
            {
                webBrowser.Navigate("https://www.google.com"+ road1 + "," + "+" + road2 + "," + "+" + city + "+");
            }
            else if (road1 == string.Empty && road2 != string.Empty && city != string.Empty)
            {
                webBrowser.Navigate("https://www.google.com" + road2 + "," + "+" + city + "+");
            }
            else if (road2 == string.Empty && road1 != string.Empty && city != string.Empty)
            {
                webBrowser.Navigate("https://www.google.com" + road1 + "," + "+" + city + "+");
            }
            else if (city == string.Empty && road1 != string.Empty && road2 != string.Empty)
            {
                webBrowser.Navigate("https://www.google.com" + "," + "+" + road2 + "+");
            }
        }*/

        private void pictureBoxExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
           
        }

        private void pictureBoxLogOut_Click(object sender, EventArgs e)
        {
            LoginPage login = new LoginPage();
            login.Show();
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string road1 = txtSearchRoad1.Text;
            string road2 = txtSearchRoad2.Text;
            string city = txtSearchCity.Text;
            if (string.IsNullOrEmpty(cmbxBillNo.Text))
            {
                lblDiliveryNo.Text = "Please, Dilivery No cannot be blank.";
                lblCustomernameError.Text = string.Empty;
                lblEmailError.Text = string.Empty;
                lblTelephoneError.Text = string.Empty;
                lblAddressError.Text = string.Empty;
            }
            else if (string.IsNullOrEmpty(txtCustomerName.Text))
            {
                lblDiliveryNo.Text = string.Empty;
                lblCustomernameError.Text = "Please, Customer name cannot be blank.";
                lblEmailError.Text = string.Empty;
                lblTelephoneError.Text = string.Empty;
                lblAddressError.Text = string.Empty;
                txtCustomerName.Focus();
            }
            else if (txtEmail.Text != string.Empty)
            {
                lblDiliveryNo.Text = string.Empty;
                lblCustomernameError.Text = string.Empty;
                lblTelephoneError.Text = string.Empty;
                lblAddressError.Text = string.Empty;
                if (!Regex.IsMatch(txtEmail.Text, @"^[0-9a-zA-Z][\w\.-][a-zA-Z0-9]@[a-zA-Z0-9][\w\.-][a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                {
                    lblDiliveryNo.Text = string.Empty;
                    lblCustomernameError.Text = string.Empty;
                    lblAddressError.Text = string.Empty;
                    lblTelephoneError.Text = string.Empty;
                    lblEmailError.Text = "Enter a valid email. Ex:name@gmail.com";
                    txtEmail.Focus();
                }
                else
                {
                    lblEmailError.Text = string.Empty;
                    //Generate Email
                    string fromMail = "pizzamaster563@gmail.com";
                    string fromPassword = "zobxnsvwnehsfnzo";

                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(fromMail);
                    message.Subject = "Greetings " + txtCustomerName.Text + "!";
                    message.To.Add(new MailAddress(txtEmail.Text.Trim()));
                    message.Body = "<html><body> <h1>Hello " + txtCustomerName.Text + "</h1>" +
                    "<p>Dear Customer,</p>" +
                    "<p>Your order already completed. Thank you for joining with us</p></ body ></ html >";
                    message.IsBodyHtml = true;

                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential(fromMail, fromPassword),
                        EnableSsl = true,
                    };
                    smtpClient.Send(message);

                    //Generate Delivery Bill
                    DeliveryBillPrint billPrint = new DeliveryBillPrint();
                    billPrint.dnumber = cmbxBillNo.SelectedItem.ToString();
                    billPrint.cname = txtCustomerName.Text;
                    billPrint.ctelephone = txtTelephone.Text;
                    if (road1 != string.Empty && road2 != string.Empty && city != string.Empty)
                    {
                        billPrint.caddress = road1 + "," + road2 + "," + city;
                        if (GenarateQRCode.Image == null)
                        {
                            MessageBox.Show("Please, Genarate th QR code before print the bill.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            billPrint.qrcodeimage = GenarateQRCode.Image;
                            billPrint.ShowDialog();
                        }
                    }
                    else if (road1 == string.Empty && road2 != string.Empty && city != string.Empty)
                    {
                        billPrint.caddress = road2 + "," + city;
                        if (GenarateQRCode.Image == null)
                        {
                            MessageBox.Show("Please, Genarate th QR code before print the bill.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            billPrint.qrcodeimage = GenarateQRCode.Image;
                            billPrint.ShowDialog();
                        }
                    }
                    else if (road2 == string.Empty && road1 != string.Empty && city != string.Empty)
                    {
                        billPrint.caddress = road1 + "," + city;
                        if (GenarateQRCode.Image == null)
                        {
                            MessageBox.Show("Please, Genarate th QR code before print the bill.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            billPrint.qrcodeimage = GenarateQRCode.Image;
                            billPrint.ShowDialog();
                        }
                    }
                    else if (city == string.Empty && road1 != string.Empty && road2 != string.Empty)
                    {
                        billPrint.caddress = road1 + "," + road2;
                        if (GenarateQRCode.Image == null)
                        {
                            MessageBox.Show("Please, Genarate th QR code before print the bill.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            billPrint.qrcodeimage = GenarateQRCode.Image;
                            billPrint.ShowDialog();
                        }
                    }
                }
            }
            else if (txtEmail.Text == string.Empty)
            {
                if (string.IsNullOrEmpty(txtTelephone.Text))
                {
                    lblDiliveryNo.Text = string.Empty;
                    lblCustomernameError.Text = string.Empty;
                    lblEmailError.Text = string.Empty;
                    lblAddressError.Text = string.Empty;
                    lblTelephoneError.Text = "Please, Telephone cannot be blank.";
                    txtTelephone.Focus();
                }
                else if (txtTelephone.Text.Any(char.IsLetter))
                {
                    lblDiliveryNo.Text = string.Empty;
                    lblCustomernameError.Text = string.Empty;
                    lblEmailError.Text = string.Empty;
                    lblAddressError.Text = string.Empty;
                    lblTelephoneError.Text = "Telephone must have numbers only";
                    txtTelephone.Focus();
                }
                else if (txtTelephone.Text.Length < 10)
                {
                    lblDiliveryNo.Text = string.Empty;
                    lblCustomernameError.Text = string.Empty;
                    lblEmailError.Text = string.Empty;
                    lblAddressError.Text = string.Empty;
                    lblTelephoneError.Text = "TP must have 10 numbers";
                    txtTelephone.Focus();
                }
                else if (road1 == string.Empty && road2 == string.Empty && city == string.Empty)
                {
                    lblDiliveryNo.Text = string.Empty;
                    lblCustomernameError.Text = string.Empty;
                    lblEmailError.Text = string.Empty;
                    lblTelephoneError.Text = string.Empty;
                    lblAddressError.Text = "Please, Add the Customer address.";
                }
                else if (road1 == string.Empty && road2 == string.Empty && city != string.Empty)
                {
                    lblDiliveryNo.Text = string.Empty;
                    lblCustomernameError.Text = string.Empty;
                    lblEmailError.Text = string.Empty;
                    lblTelephoneError.Text = string.Empty;
                    lblAddressError.Text = "Please, Add details minimum two address rows.";
                }
                else if (road1 == string.Empty && road2 != string.Empty && city == string.Empty)
                {
                    lblDiliveryNo.Text = string.Empty;
                    lblCustomernameError.Text = string.Empty;
                    lblEmailError.Text = string.Empty;
                    lblTelephoneError.Text = string.Empty;
                    lblAddressError.Text = "Please, Add details minimum two address rows.";
                }
                else if (road1 != string.Empty && road2 == string.Empty && city == string.Empty)
                {
                    lblDiliveryNo.Text = string.Empty;
                    lblCustomernameError.Text = string.Empty;
                    lblEmailError.Text = string.Empty;
                    lblTelephoneError.Text = string.Empty;
                    lblAddressError.Text = "Please, Add details minimum two address rows.";
                }
                else if (road1 == string.Empty && road2 == string.Empty && city == string.Empty)
                {
                    lblDiliveryNo.Text = string.Empty;
                    lblCustomernameError.Text = string.Empty;
                    lblEmailError.Text = string.Empty;
                    lblTelephoneError.Text = string.Empty;
                    lblAddressError.Text = "Please, Add the Customer address.";
                }
                else
                {
                    lblDiliveryNo.Text = string.Empty;
                    lblCustomernameError.Text = string.Empty;
                    lblEmailError.Text = string.Empty;
                    lblTelephoneError.Text = string.Empty;
                    lblAddressError.Text = string.Empty;
                    //Generate Delivery Bill Witout Send Email
                    DeliveryBillPrint billPrint = new DeliveryBillPrint();
                    billPrint.dnumber = cmbxBillNo.SelectedItem.ToString();
                    billPrint.cname = txtCustomerName.Text;
                    billPrint.ctelephone = txtTelephone.Text;
                    if (road1 != string.Empty && road2 != string.Empty && city != string.Empty)
                    {
                        billPrint.caddress = road1 + "," + road2 + "," + city;
                        if (GenarateQRCode.Image == null)
                        {
                            MessageBox.Show("Please, Genarate th QR code before print the bill.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            billPrint.qrcodeimage = GenarateQRCode.Image;
                            billPrint.ShowDialog();
                        }
                    }
                    else if (road1 == string.Empty && road2 != string.Empty && city != string.Empty)
                    {
                        billPrint.caddress = road2 + "," + city;
                        if (GenarateQRCode.Image == null)
                        {
                            MessageBox.Show("Please, Genarate th QR code before print the bill.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            billPrint.qrcodeimage = GenarateQRCode.Image;
                            billPrint.ShowDialog();
                        }
                    }
                    else if (road2 == string.Empty && road1 != string.Empty && city != string.Empty)
                    {
                        billPrint.caddress = road1 + "," + city;
                        if (GenarateQRCode.Image == null)
                        {
                            MessageBox.Show("Please, Genarate th QR code before print the bill.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            billPrint.qrcodeimage = GenarateQRCode.Image;
                            billPrint.ShowDialog();
                        }
                    }
                    else if (city == string.Empty && road1 != string.Empty && road2 != string.Empty)
                    {
                        billPrint.caddress = road1 + "," + road2;
                        if (GenarateQRCode.Image == null)
                        {
                            MessageBox.Show("Please, Genarate th QR code before print the bill.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            billPrint.qrcodeimage = GenarateQRCode.Image;
                            billPrint.ShowDialog();
                        }
                    }

                }
            }
            /*else if (string.IsNullOrEmpty(txtTelephone.Text))
            {
                lblDiliveryNo.Text = string.Empty;
                lblCustomernameError.Text = string.Empty;
                lblEmailError.Text = string.Empty;
                lblAddressError.Text = string.Empty;
                lblTelephoneError.Text = "Please, Telephone cannot be blank.";
                txtTelephone.Focus();
            }
            else if (txtTelephone.Text.Any(char.IsLetter))
            {
                lblDiliveryNo.Text = string.Empty;
                lblCustomernameError.Text = string.Empty;
                lblEmailError.Text = string.Empty;
                lblAddressError.Text = string.Empty;
                lblTelephoneError.Text = "Telephone must have numbers only";
                txtTelephone.Focus();
            }
            else if (txtTelephone.Text.Length < 10)
            {
                lblDiliveryNo.Text = string.Empty;
                lblCustomernameError.Text = string.Empty;
                lblEmailError.Text = string.Empty;
                lblAddressError.Text = string.Empty;
                lblTelephoneError.Text = "TP must have 10 numbers";
                txtTelephone.Focus();
            }
            else if (road1 == string.Empty && road2 == string.Empty && city == string.Empty)
            {
                lblDiliveryNo.Text = string.Empty;
                lblCustomernameError.Text = string.Empty;
                lblEmailError.Text = string.Empty;
                lblTelephoneError.Text = string.Empty;
                lblAddressError.Text = "Please, Add the Customer address.";
            }
            else if (road1 == string.Empty && road2 == string.Empty && city != string.Empty)
            {
                lblDiliveryNo.Text = string.Empty;
                lblCustomernameError.Text = string.Empty;
                lblEmailError.Text = string.Empty;
                lblTelephoneError.Text = string.Empty;
                lblAddressError.Text = "Please, Add details minimum two address rows.";
            }
            else if (road1 == string.Empty && road2 != string.Empty && city == string.Empty)
            {
                lblDiliveryNo.Text = string.Empty;
                lblCustomernameError.Text = string.Empty;
                lblEmailError.Text = string.Empty;
                lblTelephoneError.Text = string.Empty;
                lblAddressError.Text = "Please, Add details minimum two address rows.";
            }
            else if (road1 != string.Empty && road2 == string.Empty && city == string.Empty)
            {
                lblDiliveryNo.Text = string.Empty;
                lblCustomernameError.Text = string.Empty;
                lblEmailError.Text = string.Empty;
                lblTelephoneError.Text = string.Empty;
                lblAddressError.Text = "Please, Add details minimum two address rows.";
            }
            else if (road1 == string.Empty && road2 == string.Empty && city == string.Empty)
            {
                lblDiliveryNo.Text = string.Empty;
                lblCustomernameError.Text = string.Empty;
                lblEmailError.Text = string.Empty;
                lblTelephoneError.Text = string.Empty;
                lblAddressError.Text = "Please, Add the Customer address.";
            }
            else
            {
                lblDiliveryNo.Text = string.Empty;
                lblCustomernameError.Text = string.Empty;
                lblEmailError.Text = string.Empty;
                lblTelephoneError.Text = string.Empty;
                lblAddressError.Text = string.Empty;
                //Generate Delivery Bill Witout Send Email
                DeliveryBillPrint billPrint = new DeliveryBillPrint();
                billPrint.dnumber = cmbxBillNo.SelectedItem.ToString();
                billPrint.cname = txtCustomerName.Text;
                billPrint.ctelephone = txtTelephone.Text;
                if (road1 != string.Empty && road2 != string.Empty && city != string.Empty)
                {
                    billPrint.caddress = road1 + "," + road2 + "," + city;
                    if (GenarateQRCode.Image == null)
                    {
                        MessageBox.Show("Please, Genarate th QR code before print the bill.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        billPrint.qrcodeimage = GenarateQRCode.Image;
                        billPrint.ShowDialog();
                    }
                }
                else if (road1 == string.Empty && road2 != string.Empty && city != string.Empty)
                {
                    billPrint.caddress = road2 + "," + city;
                    if (GenarateQRCode.Image == null)
                    {
                        MessageBox.Show("Please, Genarate th QR code before print the bill.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        billPrint.qrcodeimage = GenarateQRCode.Image;
                        billPrint.ShowDialog();
                    }
                }
                else if (road2 == string.Empty && road1 != string.Empty && city != string.Empty)
                {
                    billPrint.caddress = road1 + ","  + city;
                    if (GenarateQRCode.Image == null)
                    {
                        MessageBox.Show("Please, Genarate th QR code before print the bill.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        billPrint.qrcodeimage = GenarateQRCode.Image;
                        billPrint.ShowDialog();
                    }
                }
                else if (city == string.Empty && road1 != string.Empty && road2 != string.Empty)
                {
                    billPrint.caddress = road1 + "," + road2;
                    if (GenarateQRCode.Image == null)
                    {
                        MessageBox.Show("Please, Genarate th QR code before print the bill.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        billPrint.qrcodeimage = GenarateQRCode.Image;
                        billPrint.ShowDialog();
                    }
                }

            }*/
            
        }
            
        

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string road1 = txtSearchRoad1.Text;
            string road2 = txtSearchRoad2.Text;
            string city = txtSearchCity.Text;

            try
            {
                if (road1 == string.Empty && road2 == string.Empty && city == string.Empty)
                {
                    lblDiliveryNo.Text = string.Empty;
                    lblCustomernameError.Text = string.Empty;
                    lblEmailError.Text = string.Empty;
                    lblTelephoneError.Text = string.Empty;
                    lblAddressError.Text = "Please, Add the Customer address.";
                }
                else
                {
                    lblDiliveryNo.Text = string.Empty;
                    lblCustomernameError.Text = string.Empty;
                    lblEmailError.Text = string.Empty;
                    lblTelephoneError.Text = string.Empty;
                    lblAddressError.Text = string.Empty;
                    
                    StringBuilder address = new StringBuilder();
                    address.Append("https://maps.google.com/?q=");

                    if (road1 != string.Empty && road2 != string.Empty && city != string.Empty)
                    {
                        address.Append(road1 + "," + "+" + road2 + "," + "+" + city + "+");
                        string qrtest = address.ToString();
                        QRCodeEncoder enc = new QRCodeEncoder();
                        Bitmap qrCode = enc.Encode(qrtest);
                        GenarateQRCode.Image = qrCode as Image;
                    }
                    else if (road1 == string.Empty && road2 != string.Empty && city != string.Empty)
                    {
                        address.Append(road2 + "," + "+" + city + "+");
                        string qrtest = address.ToString();
                        QRCodeEncoder enc = new QRCodeEncoder();
                        Bitmap qrCode = enc.Encode(qrtest);
                        GenarateQRCode.Image = qrCode as Image;
                    }
                    else if ( road1 != string.Empty && road2 == string.Empty && city != string.Empty)
                    {
                        address.Append(road1 + "," + "+" + city + "+");
                        string qrtest = address.ToString();
                        QRCodeEncoder enc = new QRCodeEncoder();
                        Bitmap qrCode = enc.Encode(qrtest);
                        GenarateQRCode.Image = qrCode as Image;
                    }
                    else if (road1 != string.Empty && road2 != string.Empty && city == string.Empty)
                    {
                        address.Append(road1 + "," + "+" + road2 + "+");
                        string qrtest = address.ToString();
                        QRCodeEncoder enc = new QRCodeEncoder();
                        Bitmap qrCode = enc.Encode(qrtest);
                        GenarateQRCode.Image = qrCode as Image;
                    }
                    else if (road1 == string.Empty && road2 == string.Empty && city != string.Empty)
                    {
                        lblAddressError.Text = "Please, Add details minimum two address rows.";
                    }
                    else if (road1 == string.Empty && road2 != string.Empty && city == string.Empty)
                    {
                        lblAddressError.Text = "Please, Add details minimum two address rows.";
                    }
                    else if (road1 != string.Empty && road2 == string.Empty && city == string.Empty)
                    {
                        lblAddressError.Text = "Please, Add details minimum two address rows.";
                    }
                    else if (road1 == string.Empty && road2 == string.Empty && city == string.Empty)
                    {
                        lblAddressError.Text = "Please, Add the Customer address.";
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        private void btnMap_Click(object sender, EventArgs e)
        {

            string road1 = txtSearchRoad1.Text;
            string road2 = txtSearchRoad2.Text;
            string city = txtSearchCity.Text;
            if (road1 != string.Empty && road2 != string.Empty && city != string.Empty)
            {
                System.Diagnostics.Process.Start("https://maps.google.com/?q=" + road1 + "," + "+" + road2 + "," + "+" + city + "+");
            }
            else if (road1 == string.Empty && road2 != string.Empty && city != string.Empty)
            {
                System.Diagnostics.Process.Start("https://maps.google.com/?q=" + road2 + "," + "+" + city + "+");
            }
            else if (road2 == string.Empty && road1 != string.Empty && city != string.Empty)
            {
                System.Diagnostics.Process.Start("https://maps.google.com/?q=" + road1 + "," + "+" + city + "+");
            }
            else if (city == string.Empty && road1 != string.Empty && road2 != string.Empty)
            {
                System.Diagnostics.Process.Start("https://maps.google.com/?q=" + road1 + "," + "+" + road2 + "+");
            }
            else if (road1 == string.Empty && road2 == string.Empty && city != string.Empty)
            {
                lblAddressError.Text = "Please, Add details minimum two address rows.";
            }
            else if (road1 == string.Empty && road2 != string.Empty && city == string.Empty)
            {
                lblAddressError.Text = "Please, Add details minimum two address rows.";
            }
            else if (road1 != string.Empty && road2 == string.Empty && city == string.Empty)
            {
                lblAddressError.Text = "Please, Add details minimum two address rows.";
            }
            else if (road1 == string.Empty && road2 == string.Empty && city == string.Empty)
            {
                lblAddressError.Text = "Please, Add the Customer address.";
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=LAPTOP-0NK52FLT;Initial Catalog=GAD;Integrated Security=True");
            lblTime.Text = DateTime.Now.ToString("T");
            lblDate.Text = DateTime.Now.ToString("MMM dd yyyy");
            lblToday.Text = DateTime.Now.ToString("dddd");
            
        }

        private void OnlineDelivery_Load(object sender, EventArgs e)
        {
            timer1.Start();
            con = new SqlConnection("Data Source=LAPTOP-0NK52FLT;Initial Catalog=GAD;Integrated Security=True");

            Fillcombo();
        }

        private void btnMapForm_Click(object sender, EventArgs e)
        {
            /*Map map = new Map();
            map.Show();*/
            cmbxBillNo.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTelephone.Text = string.Empty;
            txtSearchCity.Text = string.Empty;
            txtSearchRoad1.Text = string.Empty;
            txtSearchRoad2.Text = string.Empty;
            GenarateQRCode.Image = null;
        }

        private void cmbxBillNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbxBillNo.Text != string.Empty)
            {
                con = new SqlConnection("Data Source=LAPTOP-0NK52FLT;Initial Catalog=GAD;Integrated Security=True");
                cmd = new SqlCommand("select orders.Order_Bill_no,Customers.Customer_tel,Customers.Customer_name, Customers.Customer_address from orders,Customers where orders.Customer_tel=Customers.Customer_tel and orders.Order_Bill_no='" + cmbxBillNo.Text+"' and orders.Order_type='Delivery'", con);
                SqlDataReader myreader;
                try
                {
                    con.Open();
                    myreader = cmd.ExecuteReader();
                    while (myreader.Read()) 
                    {
                        txtTelephone.Text = myreader["Customer_tel"].ToString();
                        txtCustomerName.Text = myreader["Customer_name"].ToString();
                        txtSearchRoad1.Text = myreader["Customer_address"].ToString();
                    }
                }
                catch (Exception ex) 
                {
                    MessageBox.Show(ex.ToString());
                }
                finally 
                {
                    con.Close(); 
                }
            }
            else
            {
                MessageBox.Show("Please, Select the Delivery no", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
