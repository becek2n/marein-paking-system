using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Helper
{
    public static class Logging
    {
        public static void WriteErr(Exception objError)
        {
            StreamWriter oSw;
            string sErrMsg = "";
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;

            try
            {
                if (!Directory.Exists(SiteConfig.LOG))
                {
                    Directory.CreateDirectory(SiteConfig.LOG);
                }

                string _logFilePath = SiteConfig.LOG + System.DateTime.Today.ToString("yyyy-MM-dd") + "." + "txt";
                logFileInfo = new FileInfo(_logFilePath);
                logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists) logDirInfo.Create();
                if (!logFileInfo.Exists)
                {
                    fileStream = logFileInfo.Create();
                }
                else
                {
                    fileStream = new FileStream(_logFilePath, FileMode.Append);
                }
                oSw = new StreamWriter(fileStream);
                oSw.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + " [E] " + objError.Message);
                oSw.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + " [E] " + objError.InnerException);
                oSw.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + " [E] " + objError.StackTrace);
                oSw.WriteLine();
                oSw.WriteLine("==========================================================================================================");
                oSw.WriteLine();
                oSw.Close();

            }
            catch (Exception ex)
            {
                sErrMsg = ex.Message;
            }
        }
    }
}
