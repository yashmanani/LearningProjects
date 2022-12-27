using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace TextFinder
{
    class TextUpdater
    {
        List<UpdateModel> updateModels = new List<UpdateModel>();

        public void UpdateFiles(string CSVPath)
        {
            updateModels = File.ReadLines(CSVPath)
            .Skip(1)
            .Select(line => new UpdateModel(line))
            .ToList();

            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach (var file in updateModels)
            {
                if (File.Exists(file.FileName))
                {
                    var lines = File.ReadLines(file.FileName).ToList();
                    if (lines[file.LineNo - 1].Contains($".{file.Control} = \"{file.InputText}"))
                    {
                        lines[file.LineNo - 1] = lines[file.LineNo - 1].Replace($".{file.Control} = \"{file.InputText}", $".{file.Control} = \"{file.TranslatedText}");
                        File.WriteAllLines(file.FileName, lines);

                        using StreamWriter fileWriter = new StreamWriter($"{path}/SuccessfullyUpdated.txt", append: true);
                        fileWriter.WriteLine(file.ToString());
                    }
                    else
                    {
                        using StreamWriter fileWriter = new StreamWriter($"{path}/MatchNotFound.txt", append: true);
                        fileWriter.WriteLine(file.ToString());
                    }
                }
                else
                {
                    using StreamWriter fileWriter = new StreamWriter($"{path}/FileNotExist.txt", append: true);
                    fileWriter.WriteLine(file.FileName);
                }
            }
        }
    }
}
