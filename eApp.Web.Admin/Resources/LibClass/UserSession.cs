using eApp.Web.Admin.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eApp.Web.Admin.Resources.LibClass
{
    public static class UserSession
    {
        public static bool hasSessionRole(string userid)
        {
            var db = new dbsmappEntities();

            var isExist = db.userappsessions.Any(s => s.userid.Equals(userid));

            if(isExist)
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