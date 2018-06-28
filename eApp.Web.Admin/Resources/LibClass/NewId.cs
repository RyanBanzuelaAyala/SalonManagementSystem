using eApp.Web.Admin.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eApp.Web.Admin.Resources.LibClass
{
    public class NewId
    {
        dbsmappEntities db = new dbsmappEntities();

        public string _GenerateId(string table)
        {
            try
            {
                using(var ctx = new dbsmappEntities())
                {
                    var result = ctx.Database.SqlQuery<int>("select Id from " + table + " order by Id desc").FirstOrDefault();

                    return Convert.ToString(result + 1);
                }
            }
            catch
            {
                return "1";
            }
        }
    }
}