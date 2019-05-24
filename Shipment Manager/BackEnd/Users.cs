using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shipment_Manager.BackEnd
{
    class Users
    {
        SqlCommand cm = new SqlCommand();
    SqlDataReader reader;
    public Users()
    {
        cm.Connection = SessionInfo.cn;
    }
    public ArrayList GetAllUsers()
    {
        ArrayList studentArray = new ArrayList();
        cm.CommandText = "select * from Users";
        reader = cm.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                studentArray.Add(reader["UserName"]);
            }
        }
        reader.Close();
        return studentArray;
    }
    public ArrayList GetAllPermissions()
    {
        ArrayList studentArray = new ArrayList();
        cm.CommandText = "select * from Users";
        reader = cm.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                studentArray.Add(reader["Permissions"]);
            }
        }
        reader.Close();
        return studentArray;
    }
    public ArrayList GetAllPasswords()
    {
        ArrayList studentArray = new ArrayList();
        cm.CommandText = "select * from Users";
        reader = cm.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                studentArray.Add(reader["Password"]);
            }
        }
        reader.Close();
        return studentArray;
    }
    public bool AddNewUser(string user, string password, string permissions)
    {
        try
        {
            cm.CommandText = "select UserName from Users Where UserName='" + user + "'";
            cm.ExecuteScalar().ToString();
            return false;
        }
        catch
        {
            try
            {
                cm.CommandText = "insert into Users(UserName,password,permissions) values('" + user + "','" + password + "','" + permissions + "')";
                cm.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return false;
            }
        }
    }

    public bool Updatepassword(string user, string newpassword)
    {
        try
        {
            cm.CommandText = "update Users set password='" + newpassword + "' where UserName='" + user + "'";
            cm.ExecuteNonQuery();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Updatepermissions(string user, string permissions)
    {
        try
        {
            cm.CommandText = "update Users set Permissions='" + permissions + "' where UserName='" + user + "'";
            cm.ExecuteNonQuery();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool DeleteUser(string user)
    {
        try
        {
            cm.CommandText = "select count(UserName) from Users";
            int count = int.Parse(cm.ExecuteScalar().ToString());
            if (count == 1)
            {
                return false;
            }
            else
            {
                cm.CommandText = "Delete From Users where UserName='" + user + "'";
                cm.ExecuteNonQuery();
                return true;
            }
        }
        catch
        {
            return false;
        }
    }
}
}
