using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FSD.Service.Services
{
    public class ReportService
    {
        public readonly string FILE_PATH = "ReportFile.txt";

        public DateTime Start { get; set; }
        public string Text { get; set; }

       
        private string CalculateTime()
        {
            TimeSpan time = DateTime.Now - Start;
            return time.ToString();
        }

        public  void Save()
        {
            string reportText = $"{DateTime.Now} ) {Text} : {CalculateTime()} " + Environment.NewLine;
            if (!File.Exists(FILE_PATH))
            {
                File.WriteAllText(FILE_PATH, reportText);
            }

            File.AppendAllText(FILE_PATH, reportText);
        }
    }
}
