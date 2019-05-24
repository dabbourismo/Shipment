using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace Shipment_Manager.BackEnd
{
    public static class DataAccessLayer
    {
        public static SqlCommand cm = new SqlCommand();
        public static DataTable Get_TableData(string command)
        {
            cm.Connection = SessionInfo.cn;
            SqlDataAdapter adapter = new SqlDataAdapter(command, SessionInfo.cn);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public static bool ExecuteNonQuery(string command)
        {
            cm.Connection = SessionInfo.cn;
            cm.CommandText = command;
            try
            {
                cm.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            { return false; }

        }
        public static string ExecuteScalar(string command)
        {
            cm.Connection = SessionInfo.cn;
            cm.CommandText = command;
            if (string.IsNullOrEmpty(Convert.ToString(cm.ExecuteScalar())))
            {
                return null;
            }
            else
            {
                return Convert.ToString(cm.ExecuteScalar());
            }
        }
        public static ArrayList ExecuteReader(string command, string[] WhatDoYouWantToReturn)
        {
            cm.Connection = SessionInfo.cn;
            cm.CommandText = command;
            ArrayList arraylist = new ArrayList();
            SqlDataReader sqlreader = cm.ExecuteReader();
            while (sqlreader.Read())
            {
                foreach (string userinput in WhatDoYouWantToReturn)
                {
                    arraylist.Add(sqlreader[userinput].ToString());
                }
            }
            sqlreader.Close();
            return arraylist;
        }
    }
}
