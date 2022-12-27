using System;
using System.Collections.Generic;
using System.Text;

namespace TextFinder
{
    class OutputModel
    {
        public string FileName { get; set; }
        public int LineNo { get; set; }
        public string Control { get; set; }
        public string InputText { get; set; }
    }
}
