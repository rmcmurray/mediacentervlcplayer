using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MediaCenterVLCPlayer
{
    public class Logger
    {
        private string logFile = MediaCenterVLCPlayer.Properties.Settings.Default.VlcPluginsPath + @"..\MediaCenterVLCPlayer.log";
        public static Logger Instance;
        private FileStream fStream;
        private StreamWriter writer;

        public Logger()
        {
            Logger.Instance = this;
            fStream = File.Open(logFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
            writer = new StreamWriter(fStream);
            writer.AutoFlush = true;
        }

        ~Logger()
        {
            Close();
        }

        public void Close()
        {
            try
            {
                if (writer != null)
                    writer.Close();

                if (fStream != null)
                    fStream.Close();
            }
            catch (Exception) { }
        }

        public void writeToLog(string msg)
        {
            writer.WriteLine(msg);
            writer.Flush();
        }

        public static void WriteToLog(string msg)
        {
            //Logger.Instance.writeToLog(msg);
        }
    }
}
