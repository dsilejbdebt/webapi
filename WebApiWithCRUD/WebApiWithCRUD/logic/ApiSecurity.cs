using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace WebApiWithCRUD.logic
{
    public class ApiSecurity
    {
        public static bool VaidateUser(string username, string password)
        {
            string usrName = ConfigurationManager.AppSettings["AuthUsername"];
            string pwd = ConfigurationManager.AppSettings["AuthPassword"];
            // Check if it is valid credential  
            if (username == usrName && password == pwd)//CheckUserInDB(username, password))  
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}