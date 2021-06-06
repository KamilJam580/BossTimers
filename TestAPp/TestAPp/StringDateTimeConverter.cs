using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestAPp
{
    public static class StringDateTimeConverter 
    {
        public static string GetTimeText(TimeSpan timeToDefeat)
        {
            string formatted = "";
            if (timeToDefeat > TimeSpan.Zero)
            {
                if (timeToDefeat.Days > 0)
                {
                    formatted += string.Format("{0:%d} days", timeToDefeat);
                    formatted += "\n";
                }
                formatted += string.Format("{0:%h}:{0:%m}:{0:%s}", timeToDefeat);
            }
            else
            {
                formatted = "OK";
            }
            return formatted;
        }
    }
}
