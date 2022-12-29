using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TextFinder.Models
{
    [Table("Translation")]
    [Keyless]
    public partial class TranslationData
    {
        public int TranslationId { get; set; }
        public string FileName { get; set; }
        public int LineNo { get; set; }
        public string Control { get; set; }
        public string InputText { get; set; }
        public string TranslatedText { get; set; }
    }
}
