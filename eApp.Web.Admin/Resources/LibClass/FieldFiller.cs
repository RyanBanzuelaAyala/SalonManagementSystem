using eApp.Web.Admin.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace eApp.Web.Admin.Resources.LibClass
{
    public class FieldFiller
    {
        public static void FillFieldFiller<T>(T theInstance)
        {
            dbsmappEntities db = new dbsmappEntities();            

            if(theInstance == null)
            {
                throw new ArgumentNullException();
            }

            var vin = theInstance.GetType().GetProperty("vin");
            var vinValue = vin.GetValue(theInstance, null);
            var whatstable = vin.DeclaringType.Name;
            
            foreach(var property in theInstance.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var theValue = property.GetValue(theInstance, null);
                
                var whatName = property.Name;
                
                if(string.IsNullOrEmpty(theValue.ToString()))
                {
                    var oldValue = db.Database.SqlQuery<string>("select " + whatName + " from " + whatstable + " where vehicleid = '" + vinValue + "'").FirstOrDefault();

                    if(!string.IsNullOrEmpty(oldValue.ToString()))
                    {
                        property.SetValue(theInstance, (oldValue.ToString()), null);
                    }
                    else
                    {
                        property.SetValue(theInstance, (""), null);
                    }                    
                }
            }
        }

        public static void FillFieldFiller<T>(IEnumerable<T> theInstance)
        {
            if(theInstance == null)
            {
                throw new ArgumentNullException();
            }

            foreach(var theItem in theInstance)
            {
                FillFieldFiller(theItem);
            }
        }
    }
}