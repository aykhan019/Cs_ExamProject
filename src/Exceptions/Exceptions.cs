using System;

namespace ApplicationsExceptionNamespace
{
    public class DetailedException : Exception
    {
        public DetailedException(string messageOfException, DateTime date, int line, string file)
        {
            MessageOfException = messageOfException;
            Date = date;
            Line = line;
            File = file;
        }
        public string MessageOfException { get; set; }
        public DateTime Date { get; set; }
        public int Line { get; set; }
        public string File { get; set; }
        public override string Message
        {
            get
            {
                return $@"====================================================================================
Error : {MessageOfException}
Date : {Date.ToLongDateString()}, {Date.ToLongTimeString()}
Line : {Line}
File : {File}";
            }
        }
    }

}
