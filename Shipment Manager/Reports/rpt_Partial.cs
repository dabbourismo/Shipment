using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Shipment_Manager.Reports
{
    public partial class rpt_Partial : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt_Partial(string Report_Title)
        {
            InitializeComponent();
            xrLabel13.Text = Report_Title;
        }

    }
}
