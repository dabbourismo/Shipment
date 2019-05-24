using Shipment_Manager.FrontEnd;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shipment_Manager.SubForms.Members
{
    public partial class frm_Advanced_Search : Form
    {
        string Command;
        string RowFilter = string.Empty;
        string Report_parameter;
        public frm_Advanced_Search()
        {
            InitializeComponent();
            //TextBox_Validation_Query();
            button7.Enabled = false;
            button4.Enabled = false;
            textBox6.Enabled = false;
            dataGridView1.DataSource = BackEnd.Fake_Search.Show_Empty_DGV();
            Color_DGV();
            ErrorProvider_IconPlacement();
            Textbox_Enter_Add();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked && !checkBox5.Checked &&!checkBox6.Checked)
            {
                button7.Enabled = false;
            }
            if (checkBox1.Checked)
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                button7.Enabled = true;
            }
            else
            {
                textBox1.Enabled = false;
                textBox1.Text = string.Empty;
                textBox2.Enabled = false;
                textBox2.Text = string.Empty;
            }
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked && !checkBox5.Checked &&!checkBox6.Checked)
            {
                button7.Enabled = false;
            }
            if (checkBox2.Checked)
            {
                textBox3.Enabled = true;
                button7.Enabled = true;
            }
            else
            {
                textBox3.Enabled = false;
                textBox3.Text = string.Empty;
            }
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked && !checkBox5.Checked &&!checkBox6.Checked)
            {
                button7.Enabled = false;
            }
            if (checkBox3.Checked)
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
                button7.Enabled = true;
            }
            else
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
            }
        }
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked && !checkBox5.Checked &&!checkBox6.Checked)
            {
                button7.Enabled = false;
            }
            if (checkBox4.Checked)
            {
                textBox4.Enabled = true;
                button7.Enabled = true;
            }
            else
            {
                textBox4.Enabled = false;
                textBox4.Text = string.Empty;
            }
        }
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked && !checkBox5.Checked &&!checkBox6.Checked)
            {
                button7.Enabled = false;
            }
            if (checkBox5.Checked)
            {
                textBox5.Enabled = true;
                button7.Enabled = true;
            }
            else
            {
                textBox5.Enabled = false;
                textBox5.Text = string.Empty;
            }
        }

        //بحث
        private void button7_Click(object sender, EventArgs e)
        {
            
            //Validation
            if (ValidateChildren(ValidationConstraints.Enabled))
            {

            }
            else
            {
                return;
            }
            /*==========================================================*/
            if (checkBox1.Checked)
            {
                RowFilter += "(Pockets.Cargo_From LIKE '%" + textBox1.Text + "%'  AND  Pockets.Cargo_To LIKE '%" + textBox2.Text + "%')";
            }
            if (checkBox2.Checked)
            {
                if (RowFilter.Length > 0)
                        RowFilter += " " + " AND" + " ";
                
                    RowFilter += "(Pockets.Cargo_Car_Num LIKE '%"+textBox3.Text+"%')";
            }
            if (checkBox3.Checked)
            {
                if (RowFilter.Length >0)
                    RowFilter += " " + " AND" + " ";
             
                    RowFilter += "(Pockets.Cargo_Ship_Date BETWEEN '" + dateTimePicker1.Value.ToString("yyyy/MM/dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy/MM/dd") + "')";
            }
            if (checkBox4.Checked)
            {
                if (RowFilter.Length >0)
                    RowFilter += " " + " AND" + " ";

                    RowFilter += "(Pockets.Coupon_Num LIKE '%"+textBox4.Text+"%')";
            }
            if (checkBox5.Checked)
            {
                if (RowFilter.Length >0)
                    RowFilter += " " + " AND" + " ";
    
                    RowFilter += "(Pockets.Ship_Name LIKE '%"+textBox5.Text+"%')";
            }

            if (checkBox6.Checked)
            {
                if (RowFilter.Length > 0)
                    RowFilter += " " + " AND" + " ";

                RowFilter += "(Pockets_Head.Company_Name LIKE '%" + textBox7.Text + "%')";
            }


            Command = @"SELECT Pockets_Head.Pocket_Serial AS [رقم 
القيد],
Members.Member_M AS [مسلسل
العضو],
Members.Member_Name AS [اسم 
العضو],
Pockets_Head.Company_Name AS [اسم 
الشركة], 
Pockets_Head.Pocket_Date AS التاريخ, 
Pockets.Pocket_M AS م,
Pockets.Coupon_Num AS [رقم 
البون],

Pockets.Amount_Ton AS [كمية
طن],
Pockets.Amount_Num AS [كمية
عدد],

Pockets.Trans_Pound AS [فئة النقل
جنيه],

Pockets.Value_Pound AS [القيمة
جنيه],
Pockets.Cargo_Type AS [بيان الحمولة
نوع البضاعة],
Pockets.Cargo_From AS [بيان الحمولة
من],
Pockets.Cargo_To AS [بيان الحمولة
الى],
Pockets.Cargo_Ship_Date AS [بيان الحمولة
تاريخ الشحن],
Pockets.Cargo_Car_Num AS [بيان الحمولة
رقم السيارة],
Pockets.Charge AS عهدة,

Pockets.Cuts_Ton AS [العجز
طن],
Pockets.Ship_Name AS [اسم المركب]
FROM
Pockets INNER JOIN
Pockets_Head ON Pockets.Pocket_Head_ID = Pockets_Head.ID INNER JOIN
Members ON Pockets_Head.Member_ID = Members.ID WHERE " + RowFilter + " ";
            dataGridView1.DataSource = BackEnd.DataAccessLayer.Get_TableData(Command);

            if (dataGridView1.Rows.Count == 0)
            {
                button4.Enabled = false;
                textBox6.Enabled = false;
            }
            else
            {
                button4.Enabled = true;
                textBox6.Enabled = true;
            }
            Report_parameter = RowFilter;
            RowFilter = string.Empty;
           
        }

        public void TextBox_Validation_Query()
        {
            TextBox[] textBoxes = new TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox5 };
            //نص فارغ
            for (int i = 0; i <= 4; i++)
            {
                textBoxes[i].Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Validation_Query);
            }
        }
        private void TextBox_Validation_Query(object sender, CancelEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (textbox.Enabled)
            {
                if (string.IsNullOrEmpty(textbox.Text))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(textbox, "لا يوجد نص");

                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(textbox, null);
                }
            }

        }

        public void Color_DGV()
        {
            BackEnd.ExtensionMethods.DoubleBuffered(this.dataGridView1, true);
            dataGridView1.SuspendLayout();
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.BackColor = Color.LightGray;
            }
            dataGridView1.ResumeLayout();
        }
        public void ErrorProvider_IconPlacement()
        {
            TextBox[] textBoxes = new TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6 };
            for (int i = 0; i <= 5; i++)
            {
                errorProvider1.SetIconAlignment(textBoxes[i], System.Windows.Forms.ErrorIconAlignment.TopLeft);
                errorProvider1.SetIconPadding(textBoxes[i], 1);
            }
        }
        private void frm_Advanced_Search_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }




        public void Textbox_Enter_Add()
        {
            TextBox[] textboxes = new TextBox[] { textBox1, textBox2, textBox3,textBox4,textBox5 };
            for (int i = 0; i < 5; i++)
            {
                textboxes[i].KeyPress += new KeyPressEventHandler(this.textBox5_KeyPress);
            }
        }
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (e.KeyChar == (Char)Keys.Enter)
            {
                button7_Click(sender, e);
            }
        }
        //عرض تقرير
        private void button4_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<FORM_REPORT_VIEWER>().Any())
            {
                Application.OpenForms["FORM_REPORT_VIEWER"].BringToFront();
                return;
            }
            else
            {
                new FORM_REPORT_VIEWER(Report_parameter,textBox6.Text).Show();
            }
        }

        private void frm_Advanced_Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                button4_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4_Click(sender, e);
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked && !checkBox5.Checked &&!checkBox6.Checked)
            {
                button7.Enabled = false;
            }
            if (checkBox6.Checked)
            {
                textBox7.Enabled = true;
                button7.Enabled = true;
            }
            else
            {
                textBox7.Enabled = false;
                textBox7.Text = string.Empty;
            }
        }
    }
}
