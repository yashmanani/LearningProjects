using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using TextFinder.Models;
using System.Text.Json;

namespace TextFinder
{
    class TextUpdater
    {
        List<UpdateModel> updateModels = new List<UpdateModel>();

        public void UpdateFiles(string CSVPath)
        {
            //string AppSettings = File.ReadAllText("../../../AppSettings.json");
            //string ConnectionString = AppSettings.Substring(AppSettings.IndexOf("con\": \""), AppSettings.LastIndexOf("\"") - AppSettings.IndexOf("con\": \"")).Replace("con\": \"", "");
            GisMasterTranslationLogsContext dbContext = new GisMasterTranslationLogsContext();

            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";

            try
            {
                updateModels = File.ReadLines(CSVPath)
                    .Skip(1)
                    .Select(line => new UpdateModel(line))
                    .ToList();

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                foreach (var file in updateModels)
                {
                    try
                    {
                        if (File.Exists(file.FileName))
                        {
                            var lines = File.ReadLines(file.FileName).ToList();
                            if (lines[file.LineNo - 1].Contains($".{file.Control} = \"{file.InputText}"))
                            {
                                lines[file.LineNo - 1] = lines[file.LineNo - 1].Replace($".{file.Control} = \"{file.InputText}", $".{file.Control} = \"{file.TranslatedText}");
                                File.WriteAllLines(file.FileName, lines);

                                SuccessfullyUpdatedLog successfullyUpdatedLog = new SuccessfullyUpdatedLog
                                {
                                    FileName = file.FileName,
                                    LineNo = file.LineNo,
                                    Control = file.Control,
                                    InputText = file.InputText,
                                    TranslatedText = file.TranslatedText,
                                    CreatedOn = DateTime.Now
                                };
                                dbContext.Add(successfullyUpdatedLog);
                                //using StreamWriter fileWriter = new StreamWriter($"{path}/SuccessfullyUpdated.txt", append: true);
                                //fileWriter.WriteLine(file.ToString());
                            }
                            else
                            {
                                MatchNotFoundException matchNotFoundException = new MatchNotFoundException
                                {
                                    FileName = file.FileName,
                                    LineNo = file.LineNo,
                                    Control = file.Control,
                                    InputText = file.InputText,
                                    TranslatedText = file.TranslatedText,
                                    CreatedOn = DateTime.Now
                                };
                                dbContext.Add(matchNotFoundException);
                                //using StreamWriter fileWriter = new StreamWriter($"{path}/MatchNotFound.txt", append: true);
                                //fileWriter.WriteLine(file.ToString());
                            }
                        }
                        else
                        {
                            FileNotExistException fileNotExistException = new FileNotExistException
                            {
                                FileName = file.FileName,
                                LineNo = file.LineNo,
                                Control = file.Control,
                                InputText = file.InputText,
                                TranslatedText = file.TranslatedText,
                                CreatedOn = DateTime.Now
                            };
                            dbContext.Add(fileNotExistException);
                            //using StreamWriter fileWriter = new StreamWriter($"{path}/FileNotExist.txt", append: true);
                            //fileWriter.WriteLine(file.FileName);
                        }
                    }
                    catch (Exception e)
                    {
                        ExceptionLog exceptionLog = new ExceptionLog
                        {
                            ExceptionSource = e.Source,
                            Message = e.Message,
                            ExceptionType = e.GetType().ToString(),
                            InnerException = e.InnerException != null ? JsonSerializer.Serialize(e.InnerException) : null,
                            StackTrace = e.StackTrace,
                            FileName = file.FileName,
                            LineNo = file.LineNo,
                            Control = file.Control,
                            InputText = file.InputText,
                            TranslatedText = file.TranslatedText,
                            CreatedOn = DateTime.Now
                        };

                        using StreamWriter fileWriter = new StreamWriter($"{path}/ExceptionLogs.txt", append: true);
                        fileWriter.WriteLine(JsonSerializer.Serialize(exceptionLog));

                        dbContext.Add(exceptionLog);
                        dbContext.SaveChanges();
                        //Console.Write(e);
                    }
                }
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                ExceptionLog exceptionLog = new ExceptionLog
                {
                    ExceptionSource = e.Source,
                    Message = e.Message,
                    ExceptionType = e.GetType().ToString(),
                    InnerException = e.InnerException != null ? JsonSerializer.Serialize(e.InnerException) : null,
                    StackTrace = e.StackTrace,
                    CreatedOn = DateTime.Now
                };

                using StreamWriter fileWriter = new StreamWriter($"{path}/ExceptionLogs.txt", append: true);
                fileWriter.WriteLine(JsonSerializer.Serialize(exceptionLog));

                //    dbContext.Add(exceptionLog);
                //    try
                //    {
                //        dbContext.SaveChanges();

                //    }
                //    catch (Exception e1)
                //    {
                //        Console.Write(e1);

                //    }
                Console.Write(e);
            }
            finally
            {
                dbContext.Dispose();
            }
        }
    }
}
