using Shipment_Manager.FrontEnd;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shipment_Manager
{
    public partial class FORM_Home : Form
    {
        FrontEnd.FORM_Companies_Members FORM_Companies;
        SubForms.Members.frm_Pocket_Show frm_Pocket_Add;
        FrontEnd.FORM_REPORT_VIEWER FORM_REPORT_VIEWER;

        public FORM_Home()
        {
            InitializeComponent();

            Button_Coloring("button1");
            FORM_Companies = new FrontEnd.FORM_Companies_Members();
            groupBox1.Controls.Clear();
            FORM_Companies.TopLevel = false;
            groupBox1.Controls.Add(FORM_Companies);
            FORM_Companies.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            FORM_Companies.Dock = DockStyle.Fill;
            FORM_Companies.Show();
        }
        //بيانات الشركات
        private void button1_Click(object sender, EventArgs e)
        {
            if (BackEnd.SessionInfo.Permissions[0].Equals('y'))
            {
                Button_Coloring("button1");
                FORM_Companies = new FrontEnd.FORM_Companies_Members();
                groupBox1.Controls.Clear();
                FORM_Companies.TopLevel = false;
                groupBox1.Controls.Add(FORM_Companies);
                FORM_Companies.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                FORM_Companies.Dock = DockStyle.Fill;
                FORM_Companies.Show();
            }
        }
        //بيانات الاعضاء
        private void button2_Click(object sender, EventArgs e)
        {
            Button_Coloring("button2");
        }
        //بيانات الحافظة
        private void button3_Click(object sender, EventArgs e)
        {
            if (BackEnd.SessionInfo.Permissions[4].Equals('y'))
            {
                Button_Coloring("button3");
                frm_Pocket_Add = new SubForms.Members.frm_Pocket_Show();
                groupBox1.Controls.Clear();
                frm_Pocket_Add.TopLevel = false;
                groupBox1.Controls.Add(frm_Pocket_Add);
                frm_Pocket_Add.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                frm_Pocket_Add.Dock = DockStyle.Fill;
                frm_Pocket_Add.Show();
            }
            else { new frmDialog("لا توجد صلاحيات كافية").ShowDialog(); }
        }
        private void button4_Click(object sender, EventArgs e)
        {

            Button_Coloring("button4");
            frmDialog dialog = new frmDialog("هل انت متأكد من رغبتك فى الخروج؟", true);
            dialog.ShowDialog();
            if (frmDialog.State)
            {
                Application.Exit();
            }
         
        }



        public void Button_Coloring(string button_Name)
        {
            foreach (Control control in panel1.Controls)
            {
                if (control.GetType() == typeof(Button))
                {
                    if (control.Name == button_Name)
                    {
                        control.BackColor = Color.FromArgb(24, 32, 46);
                    }
                    else
                    {
                        control.BackColor = Color.Transparent;
                    }
                }
                
            }
        
        }

        private void FORM_Home_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void FORM_Home_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.E && e.KeyCode == Keys.Delete)
            {
                MessageBox.Show("hi");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            FrontEnd.UsersSettings frmUsers;

            Button_Coloring("button2");
            frmUsers = new FrontEnd.UsersSettings();
            groupBox1.Controls.Clear();
            frmUsers.TopLevel = false;
            groupBox1.Controls.Add(frmUsers);
            frmUsers.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frmUsers.Dock = DockStyle.Fill;
            frmUsers.Show();
        }
    }
}
