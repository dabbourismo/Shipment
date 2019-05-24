using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shipment_Manager.FrontEnd
{
    public partial class Login : Form
    {
        BackEnd.Users users = new BackEnd.Users();
        public Login()
        {
            InitializeComponent();
            foreach (string x in users.GetAllUsers())
            {
                comboBox1.Items.Add(x);
            }
            comboBox1.SelectedIndex = 0;
            this.ActiveControl = textBox1;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label6.Text = DateTime.Now.ToLongTimeString();
            label5.Text = DateTime.Now.ToShortDateString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                if (users.GetAllPasswords()[comboBox1.SelectedIndex].ToString().Equals(StringCipher.encryptus(textBox1.Text, "SmartSoft")))
                {
                    BackEnd.SessionInfo.UserName = comboBox1.Text;
                    BackEnd.SessionInfo.Permissions = users.GetAllPermissions()[comboBox1.SelectedIndex].ToString();
                    System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(thread));
                    t.SetApartmentState(System.Threading.ApartmentState.STA);
                    t.Start();
                    this.Close();
                }
                else { new frmDialog("كلمة مرور خاطئة").ShowDialog(); }
            }
            else { new frmDialog("من فضلك ادخل كلمة المرور").ShowDialog(); }
        }
        public void thread() { Application.Run(new FORM_Home()); }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }

}
