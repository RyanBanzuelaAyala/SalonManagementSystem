using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace eApp.Web.Admin.Resources.LibClass
{
    public static class Capitalize
    {
        public static void UppercaseClassFields<T>(T theInstance)
        {
            if(theInstance == null)
            {
                throw new ArgumentNullException();
            }

            foreach(var property in theInstance.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var theValue = property.GetValue(theInstance, null);
                if(theValue is string)
                {
                    property.SetValue(theInstance, ((string)theValue).ToUpper(), null);
                }
            }
        }

        public static void UppercaseClassFields<T>(IEnumerable<T> theInstance)
        {
            if(theInstance == null)
            {
                throw new ArgumentNullException();
            }

            foreach(var theItem in theInstance)
            {
                UppercaseClassFields(theItem);
            }
        }

    }
    
}