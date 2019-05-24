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
    public partial class UsersSettings : Form
    {
        BackEnd.Users users = new BackEnd.Users();
        public UsersSettings()
        {
            InitializeComponent();

            groupBox1.Visible = false;
            groupBox2.Visible = false;

            foreach (string user in users.GetAllUsers())
            {
                comboBox1.Items.Add(user);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                if (users.GetAllPasswords()[comboBox1.SelectedIndex].ToString().Equals(StringCipher.encryptus(textBox1.Text, "SmartSoft")))
                {
                    button7.Enabled = false;
                    textBox1.Enabled = false;
                    new frmDialog("تم التحقق بنجاح").ShowDialog();
                    groupBox1.Visible = true;
                    groupBox2.Visible = true;
                   
                }
                else { new frmDialog("كلمة مرور خاطئة").ShowDialog(); }
            }
            else { new frmDialog("من فضلك ادخل كلمة المرور").ShowDialog(); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == textBox3.Text)
            {
                if (textBox1.Enabled == false)
                {
                    users.Updatepassword(comboBox1.Text, StringCipher.encryptus(textBox2.Text, "SmartSoft"));
                    new frmDialog("تم تعديل كلمة المرور").ShowDialog();
                }
                else { new frmDialog("من فضلك قم بالتحقق اولا").ShowDialog(); }

            }
            else { new frmDialog("كلمتي المرور غير متطابقتين").ShowDialog(); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (BackEnd.SessionInfo.Permissions[9] == 'y')
            {
                if (textBox4.Text.Length > 0 && textBox5.Text.Length > 0 && textBox6.Text.Length > 0)
                {
                    if (textBox5.Text == textBox6.Text)
                    {
                        string permissions = "";
                        if (checkBox1.Checked == true) { permissions += "y"; }
                        else { permissions += "n"; }
                        if (checkBox2.Checked == true) { permissions += "y"; }
                        else { permissions += "n"; }
                        if (checkBox3.Checked == true) { permissions += "y"; }
                        else { permissions += "n"; }
                        if (checkBox4.Checked == true) { permissions += "y"; }
                        else { permissions += "n"; }
                        if (checkBox5.Checked == true) { permissions += "y"; }
                        else { permissions += "n"; }
                        if (checkBox6.Checked == true) { permissions += "y"; }
                        else { permissions += "n"; }
                        if (checkBox7.Checked == true) { permissions += "y"; }
                        else { permissions += "n"; }
                        if (checkBox8.Checked == true) { permissions += "y"; }
                        else { permissions += "n"; }
                        if (checkBox9.Checked == true) { permissions += "y"; }
                        else { permissions += "n"; }
                        if (checkBox10.Checked == true) { permissions += "y"; }
                        else { permissions += "n"; }
                    
                        if (users.AddNewUser(textBox4.Text, StringCipher.encryptus(textBox5.Text, "SmartSoft"), permissions))
                        {
                            new frmDialog("تم اضافة مستخدم بنجاح").ShowDialog();
                            textBox4.Clear();
                            textBox5.Clear();
                            textBox6.Clear();
                        }
                        else
                        {
                            new frmDialog("هذا المستخدم موجود بالفعل").ShowDialog();
                        }
                    }
                    else
                    {
                        new frmDialog("كلمتي المرور غير متطابقتين").ShowDialog();
                        textBox5.Clear();
                        textBox6.Clear();
                    }
                }
                else { 
                new frmDialog("من فضلك ادخل اسم المستخدم أو كلمة المرور").ShowDialog();
                }
            }
            else { new frmDialog("لا تملك صلاحيات كافية").ShowDialog(); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Admin")
            {
                new frmDialog("لا يمكن مسح الادمن").ShowDialog();
                return;
            }
            if (textBox1.Enabled == false || BackEnd.SessionInfo.Permissions[9] == 'y')
            {
                frmDialog dialog = new frmDialog("هل انت متأكد من مسح هذا المستخدم؟", true);
                dialog.ShowDialog();
                if (frmDialog.State)
                {
                    if (users.DeleteUser(comboBox1.Text))
                    {
                        new frmDialog("تم مسح المستخدم بنجاح").ShowDialog();
                    } 
                }
                
                //else { MessageBox.Show("لا يمكن حذف ذلك المستخدم حيث أنه الوحيد بالبرنامج"); }

            }
            else { new frmDialog("من فضلك قم بالتحقق اولا").ShowDialog(); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (BackEnd.SessionInfo.Permissions[9] == 'y')
            {
                string permissions = "";
                if (checkBox28.Checked == true) { permissions += "y"; }
                else { permissions += "n"; }
                if (checkBox27.Checked == true) { permissions += "y"; }
                else { permissions += "n"; }
                if (checkBox26.Checked == true) { permissions += "y"; }
                else { permissions += "n"; }
                if (checkBox25.Checked == true) { permissions += "y"; }
                else { permissions += "n"; }
                if (checkBox24.Checked == true) { permissions += "y"; }
                else { permissions += "n"; }
                if (checkBox23.Checked == true) { permissions += "y"; }
                else { permissions += "n"; }
                if (checkBox22.Checked == true) { permissions += "y"; }
                else { permissions += "n"; }
                if (checkBox21.Checked == true) { permissions += "y"; }
                else { permissions += "n"; }
                if (checkBox20.Checked == true) { permissions += "y"; }
                else { permissions += "n"; }
                if (checkBox19.Checked == true) { permissions += "y"; }
                else { permissions += "n"; }
             
                users.Updatepermissions(comboBox1.Text, permissions);
                new frmDialog("تم تعديل الصلاحيات").ShowDialog();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BackEnd.SessionInfo.Permissions[9] == 'y')
            {

                string permissions = users.GetAllPermissions()[comboBox1.SelectedIndex].ToString();
                if (permissions.ToString()[0] == 'y') { checkBox28.Checked = true; }
                else { checkBox28.Checked = false; }
                if (permissions.ToString()[1] == 'y') { checkBox27.Checked = true; }
                else { checkBox27.Checked = false; }
                if (permissions.ToString()[2] == 'y') { checkBox26.Checked = true; }
                else { checkBox26.Checked = false; }
                if (permissions.ToString()[3] == 'y') { checkBox25.Checked = true; }
                else { checkBox25.Checked = false; }
                if (permissions.ToString()[4] == 'y') { checkBox24.Checked = true; }
                else { checkBox24.Checked = false; }
                if (permissions.ToString()[5] == 'y') { checkBox23.Checked = true; }
                else { checkBox23.Checked = false; }
                if (permissions.ToString()[6] == 'y') { checkBox22.Checked = true; }
                else { checkBox22.Checked = false; }
                if (permissions.ToString()[7] == 'y') { checkBox21.Checked = true; }
                else { checkBox21.Checked = false; }
                if (permissions.ToString()[8] == 'y') { checkBox20.Checked = true; }
                else { checkBox20.Checked = false; }
                if (permissions.ToString()[9] == 'y') { checkBox19.Checked = true; }
                else { checkBox19.Checked = false; }
            
            }
        }
    }
}
