using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using SoftwareLocker;
namespace Shipment_Manager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

           TrialMaker t = new TrialMaker("TMTest1", Application.StartupPath + "\\RegFile365.reg",
Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\TMSetp365.dbf",
"Owner : م/محمد دبور",
365, 999999999, "914");
            // أول رقم ده عدد الايام والتاني عدد المرات والتالت الايدنتفاير

            byte[] MyOwnKey = { 97, 250, 1, 5, 84, 21, 7, 63,
            4, 54, 87, 56, 123, 10, 3, 62,
            7, 9, 20, 36, 37, 21, 101, 57};
            t.TripleDESKey = MyOwnKey;

            TrialMaker.RunTypes RT = t.ShowDialog();
            bool is_trial;
            if (RT != TrialMaker.RunTypes.Expired)
            {
                if (RT == TrialMaker.RunTypes.Full)
                    is_trial = false;
                else
                {
                    is_trial = true;
                }

                BackEnd.SessionInfo.cn = new SqlConnection();
                BackEnd.SessionInfo.cn.ConnectionString = BackEnd.SessionInfo.Connection;
                BackEnd.SessionInfo.cn.Open();
                try {
                    SqlCommand cm = new SqlCommand();
                    cm.Connection = BackEnd.SessionInfo.cn;
                    cm.CommandText= "CREATE TABLE [dbo].[Users]([ID][int] IDENTITY(1, 1) NOT NULL, [UserName] [nvarchar](max) NULL,[Password][nvarchar](max) NULL,[Permissions][nvarchar](max) NULL,CONSTRAINT[PK_Users] PRIMARY KEY CLUSTERED([ID] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY]";
                    cm.ExecuteNonQuery();
                    cm.CommandText = "Insert into Users(UserName,Password,Permissions) values('Admin','+Vs4ZHwle88=','yyyyyyyyyyyyyy')";
                    cm.ExecuteNonQuery();
                }
                catch { }
                Application.Run(new FrontEnd.Login());
            }
        }
    }
    }
//}
