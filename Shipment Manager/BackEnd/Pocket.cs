using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Shipment_Manager.BackEnd
{
    public static class Pocket
    {
        public static DataTable Populate_Pocket_DGV(string Pocket_Head_ID)
        {
            return DataAccessLayer.Get_TableData(@"SELECT   
                                                    ID,
                                                    Pocket_M AS م,
                                                    Coupon_Num AS [ رقم
البون],
                                                    Amount_Kilo AS [كمية
ك],
                                                    Amount_Ton AS [كمية
طن],
                                                    Amount_Num AS [كمية
عدد],
                                                    Trans_Piaster AS [فئة النقل
قرش],
                                                    Trans_Pound AS [فئة النقل
جنيه],
                                                    Value_Piaster AS [القيمة
قرش],
                                                    Value_Pound AS [القيمة
جنيه],
                                                    Cargo_Type AS [بيان الحمولة
نوع البضاعة],
                                                    Cargo_From AS [بيان الحمولة
من],
                                                    Cargo_To AS [بيان الحمولة
الى],
                                                    Cargo_Ship_Date AS [بيان الحمولة
تاريخ الشحن],
                                                    Cargo_Car_Num AS [بيان الحمولة
رقم السيارة],
                                                    Charge AS عهدة, Cuts_Kilo AS [العجز
ك1], 
                                                    Cuts_Ton AS [العجز
طن],
                                                    Ship_Name AS [اسم المركب],Pocket_Head_ID
                                                    FROM Pockets WHERE Pocket_Head_ID = '" + Pocket_Head_ID + "'");
        }

        public static DataTable Populate_Head_DGV()
        {
            return DataAccessLayer.Get_TableData(@"SELECT
                                                    Pockets_Head.ID,
                                                    Pockets_Head.Pocket_Serial AS [رقم القيد],
                                                    Members.Member_M AS [مسلسل العضو],
                                                    Members.Member_Name AS [اسم العضو],
                                                    Pockets_Head.Company_Name AS [اسم الشركة], 
                                                    Pockets_Head.Pocket_Date AS التاريخ
                                                    FROM      
                                                    Members INNER JOIN
                                                    Pockets_Head ON Members.ID = Pockets_Head.Member_ID");
        }

        public static string Get_Member_ID(string Member_M)
        {
            return DataAccessLayer.ExecuteScalar(@"SELECT ID FROM Members WHERE Member_M = '"+Member_M+"'");
        }
        public static bool Add_Pocket_Head(string Member_M, string Company_Name, string Pocket_Date, string Pocket_Serial)
        {
            if (DataAccessLayer.ExecuteNonQuery(@"INSERT INTO Pockets_Head (Member_ID,Company_Name,Pocket_Date,Pocket_Serial) VALUES('" + Get_Member_ID(Member_M) + "','" + Company_Name + "','" + Pocket_Date + "','" + Pocket_Serial + "')"))
            { return true; }
            else
            { return false; }

        }


        public static string Get_Pocket_Head_ID(string Pocket_Serial)
        {
            return DataAccessLayer.ExecuteScalar(@"SELECT ID FROM Pockets_Head WHERE Pocket_Serial='" + Pocket_Serial + "'");
        }


        public static bool Add_Pocket_Info(string Pocket_Serial, string Pocket_M, string Coupon_Num, float Amount_Kilo, float Amount_Ton, float Amount_Num, float Trans_Piaster, float Trans_Pound, float Value_Piaster, float Value_Pound, string Cargo_Type, string Cargo_From, string Cargo_To, string Cargo_Ship_Date, string Cargo_Car_Num, string Charge, float Cuts_Kilo, float Cuts_Ton, string Ship_Name)
        {
            if (DataAccessLayer.ExecuteNonQuery(@"INSERT INTO Pockets (Pocket_Head_ID,Pocket_M,Coupon_Num,Amount_Kilo,Amount_Ton,Amount_Num,Trans_Piaster,Trans_Pound,Value_Piaster,Value_Pound,Cargo_Type,Cargo_From,Cargo_To,Cargo_Ship_Date,Cargo_Car_Num,Charge,Cuts_Kilo,Cuts_Ton,Ship_Name) VALUES ('" + Get_Pocket_Head_ID(Pocket_Serial) + "','" + Pocket_M + "','" + Coupon_Num + "','" + Amount_Kilo + "','" + Amount_Ton + "','" + Amount_Num + "','" + Trans_Piaster + "','" + Trans_Pound + "','" + Value_Piaster + "','" + Value_Pound + "','" + Cargo_Type + "','" + Cargo_From + "','" + Cargo_To + "','" + Cargo_Ship_Date + "','" + Cargo_Car_Num + "','" + Charge + "','" + Cuts_Kilo + "','" + Cuts_Ton + "','" + Ship_Name + "')"))
            { return true; }
            else
            { return false; }

        }
        public static bool Delete_Pocket(string Pocket_Head_ID)
        {
            if (DataAccessLayer.ExecuteNonQuery(@"DELETE FROM Pockets_Head WHERE ID ='" + Pocket_Head_ID + "'"))
            { return true; }
            else
            { return false; }
        }


        public static ArrayList Get_Header_Info(string Pocket_Header_ID)
        {
            return DataAccessLayer.ExecuteReader(@"SELECT  
Members.Member_M, 
Pockets_Head.Company_Name,
Pockets_Head.Pocket_Date,
Pockets_Head.Pocket_Serial
FROM          
Pockets_Head INNER JOIN Members ON Pockets_Head.Member_ID = Members.ID 
WHERE Pockets_Head.ID='" + Pocket_Header_ID + "'", new string[] { "Member_M", "Company_Name", "Pocket_Date", "Pocket_Serial" });
        }


        public static bool Update_Pocket_Head(string Pocket_Head_ID, string Member_M, string Company_Name, string Pocket_Date, string Pocket_Serial)
        {
            if (DataAccessLayer.ExecuteNonQuery(@"UPDATE Pockets_Head SET Member_ID='" + Get_Member_ID(Member_M) + "',Company_Name='" + Company_Name + "',Pocket_Date='" + Pocket_Date + "',Pocket_Serial='" + Pocket_Serial + "' WHERE ID='" + Pocket_Head_ID + "'"))
            { return true; }
            else
            { return false; }
        }


        //دي رحلات كانت موجودة بس اتعدلت
        public static bool Update_Pocket_Info(string Pocket_ID, string Pocket_Head_ID, string Pocket_M, string Coupon_Num, float Amount_Kilo, float Amount_Ton, float Amount_Num, float Trans_Piaster, float Trans_Pound, float Value_Piaster, float Value_Pound, string Cargo_Type, string Cargo_From, string Cargo_To, string Cargo_Ship_Date, string Cargo_Car_Num, float Charge, float Cuts_Kilo, float Cuts_Ton, string Ship_Name)
        {
            if (DataAccessLayer.ExecuteNonQuery(@"UPDATE Pockets SET Pocket_M='" + Pocket_M + "',Coupon_Num='" + Coupon_Num + "',Amount_Kilo='" + Amount_Kilo + "',Amount_Ton='" + Amount_Ton + "',Amount_Num='" + Amount_Num + "',Trans_Piaster='" + Trans_Piaster + "',Trans_Pound='" + Trans_Pound + "',Value_Piaster='" + Value_Piaster + "',Value_Pound='" + Value_Pound + "',Cargo_Type='" + Cargo_Type + "',Cargo_From='" + Cargo_From + "',Cargo_To='" + Cargo_To + "',Cargo_Ship_Date='" + Cargo_Ship_Date + "',Cargo_Car_Num='" + Cargo_Car_Num + "',Charge='" + Charge + "',Cuts_Kilo='" + Cuts_Kilo + "',Cuts_Ton='" + Cuts_Ton + "',Ship_Name='" + Ship_Name + "' WHERE ID='" + Pocket_ID + "'"))
            { return true; }
            else
            { return false; }
        }


        //دي الرحلات الجديدةيتعملها اضافة
        public static bool EDIT_Add_Pocket_Info(string Pocket_Head_ID, string Pocket_M, string Coupon_Num, float Amount_Kilo, float Amount_Ton, float Amount_Num, float Trans_Piaster, float Trans_Pound, float Value_Piaster, float Value_Pound, string Cargo_Type, string Cargo_From, string Cargo_To, string Cargo_Ship_Date, string Cargo_Car_Num, float Charge, float Cuts_Kilo, float Cuts_Ton, string Ship_Name)
        {
            if (DataAccessLayer.ExecuteNonQuery(@"INSERT INTO Pockets (Pocket_Head_ID,Pocket_M,Coupon_Num,Amount_Kilo,Amount_Ton,Amount_Num,Trans_Piaster,Trans_Pound,Value_Piaster,Value_Pound,Cargo_Type,Cargo_From,Cargo_To,Cargo_Ship_Date,Cargo_Car_Num,Charge,Cuts_Kilo,Cuts_Ton,Ship_Name) VALUES ('" + Pocket_Head_ID + "','" + Pocket_M + "','" + Coupon_Num + "','" + Amount_Kilo + "','" + Amount_Ton + "','" + Amount_Num + "','" + Trans_Piaster + "','" + Trans_Pound + "','" + Value_Piaster + "','" + Value_Pound + "','" + Cargo_Type + "','" + Cargo_From + "','" + Cargo_To + "','" + Cargo_Ship_Date + "','" + Cargo_Car_Num + "','" + Charge + "','" + Cuts_Kilo + "','" + Cuts_Ton + "','" + Ship_Name + "')"))
            { return true; }
            else
            { return false; }
        }


        public static bool EDIT_Delete_Pocket_Info(string Pocket_ID)
        {
            if (DataAccessLayer.ExecuteNonQuery(@"DELETE FROM Pockets WHERE ID='" + Pocket_ID + "'"))
            { return true; }
            else
            { return false; }
        }


        public static DataTable Search(string Pocket_Serial,string Member_M,string Member_Name,string Company_Name,string Date_Only,DateTime Date_From,DateTime Date_To)
        {
            if (Pocket_Serial != null)
            {
                return DataAccessLayer.Get_TableData(@"SELECT
                                                    Pockets_Head.ID,
                                                    Pockets_Head.Pocket_Serial AS [رقم القيد],
                                                    Members.Member_M AS [مسلسل العضو],
                                                    Members.Member_Name AS [اسم العضو],
                                                    Pockets_Head.Company_Name AS [اسم الشركة], 
                                                    Pockets_Head.Pocket_Date AS التاريخ
                                                    FROM      
                                                    Members INNER JOIN
                                                    Pockets_Head ON Members.ID = Pockets_Head.Member_ID WHERE Pockets_Head.Pocket_Serial LIKE '%" + Pocket_Serial + "%' ");
            }
            if (Member_M != null)
            {
                return DataAccessLayer.Get_TableData(@"SELECT
                                                    Pockets_Head.ID,
                                                    Pockets_Head.Pocket_Serial AS [رقم القيد],
                                                    Members.Member_M AS [مسلسل العضو],
                                                    Members.Member_Name AS [اسم العضو],
                                                    Pockets_Head.Company_Name AS [اسم الشركة], 
                                                    Pockets_Head.Pocket_Date AS التاريخ
                                                    FROM      
                                                    Members INNER JOIN
                                                    Pockets_Head ON Members.ID = Pockets_Head.Member_ID WHERE Members.Member_M LIKE '%" + Member_M + "%' AND (Pockets_Head.Pocket_Date BETWEEN '"+Date_From.ToString("yyyy/MM/dd")+"' and '"+Date_To.ToString("yyyy/MM/dd")+"') ");
            }
            if (Member_Name != null)
            {
                return DataAccessLayer.Get_TableData(@"SELECT
                                                    Pockets_Head.ID,
                                                    Pockets_Head.Pocket_Serial AS [رقم القيد],
                                                    Members.Member_M AS [مسلسل العضو],
                                                    Members.Member_Name AS [اسم العضو],
                                                    Pockets_Head.Company_Name AS [اسم الشركة], 
                                                    Pockets_Head.Pocket_Date AS التاريخ
                                                    FROM      
                                                    Members INNER JOIN
                                                    Pockets_Head ON Members.ID = Pockets_Head.Member_ID WHERE Members.Member_Name LIKE '%" + Member_Name + "%' AND (Pockets_Head.Pocket_Date BETWEEN '" + Date_From.ToString("yyyy/MM/dd") + "' and '" + Date_To.ToString("yyyy/MM/dd") + "')");
            }
            if (Company_Name != null)
            {
                return DataAccessLayer.Get_TableData(@"SELECT
                                                    Pockets_Head.ID,
                                                    Pockets_Head.Pocket_Serial AS [رقم القيد],
                                                    Members.Member_M AS [مسلسل العضو],
                                                    Members.Member_Name AS [اسم العضو],
                                                    Pockets_Head.Company_Name AS [اسم الشركة], 
                                                    Pockets_Head.Pocket_Date AS التاريخ
                                                    FROM      
                                                    Members INNER JOIN
                                                    Pockets_Head ON Members.ID = Pockets_Head.Member_ID WHERE Pockets_Head.Company_Name LIKE '%" + Company_Name + "%' AND (Pockets_Head.Pocket_Date BETWEEN '" + Date_From.ToString("yyyy/MM/dd") + "' and '" + Date_To.ToString("yyyy/MM/dd") + "')");
            }
            if (Date_Only!=null)
            {
                return DataAccessLayer.Get_TableData(@"SELECT
                                                    Pockets_Head.ID,
                                                    Pockets_Head.Pocket_Serial AS [رقم القيد],
                                                    Members.Member_M AS [مسلسل العضو],
                                                    Members.Member_Name AS [اسم العضو],
                                                    Pockets_Head.Company_Name AS [اسم الشركة], 
                                                    Pockets_Head.Pocket_Date AS التاريخ
                                                    FROM      
                                                    Members INNER JOIN
                                                    Pockets_Head ON Members.ID = Pockets_Head.Member_ID WHERE Pockets_Head.Pocket_Date BETWEEN '" + Date_From.ToString("yyyy/MM/dd") + "' and '" + Date_To.ToString("yyyy/MM/dd") + "'");
            }
            else
            {
                return Populate_Head_DGV();
            }
        }

        public static string Get_Member_Name(string Member_M)
        {
           return DataAccessLayer.ExecuteScalar(@"SELECT Member_Name FROM Members WHERE Member_M='" + Member_M + "'");
        }
        public static string Get_Member_M(string Member_Name)
        {
            return DataAccessLayer.ExecuteScalar(@"SELECT Member_M FROM Members WHERE Member_Name='" + Member_Name + "'");
        }

        public static string Get_Header_Serial_For_Edit(string Serial_Edit)
        {
            return DataAccessLayer.ExecuteScalar(@"SELECT Pocket_Serial FROM Pockets_Head WHERE Pocket_Serial = '" + Serial_Edit + "'");
        }
        public static string Get_Member_M_For_Edit(string Member_M)
        {
            return DataAccessLayer.ExecuteScalar(@"SELECT Member_M FROM Members WHERE Member_M = '" + Member_M + "'");
        }
        public static ArrayList AutoComplete_Member_Name()
        {
            return DataAccessLayer.ExecuteReader(@"SELECT Member_Name FROM Members", new string[] { "Member_Name" });
        }
        public static ArrayList AutoComplete_Member_M()
        {
            return DataAccessLayer.ExecuteReader(@"SELECT Member_M FROM Members", new string[] { "Member_M" });
        }
    }
}
