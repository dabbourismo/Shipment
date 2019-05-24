using Shipment_Manager.FrontEnd;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shipment_Manager.BackEnd;
using System.Collections;

namespace Shipment_Manager.SubForms.Members
{
    public partial class frm_Members_Add_Update : Form
    {
        FORM_Companies_Members FORM_Companies_Members;
        string ID = "";
        //اضافة
        public frm_Members_Add_Update(FORM_Companies_Members owner)
        {
            InitializeComponent();
            FORM_Companies_Members = owner;
            this.Text = "اضافة عضو";
            numericUpDown1.Value = BackEnd.Members.Next_M();

        }
        /*=====================================================*/
        //تعديل
        public frm_Members_Add_Update(FORM_Companies_Members owner,string ID)
        {
            InitializeComponent();
            this.ID = ID;
            ArrayList Members_Info = BackEnd.Members.Info(ID);
            FORM_Companies_Members = owner;
            this.Text = "تعديل عضو";
            //Assign Data
            numericUpDown1.Value = decimal.Parse(Members_Info[0].ToString());
            textBox1.Text = Convert.ToString(Members_Info[1]);
     
        }
        /*==========================حفظ===========================*/
        private void button6_Click(object sender, EventArgs e)
        {
            if (this.Text == "اضافة عضو")
            {
                if (BackEnd.Members.Add(Convert.ToInt32(numericUpDown1.Value.ToString()), Convert.ToString(textBox1.Text)))
                {
                    //تم الاضافة بنجاح
                    FORM_Companies_Members.Focus();
                    FORM_Companies_Members.RefreshAfterADD_EDIT("ADD");
                    this.Close();
                }
            }
            else
            {
                if (BackEnd.Members.Update(this.ID,Convert.ToInt32(numericUpDown1.Value),Convert.ToString(textBox1.Text)))
                {
                    //تم التعديل بنجاح
                    FORM_Companies_Members.Focus();
                    FORM_Companies_Members.RefreshAfterADD_EDIT("EDIT");
                    this.Close();
                }
            }
        }














        public void Validation()
        {
            //use labels not message boxes
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                //label -->
                return;
            }
            else if (string.IsNullOrEmpty(numericUpDown1.Value.ToString()))
            {
                //label -->
                return;
            }
            else if (Convert.ToInt32(numericUpDown1.Value) == 0)
            {
                //يجيب اخر رقم فى الداتابيز و يزود عليه 1
            }
            else if (BackEnd.Members.Check_M(Convert.ToString(numericUpDown1.Value)))
            {
                MessageBox.Show("موجود مسبقا");
                return;
            }

        }

        private void frm_Members_Add_Update_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button6_Click(sender, e);
            }
        }

        private void frm_Members_Add_Update_Shown(object sender, EventArgs e)
        {
            numericUpDown1.Focus();
        }
    }
}
