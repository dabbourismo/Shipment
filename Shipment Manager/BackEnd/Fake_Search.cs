using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Shipment_Manager.BackEnd
{
    public static class Fake_Search
    {
        public static DataTable Show_Empty_DGV()
        {
            return DataAccessLayer.Get_TableData(@"SELECT Pockets_Head.Pocket_Serial AS [رقم 
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
Pockets.Amount_Kilo AS [كمية
ك],
Pockets.Amount_Ton AS [كمية
طن],
Pockets.Amount_Num AS [كمية
عدد],
Pockets.Trans_Piaster AS [فئة النقل
قرش], 
Pockets.Trans_Pound AS [فئة النقل
جنيه],
Pockets.Value_Piaster AS [القيمة
قرش],
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
Pockets.Cuts_Kilo AS [العجز
ك],
Pockets.Cuts_Ton AS [العجز
طن],
Pockets.Ship_Name AS [اسم المركب]
FROM
Pockets INNER JOIN
Pockets_Head ON Pockets.Pocket_Head_ID = Pockets_Head.ID INNER JOIN
Members ON Pockets_Head.Member_ID = Members.ID WHERE Pockets_Head.Company_Name='OVERWATCH' ");
        }
    }
}
