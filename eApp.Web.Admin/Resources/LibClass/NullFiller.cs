using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace eApp.Web.Admin.Resources.LibClass
{
    public static class NullFiller
    {
        public static void FillNullFields<T>(T theInstance)
        {
            if(theInstance == null)
            {
                throw new ArgumentNullException();
            }


            foreach(var property in theInstance.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var theValue = property.GetValue(theInstance, null);

                if(theValue == null)
                {
                    property.SetValue(theInstance, (""), null);
                }
            }
        }

        public static void FillNullFields<T>(IEnumerable<T> theInstance)
        {
            if(theInstance == null)
            {
                throw new ArgumentNullException();
            }

            foreach(var theItem in theInstance)
            {
                FillNullFields(theItem);
            }
        }
    }

}