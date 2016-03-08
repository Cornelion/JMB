using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;

namespace ScrapNova
{
    public static class StringHelpers
    {   
        public static string clean(this String s)
        {
            s = Regex.Replace(s, "&#8217;", "'");
            return s = Regex.Replace(s, "&nbsp;", "");
        }
    }
}
