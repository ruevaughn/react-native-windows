using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Net46
{
    class CustomLogFile : ICustomLogFile
    {
        private StreamWriter StreamWriter;

        public CustomLogFile(string fileName)
        {
            this.StreamWriter = System.IO.File.AppendText(fileName);
        }

        public void WriteLog(string logMessage)
        {
            this.StreamWriter.WriteLine(logMessage);
            this.StreamWriter.Flush();

        }
    }
}
