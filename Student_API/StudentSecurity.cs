using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentDataAccess;

namespace Student_API
{
    public class StudentSecurity
    {
        public static bool login(String UserName, String  passWord)
        {
            using (Intern_DBEntities DB = new Intern_DBEntities())
            {
                return DB.StudentUsers.Any(StudentUser => StudentUser.UserName.Equals(UserName, StringComparison.OrdinalIgnoreCase)
                 && StudentUser.UserPassWord == passWord);
            }              
        }
    }
}