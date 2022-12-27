using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace TextFinder
{
    class TextFinder
    {
        List<OutputModel> outputModelList = new List<OutputModel>();
        public void ExtractText(string path, string textToSearch)
        {
            string _path = path;
            string _textToSearch = textToSearch;
            string[] dirs = Directory.GetDirectories(_path);

            foreach (var dir in dirs)
            {
                ExtractText(dir, "");
            }

            var files = Directory.GetFiles(_path).Where(x => Path.GetExtension(x) == ".vb").ToList();
            foreach (var filePath in files)
            {
                ScanFile(filePath, textToSearch);
                Console.WriteLine(filePath);
            }

            var outputJson = JsonSerializer.Serialize(outputModelList);
            File.WriteAllText("D:/jsonOutput.txt", outputJson);
        }

        public void ScanFile(string filePath, string textToSearch)
        {
            int counter = 0;

            foreach (string line in System.IO.File.ReadLines(filePath))
            {
                if (line.Contains(textToSearch))
                {
                    OutputModel outputModel = new OutputModel
                    {
                        Control = textToSearch,
                        FileName = Path.GetFileName(filePath),
                        InputText = line,
                        LineNo = counter
                    };
                    outputModelList.Add(outputModel);
                }
                System.Console.WriteLine(line);
                counter++;
            }

            System.Console.WriteLine("There were {0} lines.", counter);
        }
    }
}
