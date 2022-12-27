using System;
using System.Collections.Generic;

#nullable disable

namespace TextFinder.Models
{
    public partial class ExceptionLog
    {
        public int ExceptionLogId { get; set; }
        public string ExceptionSource { get; set; }
        public string ExceptionType { get; set; }
        public string InnerException { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string FileName { get; set; }
        public int LineNo { get; set; }
        public string Control { get; set; }
        public string InputText { get; set; }
        public string TranslatedText { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
