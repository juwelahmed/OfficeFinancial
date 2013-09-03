using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeFinancial.Common
{
   public  class ValidationHelper
    {
       public static bool IsTextNumeric(string str)
       {
           System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
           return reg.IsMatch(str);

       }
    }
}
