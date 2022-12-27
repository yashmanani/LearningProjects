using System;
using System.Collections.Generic;
using System.Text;

namespace TextFinder
{
    class UpdateModel
    {
        public UpdateModel(string line)
        {
            var split = line.Split(",");
            FileName = split[0];
            LineNo = int.Parse(split[1]);
            Control = split[2];
            InputText = split[3];
            TranslatedText = split[4];
        }
        public string FileName { get; set; }
        public int LineNo { get; set; }
        public string Control { get; set; }
        public string InputText { get; set; }
        public string TranslatedText { get; set; }

        public override string ToString()
        {
            return $"{FileName},{LineNo},{Control},{InputText},{TranslatedText}";
        }
    }
}
