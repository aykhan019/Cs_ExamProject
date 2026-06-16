using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class FileHelper
    {
        public static void WriteExceptionToFile(Exception ex)
        {
            StackTrace st = new StackTrace(ex, true);
            StackFrame frame = st.GetFrame(0);
            string fileName = frame.GetFileName();
            int line = frame.GetFileLineNumber();
            File.AppendAllText("Errors.txt", ex.Message + "\n");
        }
    }
}
