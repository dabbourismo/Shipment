using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace Shipment_Manager.BackEnd
{
    class SessionInfo
    {
        public static SqlConnection cn;
        //public static string Connection = @"Data Source=192.168.1.50\SQLExpress;Initial Catalog=TravelManager;User ID=dab;Password=Tr@nsport123123;MultipleActiveResultSets=True;";
        public static string Connection = @"Data Source=DESKTOP-KFHGNFO\SQLEXPRESS;Initial Catalog=TravelManager;Integrated Security=True";
        public static string UserName = "";
        public static string Permissions = "";
    }
}
