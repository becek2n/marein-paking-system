using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Helper
{
    public static class SiteConfig
    {
        public static string LOG
        {
            get
            {
                return ConfigurationManager.AppSettings["Log"];
            }
        }
    }
}
