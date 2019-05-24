using Shipment_Manager.FrontEnd;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Shipment_Manager.SubForms.Members
{
    public partial class frm_Pocket_Show : Form
    {
        string RowFilter = string.Empty;
        string Report_parameter;
        public frm_Pocket_Show()
        {
            InitializeComponent();
            Populate_Header_DGV();
            try
            { Populate_Pocket_DGV(dataGridView1.Rows[0].Cells[0].Value.ToString()); }
            catch 
            {
              
                dataGridView2.DataSource = BackEnd.Pocket.Populate_Pocket_DGV("0");
                Color_Pocket_DGVs();
                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[19].Visible = false;

                dataGridView2.Columns[6].Visible = false;
                dataGridView2.Columns[8].Visible = false;

                //اخفاء الكمية
                //اخفاء الكمية
                dataGridView2.Columns[3].Visible = false;

                //اخفاء العجز قيمة واحدة
                dataGridView2.Columns[16].Visible = false;
            }
            comboBox1.SelectedIndex = 0;
            button5.Enabled = false;
        }



        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackEnd.ExtensionMethods.DoubleBuffered(this.dataGridView1, true);
            dataGridView1.Invoke((MethodInvoker)delegate
            {
                dataGridView1.SuspendLayout();
                dataGridView1.DataSource = BackEnd.Pocket.Populate_Head_DGV();
                dataGridView1.Columns[0].Visible = false;
                Color_Header_DGV();
                dataGridView1.ResumeLayout();
                try
                {
                    Populate_Pocket_DGV(dataGridView1.Rows[0].Cells[0].Value.ToString());
                }
                catch { dataGridView2.DataSource = BackEnd.Pocket.Populate_Pocket_DGV("0"); }
            });
           
        }
        public void Populate_Header_DGV()
        {
            //bgWorker.RunWorkerAsync();
            BackEnd.ExtensionMethods.DoubleBuffered(this.dataGridView1, true);
            dataGridView1.SuspendLayout();
            dataGridView1.DataSource = BackEnd.Pocket.Populate_Head_DGV();
            dataGridView1.Columns[0].Visible = false;
            Color_Header_DGV();
            dataGridView1.ResumeLayout();
        }



        public void Color_Header_DGV()
        {
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            for (int i = 0; i < 6; i++)
            {
                DataGridViewColumn dataGridViewColumn1 = dataGridView1.Columns[i];
                dataGridViewColumn1.HeaderCell.Style.BackColor = Color.LightGray;
            }
        }
        public void Populate_Pocket_DGV(string Pocket_Header_ID)
        {
            BackEnd.ExtensionMethods.DoubleBuffered(this.dataGridView2, true);
            dataGridView2.SuspendLayout();
            dataGridView2.DataSource = BackEnd.Pocket.Populate_Pocket_DGV(Pocket_Header_ID);
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[19].Visible = false;
            //اخفاء القرش
            dataGridView2.Columns[6].Visible = false;
            dataGridView2.Columns[8].Visible = false;
            //اخفاء الكمية
            dataGridView2.Columns[3].Visible = false;

            //اخفاء العجز قيمة واحدة
            dataGridView2.Columns[16].Visible = false;
            Color_Pocket_DGVs();
            dataGridView2.ResumeLayout();
        }
        public void Color_Pocket_DGVs()
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
        //جلب الحافظة
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                Populate_Pocket_DGV(row.Cells[0].Value.ToString());
                Summing_Lables();
            }
        }
        //تعديل بالدبل كليك
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            button3_Click(sender, e);
        }
        //مسح الحافظة
        private void button2_Click(object sender, EventArgs e)
        {
            if (BackEnd.SessionInfo.Permissions[7].Equals('y'))
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    new frmDialog("لا يوجد حافظات لمسحها").ShowDialog();
                    return;
                }
                int theRow = dataGridView1.CurrentRow.Index;
                frmDialog dialog = new frmDialog("هل انت متأكد من رغبتك بمسح هذة الحافظة؟", true);
                dialog.ShowDialog();
                if (frmDialog.State)
                {
                    foreach (DataGridViewRow Row in dataGridView1.SelectedRows)
                    {
                        BackEnd.Pocket.Delete_Pocket(Row.Cells[0].Value.ToString());
                    }
                    //new frmDialog("تم مسح الحافظة").ShowDialog();
                    Populate_Header_DGV();
                    dataGridView1.ClearSelection();
                    dataGridView2.DataSource = BackEnd.Pocket.Populate_Pocket_DGV("0");//عشان بعد السيليكشن تبان فاضية
                }
            }
            else
            {
                new frmDialog("لا توجد صلاحيات كافية").ShowDialog();
                return; ;
            }

        }
        //اضافة حافظة
        private void button1_Click(object sender, EventArgs e)
        {
            if (BackEnd.SessionInfo.Permissions[5].Equals('y'))
            {
                if (Application.OpenForms.OfType<frm_Pocket_Add_Update>().Any())
                {
                    Application.OpenForms["frm_Pocket_Add_Update"].BringToFront();
                    return;
                }
                else
                {
                    new frm_Pocket_Add_Update(this).Show();
                }
            }
            else
            {
                new frmDialog("لا توجد صلاحيات كافية").ShowDialog();
                return;
            }
        }

        //public void RefreshAfterADD_EDIT(string ADD_EDIT)
        //{
        //    if (ADD_EDIT == "ADD")
        //    {
        //        try
        //        {
        //            Populate_Header_DGV();
        //            this.dataGridView1.CurrentCell = this.dataGridView1[1, dataGridView1.Rows.Count - 1];
        //            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.SelectedRows[0].Index;
        //        }
        //        catch { }
        //    }
        //    //EDIT
        //    else
        //    {
        //        if (dataGridView2.SelectedRows.Count > 0)
        //        {
        //            int Row = dataGridView1.CurrentRow.Index;
        //            Populate_Header_DGV();
        //            try
        //            {
        //                this.dataGridView1.CurrentCell = this.dataGridView1[1, Row];
        //                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.SelectedRows[0].Index;
        //            }
        //            catch { }
        //        }
        //    }

        //}

        //تعديل حافظة

        private void button3_Click(object sender, EventArgs e)
        {
            if (BackEnd.SessionInfo.Permissions[6].Equals('y'))
            {
                if (Application.OpenForms.OfType<frm_Pocket_Add_Update>().Any())
                {
                    Application.OpenForms["frm_Pocket_Add_Update"].BringToFront();
                    return;
                }
                else
                {
                    foreach (DataGridViewRow Row in dataGridView1.SelectedRows)
                    {
                        new frm_Pocket_Add_Update(this, Row.Cells[0].Value.ToString()).Show();
                    }

                }
            }
            else
            {
                new frmDialog("لا توجد صلاحيات كافية").ShowDialog();
                return;
            }
        }
        //البحث
        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex==0)//>رقم القيد
            {
                dataGridView1.DataSource = BackEnd.Pocket.Search(textBox1.Text, null, null, null, null, dateTimePicker1.Value,dateTimePicker2.Value);
                RowFilter += "(Pockets_Head.Pocket_Serial LIKE '%" + textBox1.Text + "%')";
            }
            if (comboBox1.SelectedIndex == 1)//>مسلسل العضو
            {
                dataGridView1.DataSource = BackEnd.Pocket.Search(null, textBox1.Text, null, null, null, dateTimePicker1.Value, dateTimePicker2.Value);
                RowFilter += "(Members.Member_Name LIKE '%" + BackEnd.Pocket.Get_Member_Name(textBox1.Text) + "%') AND (Pockets_Head.Pocket_Date BETWEEN '" + dateTimePicker1.Value.ToString("yyyy/MM/dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy/MM/dd") + "')";
            }
            if (comboBox1.SelectedIndex == 2)//>اسم العضو
            {
                dataGridView1.DataSource = BackEnd.Pocket.Search(null, null, textBox1.Text, null, null, dateTimePicker1.Value, dateTimePicker2.Value);
                RowFilter += "(Members.Member_Name LIKE '%" + textBox1.Text + "%') AND (Pockets_Head.Pocket_Date BETWEEN '" + dateTimePicker1.Value.ToString("yyyy/MM/dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy/MM/dd") + "')";
            }
            if (comboBox1.SelectedIndex ==3)//>اسم الشركة
            {
                dataGridView1.DataSource = BackEnd.Pocket.Search(null, null, null, textBox1.Text, null, dateTimePicker1.Value, dateTimePicker2.Value);
                RowFilter += "(Pockets_Head.Company_Name LIKE '%" + textBox1.Text + "%') AND (Pockets_Head.Pocket_Date BETWEEN '" + dateTimePicker1.Value.ToString("yyyy/MM/dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy/MM/dd") + "')";
            }
            if (comboBox1.SelectedIndex ==4)//التاريخ
            {
                dataGridView1.DataSource = BackEnd.Pocket.Search(null, null, null, null, "Date_Only", dateTimePicker1.Value, dateTimePicker2.Value);
                RowFilter += "(Pockets_Head.Pocket_Date BETWEEN '" + dateTimePicker1.Value.ToString("yyyy/MM/dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy/MM/dd") + "')";
            }
            try
            { Populate_Pocket_DGV(dataGridView1.Rows[0].Cells[0].Value.ToString()); }
            catch { dataGridView2.DataSource = BackEnd.Pocket.Populate_Pocket_DGV("0"); }
            if (dataGridView1.Rows.Count == 0)
            {
                button5.Enabled = false;
            }
            else
            {
                button5.Enabled = true;
            }
            Report_parameter = RowFilter;
            RowFilter = string.Empty;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
            }
            else
            {
                dateTimePicker1.Visible = true;
                dateTimePicker2.Visible = true;
                label2.Visible = true;
                label3.Visible = true;

            }
            if (comboBox1.SelectedIndex == 4)
            {
                textBox1.Enabled = false;
            }
            else
            {
                textBox1.Enabled = true;
            }
            
        }

        public void RefreshAfterADD_EDIT(string ADD_EDIT)
        {
            if (ADD_EDIT == "ADD")
            {
                try
                {
                    dataGridView1.DataSource = BackEnd.Pocket.Populate_Head_DGV();
                    this.dataGridView1.CurrentCell = this.dataGridView1[1, dataGridView1.Rows.Count - 1];
                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.SelectedRows[0].Index;
                    Populate_Pocket_DGV(dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value.ToString()); //بيدوس على الريكورد الاخير
                }
                catch { }
            }
            //EDIT
            else
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    int Row = dataGridView1.CurrentRow.Index;
                    dataGridView1.DataSource = BackEnd.Pocket.Populate_Head_DGV();
                    try
                    {
                        this.dataGridView1.CurrentCell = this.dataGridView1[1, Row];
                        dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.SelectedRows[0].Index;
                    }
                    catch { }
                    Populate_Pocket_DGV(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[0].Value.ToString());//بيدوس على الريكورد اللى عليه هايلايت
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                Populate_Header_DGV();
                try
                { Populate_Pocket_DGV(dataGridView1.Rows[0].Cells[0].Value.ToString()); }
                catch
                { dataGridView2.DataSource = BackEnd.Pocket.Populate_Pocket_DGV("0"); }
            }
        }
        //بحث متقدم
        private void button4_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frm_Advanced_Search>().Any())
            {
                Application.OpenForms["frm_Advanced_Search"].BringToFront();
                return;
            }
            else
            {
                new frm_Advanced_Search().Show();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button7_Click(sender, e);
            }
        }

        private void frm_Pocket_Show_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                button1_Click(sender, e);
            }
            if (e.KeyCode==Keys.F2)
            {
                button3_Click(sender, e);
            }
            if (e.KeyCode == Keys.F5)
            {
                button4_Click(sender, e);
            }
        }

        private void frm_Pocket_Show_Shown(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (BackEnd.SessionInfo.Permissions[8].Equals('y'))
            {
                if (Application.OpenForms.OfType<FORM_REPORT_VIEWER>().Any())
                {
                    Application.OpenForms["FORM_REPORT_VIEWER"].BringToFront();
                    return;
                }
                else
                {
                    new FORM_REPORT_VIEWER(Report_parameter, "FullRperort", "rpt").Show();
                }
            }
            else
            {
                new frmDialog("لا توجد صلاحيات كافية").ShowDialog();
                return;
            }
        }


        private void dateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button7_Click(sender, e);
            }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button7_Click(sender, e);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                Populate_Pocket_DGV(row.Cells[0].Value.ToString());
                Summing_Lables();
            }
            
        }

        

       
    }
}
