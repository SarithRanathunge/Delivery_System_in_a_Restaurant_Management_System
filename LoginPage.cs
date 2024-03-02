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
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static MetroFramework.Drawing.MetroPaint.BorderColor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PizzaMaster_GADProject_
{
    public partial class LoginPage : Form
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

        public LoginPage()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        SqlDataAdapter da;

        private void btnNightMode_Paint(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(37, 37, 38);
            cmbxEmployeeType.FillColor = Color.FromArgb(37, 37, 38);
            cmbxEmployeeType.ForeColor = Color.White;
            txtUsername.FillColor = Color.FromArgb(37, 37, 38);
            txtUsername.ForeColor = Color.White;
            txtPassword.FillColor = Color.FromArgb(37, 37, 38);
            txtPassword.ForeColor = Color.White;
            whiteline.BackColor = Color.White;
            label1.ForeColor = Color.White;
            label2.ForeColor = Color.White;
            label3.ForeColor = Color.White;
            Logopanel.BackgroundImage = Image.FromFile("C:\\Users\\Sarith\\Pictures\\GAD Project Logo\\Pizza Master Logo dark Theme.png");
            btnNightMode.SendToBack();
        }

        private void LoginPage_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=LAPTOP-0NK52FLT;Initial Catalog=GAD;Integrated Security=True");
        }

        private void logincheker(string emptype)
        {
            if (txtUsername.Text == string.Empty)
            {
                lblPasswordError.Text = string.Empty;
                lblUsernameError.Text = "Username cannot be empty";
            }
            else if (txtPassword.Text == string.Empty)
            {
                lblUsernameError.Text = string.Empty;
                lblPasswordError.Text = "Password cannot be empty";
            }
            else
            {
                string username;
                string password;
                cmd = new SqlCommand("select Username ,Password from EmployeeReg where Position='@emptype'", con);

                try
                {
                    con.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        username = reader["Username"].ToString();
                        password = reader["Password"].ToString();

                        if (txtUsername.Text == username && txtPassword.Text == password) //if username password is corerct
                        {
                            this.Hide();
                        }
                        else if (txtUsername.Text != username)
                        {
                            MessageBox.Show("Incorrect Username, Please Try Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (txtPassword.Text != password)
                        {
                            MessageBox.Show("Incorrect Password, Please Try Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
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
            

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (cmbxEmployeeType.Text == string.Empty)
            {
                lblUsernameError.Text = string.Empty;
                lblPasswordError.Text = string.Empty;
                lblEmployeeError.Text = "Employee Type cannot be empty.";
            }
            else if (cmbxEmployeeType.Text != string.Empty)
            {
                lblEmployeeError.Text = string.Empty;
                if (cmbxEmployeeType.SelectedIndex == 0)//manager
                {
                    logincheker("manager");
                    //show manager form
                }
                else if (cmbxEmployeeType.SelectedIndex == 1)//Cashier
                {

                    logincheker("Cashier");
                    //show cashier form
                }
                else if (cmbxEmployeeType.SelectedIndex == 2)//Delivery
                {

                    logincheker("Delivery");
                    OnlineDelivery deiliveyfrom = new OnlineDelivery();
                    deiliveyfrom.Show();
                    this.Hide();
                }
                else if (cmbxEmployeeType.SelectedIndex == 3)//Kitchen
                {

                    logincheker("Kitchen");
                    //show kitchen form

                }
            }
            
            
        }

        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            LoadingPage loading = new LoadingPage();
            loading.LoadingFormClose();
            Application.Exit();
        }

        private void btnLightMode_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            cmbxEmployeeType.FillColor = Color.White;
            cmbxEmployeeType.ForeColor = Color.Black;
            txtUsername.FillColor = Color.White;
            txtUsername.ForeColor = Color.Black;
            txtPassword.FillColor = Color.White;
            txtPassword.ForeColor = Color.Black;
            whiteline.BackColor = Color.Black;
            label1.ForeColor = Color.Black;
            label2.ForeColor = Color.Black;
            label3.ForeColor = Color.Black;
            Logopanel.BackgroundImage = Image.FromFile("C:\\Users\\Sarith\\Pictures\\GAD Project Logo\\Pizza Master Logo.png");
            btnLightMode.SendToBack();
            btnCloseForm.BackColor = Color.White;
        }
    }
}
