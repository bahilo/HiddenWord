using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenWordBusiness.Exceptions
{
    public class ExceptionGenerator
    {

        public void exceptionFor<T>(
            string actionName, 
            string message,
            T input, 
            int? excludeValue = 0, 
            int? excludeValueLessThan = int.MinValue, 
            int? excludeValueGreaterThan = int.MaxValue, 
            bool? isExludeNullable = true )
        {

            bool isMatch = false;

            switch (actionName.ToUpper())
            {
                case "INSERT":
                    if ( !(input.GetType() == typeof(int)) && (bool)isExludeNullable && input == null)
                    {
                        isMatch = true;
                    }
                    //System.Diagnostics.Trace.Listeners.Add();
                    break;
                case "UPDATE":
                    if ( !(input.GetType() == typeof(int)) && (bool)isExludeNullable && input == null)
                    {
                        isMatch = true;
                    }
                    else if ( (input.GetType() == typeof(int)) && ((int)(object)input < excludeValueLessThan) )
                    {
                        isMatch = true;
                    }
                    else if (input.Equals(excludeValue))
                    {
                        isMatch = true;
                    }
                    break;
                case "DELETE":
                    if ( (input.GetType() == typeof(int)) && (int)(object)input > excludeValueGreaterThan)
                    {
                        isMatch = true;
                    }
                    break;
                case "READ":
                    if ( !(input.GetType() == typeof(int)) && (bool)isExludeNullable && input == null)
                    {
                        isMatch = true;
                    }
                    if ( (input.GetType() == typeof(int)) &&  input.Equals(excludeValue) )
                    {
                        isMatch = true;
                   }
                    break;
            }

            if (isMatch)
                throw new ApplicationException(
                    new StringBuilder()
                    .AppendFormat(message+ "\n\n")
                    .ToString());


        }
    }
}
