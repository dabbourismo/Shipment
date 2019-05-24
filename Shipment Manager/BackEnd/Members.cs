using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Shipment_Manager.BackEnd
{
    public static class Members
    {
        public static bool Add(int Member_M, string Member_Name)
        {
            if (DataAccessLayer.ExecuteNonQuery(@"INSERT INTO Members (Member_M,Member_Name) VALUES ('" + Member_M + "','" + Member_Name + "')"))
            { return true; }
            else
            { return false; }
        }
        public static bool Update(string ID, int Member_M, string Member_Name)
        {
            if (DataAccessLayer.ExecuteNonQuery(@"UPDATE Members SET Member_M='" + Member_M + "',Member_Name='" + Member_Name + "' WHERE ID='" + ID + "'"))
            { return true; }
            else
            { return false; }
        }
        public static bool Delete(string ID)
        {
            if (DataAccessLayer.ExecuteNonQuery(@"DELETE FROM Members WHERE ID='" + ID + "'"))
            { return true; }
            else
            { return false; }
        }
        public static DataTable Populate_DGV()
        {
            return DataAccessLayer.Get_TableData(@"SELECT
            ID, Member_M AS مسلسل, Member_Name AS [اسم العضو]
            FROM           
            Members");
        }
        public static ArrayList Info(string ID)
        {
            return DataAccessLayer.ExecuteReader(@"SELECT Member_M,Member_Name FROM Members WHERE ID='" + ID + "'", new string[] { "Member_M", "Member_Name" });
        }
        public static Decimal Next_M()
        {
            return Convert.ToDecimal(DataAccessLayer.ExecuteScalar(@"SELECT Member_M FROM Members ORDER BY Member_M DESC")) + 1;
        }
        public static bool Check_M(string Member_M)
        {
            if (DataAccessLayer.ExecuteScalar(@"SELECT Member_M FROM Members") == "NULL STRING")
            { return false; }
            else
            { return true; }
        }

        public static bool Reset_Program()
        {
            if (DataAccessLayer.ExecuteNonQuery(@"DELETE FROM Members"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static DataTable Search(string Member_M, string Member_Name)
        {
            if (Member_M != null) //>بالرقم
            {
                return DataAccessLayer.Get_TableData(@"SELECT
            ID, Member_M AS مسلسل, Member_Name AS [اسم العضو] FROM           
            Members WHERE Member_M LIKE '%" + Member_M + "%' ");
            }
            else if (Member_Name != null)//>بالاسم
            {
                return DataAccessLayer.Get_TableData(@"SELECT
            ID, Member_M AS مسلسل, Member_Name AS [اسم العضو] FROM           
            Members WHERE Member_Name LIKE '%" + Member_Name + "%' ");
            }
            else
            {
                return Populate_DGV();
            }
        }

    }
}
