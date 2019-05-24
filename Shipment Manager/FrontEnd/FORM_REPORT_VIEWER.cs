using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;

namespace Shipment_Manager.FrontEnd
{
    public partial class FORM_REPORT_VIEWER : DevExpress.XtraEditors.XtraForm
    {
        string RowFilter = "";
        string Message = "";
        public FORM_REPORT_VIEWER()
        {
            InitializeComponent();
        }

        public FORM_REPORT_VIEWER(string RowFilter,string Message)
        {
            InitializeComponent();
            this.Message = Message;
            this.RowFilter = RowFilter;
            string Command = @"SELECT 
Pockets_Head.Pocket_Serial AS [رقم القيد],
Members.Member_M AS [مسلسل
العضو],
Members.Member_Name AS [اسم العضو],
Pockets_Head.Company_Name AS شركة, 
Pockets_Head.Pocket_Date AS التاريخ, 
Pockets.Pocket_M AS م,
Pockets.Coupon_Num AS [رقم البون],
Pockets.Amount_Kilo AS ك,
Pockets.Amount_Ton AS طن,
Pockets.Amount_Num AS عدد,
Pockets.Trans_Piaster AS [قرش 1],
Pockets.Trans_Pound AS [جنيه 1], 
Pockets.Value_Piaster AS قرش,
Pockets.Value_Pound AS جنية,
Pockets.Cargo_Type AS [نوع البضاعه],
Pockets.Cargo_From AS من,
Pockets.Cargo_To AS الى,
Pockets.Cargo_Car_Num AS [رقم السيارة], 
Pockets.Cargo_Ship_Date AS [تاريخ الشحن],
Pockets.Charge AS عهدة,
Pockets.Cuts_Kilo AS [العجز ك],
Pockets.Cuts_Ton AS [العجز طن],
Pockets.Ship_Name AS [اسم المركب]
FROM
Pockets INNER JOIN
Pockets_Head ON Pockets.Pocket_Head_ID = Pockets_Head.ID INNER JOIN
Members ON Pockets_Head.Member_ID = Members.ID WHERE " + RowFilter + " ";
            Partial_Report(Command, "Pockets_Head");
        }
        public FORM_REPORT_VIEWER(string RowFilter,string FullReport,string rpt)
        {
            InitializeComponent();
            this.RowFilter = RowFilter;
            string Command = @"SELECT  
Pockets.Pocket_M AS م,
Pockets.Ship_Name AS [اسم المركب],
Pockets_Head.Pocket_Serial AS [رقم القيد],
Pockets_Head.Pocket_Date AS التاريخ,
Pockets_Head.Company_Name AS شركة, 
Pockets.Coupon_Num AS [رقم البون],
Pockets.Amount_Kilo AS ك,
Pockets.Amount_Ton AS طن,
Pockets.Amount_Num AS عدد,
Pockets.Trans_Piaster AS [قرش 1],
Pockets.Trans_Pound AS [جنيه 1], 
Pockets.Value_Piaster AS قرش,
Pockets.Value_Pound AS جنية,
Pockets.Cargo_Type AS [نوع البضاعه],
Pockets.Cargo_From AS من,
Pockets.Cargo_To AS الى,
Pockets.Cargo_Car_Num AS [رقم السيارة], 
Pockets.Cargo_Ship_Date AS [تاريخ الشحن],
Pockets.Charge AS عهدة,
Pockets.Cuts_Kilo AS [العجز ك],
Pockets.Cuts_Ton AS [العجز طن],
Members.Member_Name AS [اسم العضو]
FROM Pockets INNER JOIN
Pockets_Head ON Pockets.Pocket_Head_ID = Pockets_Head.ID INNER JOIN
Members ON Pockets_Head.Member_ID = Members.ID
WHERE " + RowFilter + "";
            Full_Report(Command, "Pockets_Head");
        }


        public void Full_Report(string SqlCommand, string TableName)
        {
            DataSet1 ds = new DataSet1();
            SqlDataAdapter sda = new SqlDataAdapter(SqlCommand, BackEnd.SessionInfo.cn);
            sda.Fill(ds.Tables[TableName]);
            Reports.rpt_Pocket rpt_Pocket = new Reports.rpt_Pocket();
            //XtraReport report = XtraReport.FromFile(Directory.GetCurrentDirectory() + @"\SavedReports\rpt_StudentsData.repx", true);
            rpt_Pocket.DataSource = ds.Tables[TableName];
            rpt_Pocket.DataMember = "ValidDataMember";
            documentViewer1.DocumentSource = rpt_Pocket;
            documentViewer1.Refresh();
        }



        public void Partial_Report(string SqlCommand, string TableName)
        {
            DataSet1 ds = new DataSet1();
            SqlDataAdapter sda = new SqlDataAdapter(SqlCommand, BackEnd.SessionInfo.cn);
            sda.Fill(ds.Tables[TableName]);
            Reports.rpt_Partial rpt_Partial = new Reports.rpt_Partial(Message);
            rpt_Partial.DataSource = ds.Tables[TableName];
            rpt_Partial.DataMember = "ValidDataMember";
            documentViewer1.DocumentSource = rpt_Partial;
            documentViewer1.Refresh();
        }
    }
}