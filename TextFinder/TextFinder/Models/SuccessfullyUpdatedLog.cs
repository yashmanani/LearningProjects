using System;
using System.Collections.Generic;

#nullable disable

namespace TextFinder.Models
{
    public partial class SuccessfullyUpdatedLog
    {
        public int SuccessfullyUpdatedLogId { get; set; }
        public string FileName { get; set; }
        public int LineNo { get; set; }
        public string Control { get; set; }
        public string InputText { get; set; }
        public string TranslatedText { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
