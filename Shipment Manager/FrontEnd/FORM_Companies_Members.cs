using Shipment_Manager.BackEnd;
using Shipment_Manager.SubForms.Members;
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
    public partial class FORM_Companies_Members : Form
    {
        public FORM_Companies_Members()
        {
            InitializeComponent();
            dataGridView2.DataSource = Members.Populate_DGV();
            MiscSettings();
            //hide column [0]
            
        }
        /*===========================الشركات========================*/
        //اضافة
        private void button1_Click(object sender, EventArgs e)
        {

        }
        //تعديل
        private void button3_Click(object sender, EventArgs e)
        {

        }
        //مسح
        private void button2_Click(object sender, EventArgs e)
        {

        }
        /*===========================الاعضاء========================*/
        //اضافة
        private void button6_Click(object sender, EventArgs e)
        {
            if (BackEnd.SessionInfo.Permissions[1].Equals('y'))
            {
                if (Application.OpenForms.OfType<frm_Members_Add_Update>().Any())
                {
                    Application.OpenForms["frm_Members_Add_Update"].BringToFront();
                    return;
                }
                else
                {
                    new frm_Members_Add_Update(this).Show();
                }
            }
            else { new frmDialog("لا توجد صلاحيات كافية").ShowDialog(); }
        }
        //تعديل
        private void button5_Click(object sender, EventArgs e)
        {
            if (BackEnd.SessionInfo.Permissions[2].Equals('y'))
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    int Row = dataGridView2.CurrentRow.Index;
                    if (Application.OpenForms.OfType<frm_Members_Add_Update>().Any())
                    {
                        Application.OpenForms["frm_Members_Add_Update"].BringToFront();
                        return;
                    }
                    else
                    {
                        new frm_Members_Add_Update(this, dataGridView2[0, Row].Value.ToString()).Show();
                    }
                }
                else
                {
                    //MessageBox.Show("لا يوجد اعضاء لتعديلها");
                    this.Focus();
                }
            }
            else { new frmDialog("لا توجد صلاحيات كافية").ShowDialog(); }

        
        }
        //مسح
        private void button4_Click(object sender, EventArgs e)
        {
            if (BackEnd.SessionInfo.Permissions[3].Equals('y'))
            {
                foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                {
                    int Row = dataGridView2.CurrentRow.Index;
                    frmDialog dialog = new frmDialog("هل انت متأكد من رغبتك بحذف العضو المحدد؟ سيتم حذف جميع الحافظات المتعلقة بهذا العضو", true);
                    dialog.ShowDialog();
                    if (frmDialog.State)
                    {
                        if (Members.Delete(row.Cells[0].Value.ToString()))
                        {
                            dataGridView2.Focus();
                            dataGridView2.DataSource = Members.Populate_DGV();
                            try
                            {
                                this.dataGridView2.CurrentCell = this.dataGridView2[1, Row - 1];
                                dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView2.SelectedRows[0].Index;
                            }
                            catch
                            {

                            }
                        }
                    }

                }
            }
            else { new frmDialog("لا توجد صلاحيات كافية").ShowDialog(); }

        }
        //بحث
        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                dataGridView2.DataSource = Members.Search(Convert.ToString(textBox1.Text), null);
            }
            else
            {
                dataGridView2.DataSource = Members.Search(null, Convert.ToString(textBox1.Text));
            }
        }
        /*==============================Functions============================*/

        public void RefreshAfterADD_EDIT(string ADD_EDIT)
        {
            if (ADD_EDIT == "ADD")
            {
                try
                {
                    dataGridView2.DataSource = Members.Populate_DGV();
                    this.dataGridView2.CurrentCell = this.dataGridView2[1, dataGridView2.Rows.Count - 1];
                    dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView2.SelectedRows[0].Index;
                }
                catch { }
            }
            //EDIT
            else
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    int Row = dataGridView2.CurrentRow.Index;
                    dataGridView2.DataSource = Members.Populate_DGV();
                    try
                    {
                        this.dataGridView2.CurrentCell = this.dataGridView2[1, Row];
                        dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView2.SelectedRows[0].Index;
                    }
                    catch { }
                }
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                dataGridView2.DataSource = Members.Populate_DGV();
            }
        }

        public void MiscSettings()
        {
            comboBox1.SelectedIndex = 1;
            dataGridView2.Columns[0].Visible = false;
        }

        private void FORM_Companies_Members_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                button6_Click(sender, e);
            }
            if (e.KeyCode == Keys.F2)
            {
                button5_Click(sender, e);
            }
            if (e.Modifiers == Keys.Shift &&(e.KeyCode == Keys.PageUp) )
            {
                frmDialog dialog = new frmDialog("لقد تم الضغط على زر تصفية البيانات ، هل تريد بالتأكيد تصفية جميع بيانات البرنامج؟", true);
                dialog.ShowDialog();
                if (frmDialog.State)
                {
                    frmDialog dialog2 = new frmDialog("هل تريد بالتأكيد تصفية جميع بيانات البرنامج؟ هذه العملية لا ينصح بها الا اذا كنت متأكدا", true);
                    dialog2.ShowDialog();
                    if (frmDialog.State)
                    {
                        if (BackEnd.Members.Reset_Program())
                        {
                            new frmDialog("تم تصفية جميع بيانات البرنامج ، فضلا قم باعادة تشغيل البرنامج").ShowDialog();
                            Application.Exit();
                        }
                    }
                }
            }
        }

        private void FORM_Companies_Members_Shown(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button7_Click(sender, e);
            }
        }


        //تعديل ضغطة الزرار اليمين
        private void MenuClicked(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int Row = dataGridView2.CurrentRow.Index;
                button5_Click(sender, e);
            }
        }
        // حذف ضغطة زرار يمين
        private void MenuClicked2(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int Row = dataGridView2.CurrentRow.Index;
                button4_Click(sender, e);
            }
        }


        private void dataGridView2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dataGridView2.CurrentCell = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dataGridView2.Rows[e.RowIndex].Selected = true;
                dataGridView2.Focus();

                ContextMenu m = new ContextMenu();
                m.RightToLeft = RightToLeft.Yes;
                int currentMouseOverRow = dataGridView2.HitTest(e.X, e.Y).RowIndex;
                if (currentMouseOverRow >= 0)
                {
                    MenuItem x = new MenuItem(string.Format("تعديل العضو", currentMouseOverRow.ToString()));
                    x.Click += MenuClicked;
                    MenuItem y = new MenuItem(string.Format("حذف العضو", currentMouseOverRow.ToString()));
                    y.Click += MenuClicked2;
                    m.MenuItems.Add(x);
                    m.MenuItems.Add(y);
                }

                m.Show(dataGridView2, new Point(e.X, e.Y));
            }
        }

        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                m.RightToLeft = RightToLeft.Yes;
                int currentMouseOverRow = dataGridView2.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    MenuItem x = new MenuItem(string.Format("تعديل العضو", currentMouseOverRow.ToString()));
                    x.Click += MenuClicked;
                    MenuItem y = new MenuItem(string.Format("حذف العضو", currentMouseOverRow.ToString()));
                    y.Click += MenuClicked2;
                    m.MenuItems.Add(x);
                    m.MenuItems.Add(y);
                }
                m.Show(dataGridView2, new Point(e.X, e.Y));
            }
        }

       



    }

}
