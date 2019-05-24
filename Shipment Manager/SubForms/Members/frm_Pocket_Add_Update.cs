using Shipment_Manager.FrontEnd;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shipment_Manager.SubForms.Members
{
    public partial class frm_Pocket_Add_Update : Form
    {
        DataTable Pocket_Table = new DataTable();
        frm_Pocket_Show frm_pocket_show;
        DataTable Pocket_Edit_Datatable = new DataTable();

        ArrayList Deleted_IDs = new ArrayList();
        ArrayList Edited_IDs = new ArrayList();
        string Serial_Edit = "";
        string Pocket_Head_ID = "";
        string ID_For_Edit;//عشان نخزن الايدي بقيمته و ميروحش مننا بحيث المتعدل يتخزن بقيمته انما المضاف حديثا حيتخزن ايدي بصفر

        /*===========================================================*/
        /*===========================================================*/

        //اضافة حافظة
        public frm_Pocket_Add_Update(frm_Pocket_Show owner)
        {
            InitializeComponent();
            frm_pocket_show = owner;
            Populate_Pocket_DGV();
            this.Text = "اضافة حافظة";


            ErrorProvider_IconPlacement();
            TextBox_Validation_Query();
            Textbox_Numbers_Letters();
            Textbox_Numbers_Only();
            TextBox_Member_Validation();
            Textbox_Serial_Check();
            AutoCompleteText();
            TextBox_Member_M_Validation();
            Textbox_Zeroes_Only();
            Summing_Lables();
            M_Calculate();
            Textbox_Enter_Add();
        }
        //تعديل حافظة
        public frm_Pocket_Add_Update(frm_Pocket_Show owner, string Pocket_Head_ID)
        {
            InitializeComponent();
            frm_pocket_show = owner;

            this.Text = "تعديل حافظة";
            button1.Text = "حفظ التغييرات";
            this.Pocket_Head_ID = Pocket_Head_ID;
            Populate_Pocket_DGV(Pocket_Head_ID);

            ArrayList Pocket_Header_Info = BackEnd.Pocket.Get_Header_Info(Pocket_Head_ID);
            textBox2.Text = Pocket_Header_Info[0].ToString();
            textBox3.Text = Pocket_Header_Info[1].ToString();
            dateTimePicker1.Value = Convert.ToDateTime(Pocket_Header_Info[2].ToString());
            textBox1.Text = Pocket_Header_Info[3].ToString();
            Serial_Edit = Pocket_Header_Info[3].ToString();//Used For validation

            ErrorProvider_IconPlacement();
            TextBox_Validation_Query();
            Textbox_Numbers_Letters();
            Textbox_Numbers_Only();
            TextBox_Member_Validation();
            Textbox_Serial_Check();
            AutoCompleteText();
            TextBox_Member_M_Validation();
            Textbox_Zeroes_Only();
            Summing_Lables();
            Textbox_Enter_Add();
        }


        /*===========================================================*/
        /*===========================================================*/
        //اضافة
        private void button6_Click(object sender, EventArgs e)
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

            if (button6.Text == "حفظ التعديل")
            {
                button4.Enabled = true;
                button1.Enabled = true;
                button5.Enabled = true;
                button6.Text = "   اضافة";

            }
            if (this.Text == "اضافة حافظة")
            {
                DataRow Datarow = Pocket_Table.NewRow();
                Datarow[0] = textBox4.Text;
                Datarow[1] = textBox5.Text;
                Datarow[2] = textBox6.Text;
                Datarow[3] = textBox7.Text;
                Datarow[4] = textBox8.Text;
                Datarow[5] = textBox9.Text;
                Datarow[6] = textBox10.Text;
                Datarow[7] = textBox11.Text;
                Datarow[8] = textBox12.Text;
                Datarow[9] = textBox13.Text;
                Datarow[10] = textBox14.Text;
                Datarow[11] = textBox15.Text;
                Datarow[12] = dateTimePicker2.Value.ToString("yyyy/MM/dd");
                Datarow[13] = textBox16.Text;
                Datarow[14] = textBox17.Text;
                Datarow[15] = textBox18.Text;
                Datarow[16] = textBox19.Text;
                Datarow[17] = textBox20.Text;

                Pocket_Table.Rows.Add(Datarow);
                dataGridView2.DataSource = Pocket_Table;
                int nRowIndex = dataGridView2.Rows.Count - 1;
                dataGridView2.Rows[nRowIndex].Selected = true;
            }
            if (this.Text == "تعديل حافظة")
            {
                DataRow Datarow = Pocket_Edit_Datatable.NewRow();
                //في حالة اننا ضيفنا رحلة جديدة
                if (string.IsNullOrEmpty(ID_For_Edit))
                {
                    Datarow[0] = "0";//to be added row
                }
                //فى حالة انها رحلة موجودة و بنعدلها
                else
                {
                    Datarow[0] = ID_For_Edit;
                    ID_For_Edit = null;
                }
                Datarow[1] = textBox4.Text;
                Datarow[2] = textBox5.Text;
                Datarow[3] = textBox6.Text;
                Datarow[4] = textBox7.Text;
                Datarow[5] = textBox8.Text;
                Datarow[6] = textBox9.Text;
                Datarow[7] = textBox10.Text;
                Datarow[8] = textBox11.Text;
                Datarow[9] = textBox12.Text;
                Datarow[10] = textBox13.Text;
                Datarow[11] = textBox14.Text;
                Datarow[12] = textBox15.Text;
                Datarow[13] = dateTimePicker2.Value.ToString("yyyy/MM/dd");
                Datarow[14] = textBox16.Text;
                Datarow[15] = textBox17.Text;
                Datarow[16] = textBox18.Text;
                Datarow[17] = textBox19.Text;
                Datarow[18] = textBox20.Text;

                Pocket_Edit_Datatable.Rows.Add(Datarow);
                dataGridView2.DataSource = Pocket_Edit_Datatable;
                int nRowIndex = dataGridView2.Rows.Count - 1;
                dataGridView2.Rows[nRowIndex].Selected = true;
            }
            Summing_Lables();
            M_Calculate();
            Clear_Textboxes();
        }
        //تعديل صف
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count == 0)
            {
                new frmDialog("لا توجد رحلات لتعديلها").ShowDialog();
                return;
            }
            button4.Enabled = false;
            button1.Enabled = false;
            button5.Enabled = false;
            button6.Text = "حفظ التعديل";

            if (this.Text == "اضافة حافظة")
            {
                foreach (DataGridViewRow Row in dataGridView2.SelectedRows)
                {
                    textBox4.Text = Row.Cells[0].Value.ToString();
                    textBox5.Text = Row.Cells[1].Value.ToString();
                    textBox6.Text = Row.Cells[2].Value.ToString();
                    textBox7.Text = Row.Cells[3].Value.ToString();
                    textBox8.Text = Row.Cells[4].Value.ToString();
                    textBox9.Text = Row.Cells[5].Value.ToString();
                    textBox10.Text = Row.Cells[6].Value.ToString();
                    textBox11.Text = Row.Cells[7].Value.ToString();
                    textBox12.Text = Row.Cells[8].Value.ToString();
                    textBox13.Text = Row.Cells[9].Value.ToString();
                    textBox14.Text = Row.Cells[10].Value.ToString();
                    textBox15.Text = Row.Cells[11].Value.ToString();
                    dateTimePicker2.Value = Convert.ToDateTime(Row.Cells[12].Value.ToString());
                    textBox16.Text = Row.Cells[13].Value.ToString();
                    textBox17.Text = Row.Cells[14].Value.ToString();
                    textBox18.Text = Row.Cells[15].Value.ToString();
                    textBox19.Text = Row.Cells[16].Value.ToString();
                    textBox20.Text = Row.Cells[17].Value.ToString();

                    Pocket_Table.Rows.RemoveAt(Row.Index);
                    dataGridView2.DataSource = Pocket_Table;
                }
            }
            /*=======================================================*/

            if (this.Text == "تعديل حافظة")
            {
                foreach (DataGridViewRow Row in dataGridView2.SelectedRows)
                {
                    ID_For_Edit = Row.Cells[0].Value.ToString();
                    textBox4.Text = Row.Cells[1].Value.ToString();
                    textBox5.Text = Row.Cells[2].Value.ToString();
                    textBox6.Text = Row.Cells[3].Value.ToString();
                    textBox7.Text = Row.Cells[4].Value.ToString();
                    textBox8.Text = Row.Cells[5].Value.ToString();
                    textBox9.Text = Row.Cells[6].Value.ToString();
                    textBox10.Text = Row.Cells[7].Value.ToString();
                    textBox11.Text = Row.Cells[8].Value.ToString();
                    textBox12.Text = Row.Cells[9].Value.ToString();
                    textBox13.Text = Row.Cells[10].Value.ToString();
                    textBox14.Text = Row.Cells[11].Value.ToString();
                    textBox15.Text = Row.Cells[12].Value.ToString();
                    dateTimePicker2.Value = Convert.ToDateTime(Row.Cells[13].Value.ToString());
                    textBox16.Text = Row.Cells[14].Value.ToString();
                    textBox17.Text = Row.Cells[15].Value.ToString();
                    textBox18.Text = Row.Cells[16].Value.ToString();
                    textBox19.Text = Row.Cells[17].Value.ToString();
                    textBox20.Text = Row.Cells[18].Value.ToString();

                    Edited_IDs.Add(Row.Cells[0].Value.ToString());//خزن فى اراي التعديل
                    Pocket_Edit_Datatable.Rows.RemoveAt(Row.Index);
                    dataGridView2.DataSource = Pocket_Edit_Datatable;
                }

            }
            Summing_Lables();
        }
        //مسح صف
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count>0)
            {
                frmDialog dialog = new frmDialog("هل انت متأكد من رغبتك بحذف الرحلة المحددة؟", true);
                dialog.ShowDialog();
                if (frmDialog.State)
                {
                    if (this.Text == "اضافة حافظة")
                    {
                        int theRow = dataGridView2.CurrentRow.Index;
                        foreach (DataGridViewRow Row in dataGridView2.SelectedRows)
                        {
                            Pocket_Table.Rows.RemoveAt(Row.Index);
                            dataGridView2.DataSource = Pocket_Table;
                        }

                    }
                    if (this.Text == "تعديل حافظة")
                    {
                        foreach (DataGridViewRow Row in dataGridView2.SelectedRows)
                        {
                            int theRow = dataGridView2.CurrentRow.Index;
                            Deleted_IDs.Add(Row.Cells[0].Value.ToString()); //خزن فى الاراي بتاعه المسح
                            Pocket_Edit_Datatable.Rows.RemoveAt(Row.Index);
                            dataGridView2.DataSource = Pocket_Edit_Datatable;
                        }
                    }

                }
                Summing_Lables();
                M_Calculate();
            }
            else
            {
                new frmDialog("لا توجد رحلات").ShowDialog();
            }
           
           
        }

        //حفظ التغييرات
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.Text == "اضافة حافظة")
            {
                if (BackEnd.Pocket.Add_Pocket_Head(textBox2.Text, textBox3.Text, dateTimePicker1.Value.ToString("yyyy/MM/dd"), textBox1.Text))
                {
                    foreach (DataGridViewRow Row in dataGridView2.Rows)
                    {
                        if (BackEnd.Pocket.Add_Pocket_Info(textBox1.Text, Row.Cells[0].Value.ToString(), Row.Cells[1].Value.ToString(), float.Parse(Row.Cells[2].Value.ToString()), float.Parse(Row.Cells[3].Value.ToString()), float.Parse(Row.Cells[4].Value.ToString()), float.Parse(Row.Cells[5].Value.ToString()), float.Parse(Row.Cells[6].Value.ToString()), float.Parse(Row.Cells[7].Value.ToString()), float.Parse(Row.Cells[8].Value.ToString()), Row.Cells[9].Value.ToString(), Row.Cells[10].Value.ToString(), Row.Cells[11].Value.ToString(), Row.Cells[12].Value.ToString(), Row.Cells[13].Value.ToString(), Row.Cells[14].Value.ToString(), float.Parse(Row.Cells[15].Value.ToString()), float.Parse(Row.Cells[16].Value.ToString()), Row.Cells[17].Value.ToString()))
                        { }
                    }
                    frm_pocket_show.Focus();
                    frm_pocket_show.RefreshAfterADD_EDIT("ADD");
                    //تم الاضافة بنجاح
                    this.Close();
                }
            }
            if (this.Text == "تعديل حافظة")
            {
                if (BackEnd.Pocket.Update_Pocket_Head(Pocket_Head_ID, textBox2.Text, textBox3.Text, dateTimePicker1.Value.ToString("yyyy/MM/dd"), textBox1.Text))
                {
                    foreach (DataGridViewRow Row in dataGridView2.Rows)
                    {
                        //دي رحلات جديدة يتعملها اضافة
                        if (Row.Cells[0].Value.ToString() == "0")
                        {
                            if (BackEnd.Pocket.EDIT_Add_Pocket_Info(Pocket_Head_ID, Row.Cells[1].Value.ToString(), Row.Cells[2].Value.ToString(), float.Parse(Row.Cells[3].Value.ToString()), float.Parse(Row.Cells[4].Value.ToString()), float.Parse(Row.Cells[5].Value.ToString()), float.Parse(Row.Cells[6].Value.ToString()), float.Parse(Row.Cells[7].Value.ToString()), float.Parse(Row.Cells[8].Value.ToString()), float.Parse(Row.Cells[9].Value.ToString()), Row.Cells[10].Value.ToString(), Row.Cells[11].Value.ToString(), Row.Cells[12].Value.ToString(), Row.Cells[13].Value.ToString(), Row.Cells[14].Value.ToString(), float.Parse(Row.Cells[15].Value.ToString()), float.Parse(Row.Cells[16].Value.ToString()), float.Parse(Row.Cells[17].Value.ToString()), Row.Cells[18].Value.ToString()))
                            { }
                        }
                        //دي رحلات اتمسحت حنشيلها بالاراي برضه
                        //الاولوية للمسح عشان لو عمل ايديت وبعدين مسح
                        foreach (string ID in Deleted_IDs)
                        {
                            if (BackEnd.Pocket.EDIT_Delete_Pocket_Info(ID))
                            { }
                        }

                        //دي رحلات اتعملها تعديل حنجيبها من الاراي
                        if (Edited_IDs.Contains(Row.Cells[0].Value.ToString()))
                        {
                            if (BackEnd.Pocket.Update_Pocket_Info(Row.Cells[0].Value.ToString(), Pocket_Head_ID, Row.Cells[1].Value.ToString(), Row.Cells[2].Value.ToString(), float.Parse(Row.Cells[3].Value.ToString()), float.Parse(Row.Cells[4].Value.ToString()), float.Parse(Row.Cells[5].Value.ToString()), float.Parse(Row.Cells[6].Value.ToString()), float.Parse(Row.Cells[7].Value.ToString()), float.Parse(Row.Cells[8].Value.ToString()), float.Parse(Row.Cells[9].Value.ToString()), Row.Cells[10].Value.ToString(), Row.Cells[11].Value.ToString(), Row.Cells[12].Value.ToString(), Row.Cells[13].Value.ToString(), Row.Cells[14].Value.ToString(), float.Parse(Row.Cells[15].Value.ToString()), float.Parse(Row.Cells[16].Value.ToString()), float.Parse(Row.Cells[17].Value.ToString()), Row.Cells[18].Value.ToString()))
                            { }
                        }

                    }
                    frm_pocket_show.Focus();
                    frm_pocket_show.RefreshAfterADD_EDIT("EDIT");
                    //تم التعديل بنجاح
                    this.Close();
                }
                else
                {
                    MessageBox.Show("update failed");
                }
            }
        }

        /*===========================================================*/
        /*===========================================================*/


        public void Refresh_After_Delete(int theRow)
        {
            try
            {
                this.dataGridView2.CurrentCell = this.dataGridView2[0, theRow - 1];
                dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView2.SelectedRows[0].Index;
            }
            catch { }
        } //غير مستخدمة
        public void Populate_Pocket_DGV()
        {
            BackEnd.ExtensionMethods.DoubleBuffered(this.dataGridView2, true);
            dataGridView2.SuspendLayout();
            Create_Datatable();
            Color_Pocket_DGVs();
            dataGridView2.ResumeLayout();
        } // عند الاضافة
        public void Populate_Pocket_DGV(string Pocket_Head_ID)
        {
            BackEnd.ExtensionMethods.DoubleBuffered(this.dataGridView2, true);
            dataGridView2.SuspendLayout();
            Pocket_Edit_Datatable = BackEnd.Pocket.Populate_Pocket_DGV(Pocket_Head_ID);
            dataGridView2.DataSource = Pocket_Edit_Datatable;
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[19].Visible = false;
            //خانة القرش
            dataGridView2.Columns[6].Visible = false;
            dataGridView2.Columns[8].Visible = false;

            dataGridView2.Columns[3].Visible = false;


            dataGridView2.Columns[17].Visible = false;
            Color_Pocket_DGVs_Edit();
            dataGridView2.ResumeLayout();
        } //عند التعديل
        public void Color_Pocket_DGVs()
        {
            foreach (DataGridViewColumn col in dataGridView2.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            for (int i = 0; i <= 17; i++)
            {
                DataGridViewColumn dataGridViewColumn2 = dataGridView2.Columns[i];
                dataGridViewColumn2.HeaderCell.Style.BackColor = Color.LightGray;
                if (i == 0)
                {
                    dataGridView2.Columns[i].DefaultCellStyle.ForeColor = Color.Red;
                    dataGridView2.Columns[i].HeaderCell.Style.Padding = new Padding(0, 0, 5, 0);
                    dataGridViewColumn2.HeaderCell.Style.ForeColor = Color.Red;

                }
                if (i == 1)
                {
                    dataGridView2.Columns[i].DefaultCellStyle.ForeColor = Color.SaddleBrown;
                    dataGridViewColumn2.HeaderCell.Style.ForeColor = Color.SaddleBrown;
                }
                if (i == 2 || i == 3 || i == 4)
                {
                    dataGridView2.Columns[i].DefaultCellStyle.ForeColor = Color.DarkGreen;
                    dataGridViewColumn2.HeaderCell.Style.ForeColor = Color.DarkGreen;
                }
                if (i == 5 || i == 6)
                {
                    dataGridView2.Columns[i].DefaultCellStyle.ForeColor = Color.Purple;
                    dataGridViewColumn2.HeaderCell.Style.ForeColor = Color.Purple;
                }
                if (i == 7 || i == 8)
                {
                    dataGridView2.Columns[i].DefaultCellStyle.ForeColor = Color.DarkViolet;
                    dataGridViewColumn2.HeaderCell.Style.ForeColor = Color.DarkViolet;
                }
                if (i == 9 || i == 12 || i == 13)
                {
                    dataGridView2.Columns[i].DefaultCellStyle.ForeColor = Color.DarkBlue;
                    dataGridViewColumn2.HeaderCell.Style.ForeColor = Color.DarkBlue;
                }
                if (i == 10 || i == 11)
                {
                    dataGridView2.Columns[i].DefaultCellStyle.ForeColor = Color.DarkRed;
                    dataGridViewColumn2.HeaderCell.Style.ForeColor = Color.DarkRed;
                }
                if (i == 14)
                {
                    dataGridView2.Columns[i].DefaultCellStyle.ForeColor = Color.DarkCyan;
                    dataGridViewColumn2.HeaderCell.Style.ForeColor = Color.DarkCyan;
                }
                if (i == 15 || i == 16)
                {
                    dataGridView2.Columns[i].DefaultCellStyle.ForeColor = Color.DarkGreen;
                    dataGridViewColumn2.HeaderCell.Style.ForeColor = Color.DarkGreen;
                }
                if (i == 17)
                {
                    dataGridView2.Columns[i].DefaultCellStyle.ForeColor = Color.LightSeaGreen;
                    dataGridViewColumn2.HeaderCell.Style.ForeColor = Color.LightSeaGreen;
                }
            }
        }
        public void Color_Pocket_DGVs_Edit()
        {
            foreach (DataGridViewColumn col in dataGridView2.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            for (int i = 0; i < 19; i++)
            {
                DataGridViewColumn dataGridViewColumn2 = dataGridView2.Columns[i];
                dataGridViewColumn2.HeaderCell.Style.BackColor = Color.LightGray;
                if (i == 0 || i == 1)
                {
                    dataGridView2.Columns[i].DefaultCellStyle.ForeColor = Color.Red;
                    dataGridView2.Columns[i].HeaderCell.Style.Padding = new Padding(0, 0, 5, 0);
                    dataGridViewColumn2.HeaderCell.Style.ForeColor = Color.Red;

                }
                if (i == 2)
                {
                    dataGridView2.Columns[i].DefaultCellStyle.ForeColor = Color.SaddleBrown;
                    dataGridViewColumn2.HeaderCell.Style.ForeColor = Color.SaddleBrown;
                }
                if (i == 3 || i == 4 || i == 5)
                {
                    dataGridView2.Columns[i].DefaultCellStyle.ForeColor = Color.DarkGreen;
                    dataGridViewColumn2.HeaderCell.Style.ForeColor = Color.DarkGreen;
                }
                if (i == 6 || i == 7)
                {
                    dataGridView2.Columns[i].DefaultCellStyle.ForeColor = Color.Purple;
                    dataGridViewColumn2.HeaderCell.Style.ForeColor = Color.Purple;
                }
                if (i == 8 || i == 9)
                {
                    dataGridView2.Columns[i].DefaultCellStyle.ForeColor = Color.DarkViolet;
                    dataGridViewColumn2.HeaderCell.Style.ForeColor = Color.DarkViolet;
                }
                if (i == 10 || i == 13 || i == 14)
                {
                    dataGridView2.Columns[i].DefaultCellStyle.ForeColor = Color.DarkBlue;
                    dataGridViewColumn2.HeaderCell.Style.ForeColor = Color.DarkBlue;
                }
                if (i == 11 || i == 12)
                {
                    dataGridView2.Columns[i].DefaultCellStyle.ForeColor = Color.DarkRed;
                    dataGridViewColumn2.HeaderCell.Style.ForeColor = Color.DarkRed;
                }
                if (i == 15)
                {
                    dataGridView2.Columns[i].DefaultCellStyle.ForeColor = Color.DarkCyan;
                    dataGridViewColumn2.HeaderCell.Style.ForeColor = Color.DarkCyan;
                }
                if (i == 16 || i == 17)
                {
                    dataGridView2.Columns[i].DefaultCellStyle.ForeColor = Color.DarkGreen;
                    dataGridViewColumn2.HeaderCell.Style.ForeColor = Color.DarkGreen;
                }
                if (i == 18)
                {
                    dataGridView2.Columns[i].DefaultCellStyle.ForeColor = Color.LightSeaGreen;
                    dataGridViewColumn2.HeaderCell.Style.ForeColor = Color.LightSeaGreen;
                }
            }
        }
        public void Create_Datatable()
        {
            Pocket_Table.Clear();
            Pocket_Table.Columns.Add(@"م"); //م
            Pocket_Table.Columns.Add(@"رقم
البون"); //رقم البون
            Pocket_Table.Columns.Add(@"الكمية
ك"); //الكمية ك
            Pocket_Table.Columns.Add(@"الكمية
طن"); //الكمية طن
            Pocket_Table.Columns.Add(@"الكمية
عدد"); //الكمية عدد
            Pocket_Table.Columns.Add(@"فئة النقل
قرش"); //فئة النقل قرش
            Pocket_Table.Columns.Add(@"فئة النقل
جنيه"); //فئة النقل جنية
            Pocket_Table.Columns.Add(@"القيمة
قرش"); //القيمة قرش
            Pocket_Table.Columns.Add(@"القيمة
جنيه"); //القيمة جنية
            Pocket_Table.Columns.Add(@"بيان الحمولة
نوع البضاعة"); //بيان الحمولة - نوع البضاعه
            Pocket_Table.Columns.Add(@"بيان الحمولة
من"); //بيان الحمولة من
            Pocket_Table.Columns.Add(@"بيان الحمولة
الى"); //بيان الحمولة الى
            Pocket_Table.Columns.Add(@"بيان الحمولة
تاريخ الشحن"); //بيان الحمولة تاريخ الشحن
            Pocket_Table.Columns.Add(@"بيان الحمولة
رقم السيارة"); //بيان الحمولة رقم السيارة
            Pocket_Table.Columns.Add(@"بيان الحمولة
عهدة"); //بيان الحمولة عهدة
            Pocket_Table.Columns.Add(@"العجز
ك"); //العجز ك
            Pocket_Table.Columns.Add(@"العجز
طن"); // العجز طن
            Pocket_Table.Columns.Add(@"اسم المركب"); //اسم المركب
            dataGridView2.DataSource = Pocket_Table;
            //خانة القرش
            dataGridView2.Columns[5].Visible = false;
            dataGridView2.Columns[7].Visible = false;
       
            //اخفاء الكمية
            dataGridView2.Columns[2].Visible = false;

            //اخفاء العجز قيمة واحدة
            dataGridView2.Columns[15].Visible = false;
        }
        //جلب اسم العضو
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            textBox21.Text = BackEnd.Pocket.Get_Member_Name(textBox2.Text);


        }
        //جلب رقم العضو
        private void textBox21_TextChanged(object sender, EventArgs e)
        {

            //textBox2.Text = BackEnd.Pocket.Get_Member_M(textBox21.Text);

        }

        /*===========================================================*/
        /*===========================================================*/


        //dont forget to set "Auto Validate" property of form to "Enable Allow Focus Change"
        //dont forget to create form closing event and e.cancel = false;

        public void ErrorProvider_IconPlacement()
        {
            TextBox[] textBoxes = new TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9, textBox10, textBox11, textBox12, textBox13, textBox14, textBox15, textBox16, textBox17, textBox18, textBox19, textBox20, textBox21 };
            for (int i = 0; i <= 20; i++)
            {
                errorProvider1.SetIconAlignment(textBoxes[i], System.Windows.Forms.ErrorIconAlignment.TopLeft);
                errorProvider1.SetIconPadding(textBoxes[i], 1);


            }
        }
        /*===========================================================*/
        //اتاكد ان فيه كلام
        public void TextBox_Validation_Query()
        {
            TextBox[] textBoxes = new TextBox[] { textBox1, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9, textBox10, textBox11, textBox12, textBox13, textBox14, textBox15, textBox16, textBox17, textBox18, textBox19, textBox20 };
            //نص فارغ
            for (int i = 0; i <= 18; i++)
            {
                textBoxes[i].Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Validation_Query);
            }
        }
        private void TextBox_Validation_Query(object sender, CancelEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
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
        /*===========================================================*/
        //اسم العضو و كود العضو
        public void TextBox_Member_Validation()
        {
            TextBox[] textBoxes = new TextBox[] { textBox2, textBox21 };
            for (int i = 0; i < 2; i++)
            {
                textBoxes[i].Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Member_Validation);
            }
        }
        private void TextBox_Member_Validation(object sender, CancelEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (string.IsNullOrEmpty(textbox.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(textbox, "فضلا ادخل اسم العضو او كود العضو");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textbox, null);
            }
        }
        /*===========================================================*/
        //كود العضو
        public void TextBox_Member_M_Validation()
        {
            TextBox[] textBoxes = new TextBox[] { textBox2 };
            for (int i = 0; i < 1; i++)
            {
                textBoxes[i].Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Member_M_Validation);
            }
        }
        private void TextBox_Member_M_Validation(object sender, CancelEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (BackEnd.Pocket.Get_Member_M_For_Edit(textbox.Text) == null)
            {
                e.Cancel = true;
                errorProvider1.SetError(textbox, "هذا العضو غير موجود");

            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textbox, null);
            }

        }
        /*===========================================================*/
        //رقم القيد
        public void Textbox_Serial_Check()
        {
            TextBox[] textBoxes = new TextBox[] { textBox1 };
            for (int i = 0; i < 1; i++)
            {
                textBoxes[i].Validating += new System.ComponentModel.CancelEventHandler(this.Textbox_Serial_Check);
            }
        }
        private void Textbox_Serial_Check(object sender, CancelEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (Serial_Edit == textbox.Text)
            {
                e.Cancel = false;
                errorProvider1.SetError(textbox, null);
                return;
            }
            if (string.IsNullOrEmpty(textbox.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(textbox, "لا يوجد نص");
            }
            else
            {

                if (BackEnd.Pocket.Get_Header_Serial_For_Edit(textbox.Text) == null)
                {
                    e.Cancel = false;
                    errorProvider1.SetError(textbox, null);
                }
                else
                {
                    e.Cancel = true;
                    errorProvider1.SetError(textbox, "رقم القيد موجود مسبقا");
                }
            }
        }
        /*===========================================================*/
        //حروف و ارقام
        public void Textbox_Numbers_Letters()
        {
            TextBox[] textboxes = new TextBox[] { textBox1, textBox2, textBox20, textBox13, textBox14, textBox15, textBox16 };
            for (int i = 0; i < 7; i++)
            {
                textboxes[i].KeyPress += new KeyPressEventHandler(this.textBox_KeyPress);
            }
        }
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar == (char)Keys.Divide && (!(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space)))
            {
                e.Handled = true;
            }
        }
        /*===========================================================*/
        //ارقام و كسور
        public void Textbox_Numbers_Only()
        {
            TextBox[] textboxes = new TextBox[] { textBox6, textBox7, textBox8, textBox9, textBox10, textBox11, textBox12, textBox18, textBox19, textBox4, textBox5 };
            for (int i = 0; i < 11; i++)
            {
                textboxes[i].KeyPress += new KeyPressEventHandler(this.textBox_KeyPress2);
            }
        }
        private void textBox_KeyPress2(object sender, KeyPressEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            while (textbox.Text.StartsWith("."))
            {
                textbox.Text = textbox.Text.Remove(0, 1);
            }

            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '\b')
            {
                // Allow Digits and BackSpace char
            }
            else if (e.KeyChar == '.' && !((TextBox)sender).Text.Contains('.'))
            {
                //Allows only one Dot Char
            }
            else
            {
                e.Handled = true;
            }

        }

        /*===========================================================*/
        //اصفار فقط
        public void Textbox_Zeroes_Only()
        {
            TextBox[] textboxes = new TextBox[] { textBox6, textBox7, textBox8, textBox9, textBox10, textBox11, textBox12, textBox17, textBox18, textBox19 };
            for (int i = 0; i < 10; i++)
            {
                textboxes[i].TextChanged += new EventHandler(this.textBox6_TextChanged);
            }
        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (string.IsNullOrEmpty(textbox.Text))
            {
                textbox.Text = "0";
            }
        }
        /*===========================================================*/
        //انتر يعمل اضافة
        public void Textbox_Enter_Add()
        {
            TextBox[] textboxes = new TextBox[] { textBox4,textBox5,textBox6,textBox7,textBox8,textBox9,textBox10,textBox11,textBox12,textBox13,textBox14,textBox15,textBox16,textBox17,textBox18,textBox19,textBox20 };
            for (int i = 0; i < 17; i++)
            {
                textboxes[i].KeyPress += new KeyPressEventHandler(this.textBox5_KeyPress);
            }
        }
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (e.KeyChar == (Char)Keys.Enter)
            {
                button6_Click(sender, e);
            }
        }
        /*===========================================================*/
        private void frm_Pocket_Add_Update_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void AutoCompleteText()
        {

            textBox21.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox21.AutoCompleteSource = AutoCompleteSource.CustomSource;

            textBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;

            AutoCompleteStringCollection AutoCompeleteString21 = new AutoCompleteStringCollection();
            AutoCompleteStringCollection AutoCompeleteString2 = new AutoCompleteStringCollection();

            ArrayList Member_Names = BackEnd.Pocket.AutoComplete_Member_Name();
            ArrayList Member_M = BackEnd.Pocket.AutoComplete_Member_M();
            for (int i = 0; i < Member_Names.Count; i++)
            {
                AutoCompeleteString21.Add(Member_Names[i].ToString());
            }
            for (int i = 0; i < Member_M.Count; i++)
            {
                AutoCompeleteString2.Add(Member_M[i].ToString());
            }
            textBox21.AutoCompleteCustomSource = AutoCompeleteString21;
            textBox2.AutoCompleteCustomSource = AutoCompeleteString2;
        }

        private void textBox21_Leave(object sender, EventArgs e)
        {
            try
            {
                textBox2.Text = BackEnd.Pocket.Get_Member_M(textBox21.Text);
            }
            catch
            {

            }
        }


        /*===========================================================*/
        /*===========================================================*/
        public void Clear_Textboxes()
        {
            textBox5.Text = string.Empty;

            textBox6.Text = "0";
            textBox7.Text = "0";
            textBox8.Text = "0";

            textBox9.Text = "0";
            textBox10.Text = "0";

            textBox11.Text = "0";
            textBox12.Text = "0";

            textBox13.Text = string.Empty;
            textBox14.Text = string.Empty;
            textBox15.Text = string.Empty;

            textBox16.Text = string.Empty;

            textBox17.Text = "0";
            textBox18.Text = "0";
            textBox19.Text = "0";


            textBox20.Text = string.Empty;
            textBox5.Focus();

        }
        public void M_Calculate()
        {
            if (this.Text != "تعديل حافظة")
            {
                textBox4.Text = (dataGridView2.Rows.Count + 1).ToString();
            }
        }
        public void Summing_Lables()
        {
            float Trans_Sum = 0;
            float trans_piaster = 0;
            float Value_Sum = 0;
            float value_piaster = 0;
            try
            {
                for (int i = 0; i < dataGridView2.Rows.Count; ++i)
                {
                    Trans_Sum += float.Parse(Convert.ToString(dataGridView2.Rows[i].Cells[7].Value));
                    Value_Sum += float.Parse(Convert.ToString(dataGridView2.Rows[i].Cells[9].Value));

                    trans_piaster += float.Parse(Convert.ToString(dataGridView2.Rows[i].Cells[6].Value)) / 100;
                    value_piaster += float.Parse(Convert.ToString(dataGridView2.Rows[i].Cells[8].Value)) / 100;
                }
                label25.Text = (Trans_Sum + trans_piaster).ToString();
                label27.Text = (Value_Sum + value_piaster).ToString();
            }
            catch (Exception)
            {
                for (int i = 0; i < dataGridView2.Rows.Count; ++i)
                {
                    Trans_Sum += float.Parse(Convert.ToString(dataGridView2.Rows[i].Cells[6].Value));
                    Value_Sum += float.Parse(Convert.ToString(dataGridView2.Rows[i].Cells[8].Value));

                    trans_piaster += float.Parse(Convert.ToString(dataGridView2.Rows[i].Cells[5].Value)) / 100;
                    value_piaster += float.Parse(Convert.ToString(dataGridView2.Rows[i].Cells[7].Value)) / 100;
                }
                label25.Text = (Trans_Sum + trans_piaster).ToString();
                label27.Text = (Value_Sum + value_piaster).ToString();
            }

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
           
            if (textBox7.Text == "0" || string.IsNullOrEmpty(textBox7.Text))
            {
                try
                {
                    textBox12.Text = Convert.ToString(float.Parse(textBox8.Text) * float.Parse(textBox10.Text));
                }
                catch (Exception)
                {


                }
            }
            else
            {
                try
                {
                    textBox12.Text = Convert.ToString(float.Parse(textBox7.Text) * float.Parse(textBox10.Text));
                }
                catch (Exception)
                {


                }
            }
           
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (textBox7.Text == "0" || string.IsNullOrEmpty(textBox7.Text))
            {
                try
                {
                    textBox12.Text = Convert.ToString(float.Parse(textBox8.Text) * float.Parse(textBox10.Text));
                }
                catch (Exception)
                {


                }
            }
            else
            {
                try
                {
                    textBox12.Text = Convert.ToString(float.Parse(textBox7.Text) * float.Parse(textBox10.Text));
                }
                catch (Exception)
                {
                    
               
                }
               
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (textBox7.Text == "0" || string.IsNullOrEmpty(textBox7.Text))
            {
                try
                {
                    textBox12.Text = Convert.ToString(float.Parse(textBox8.Text) * float.Parse(textBox10.Text));
                }
                catch (Exception)
                {


                }

            }
            else
            {
                try
                {
                    textBox12.Text = Convert.ToString(float.Parse(textBox7.Text) * float.Parse(textBox10.Text));
                }
                catch (Exception)
                {


                }
            }
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Contains("بيبسي") || textBox3.Text.Contains("البيبسي") || textBox3.Text.Contains("البيبسى") || textBox3.Text.Contains("بيبسى") || textBox3.Text.Contains("كولا"))
            {
                textBox7.Text = "1";

            }
        }

       






    }
}
