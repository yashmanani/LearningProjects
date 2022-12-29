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

        public void UpdateFiles(string CSVPath)
        {
            //string AppSettings = File.ReadAllText("../../../AppSettings.json");
            //string ConnectionString = AppSettings.Substring(AppSettings.IndexOf("con\": \""), AppSettings.LastIndexOf("\"") - AppSettings.IndexOf("con\": \"")).Replace("con\": \"", "");
            GisMasterTranslationLogsContext dbContext = new GisMasterTranslationLogsContext();

            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";

            try
            {
                //updateModels = File.ReadLines(CSVPath)    //code to update from CSV File
                //    .Skip(1)
                //    .Select(line => new UpdateModel(line))
                //    .ToList();
                List<TranslationData> translationDatas = dbContext.TranslationDatas.ToList();

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                foreach (var file in translationDatas)
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
                            else if (file.Control == "SetToolTip" && lines[file.LineNo - 1].Contains($".{file.Control}(") && lines[file.LineNo - 1].Contains($"\"{file.InputText}\""))
                            {
                                lines[file.LineNo - 1] = lines[file.LineNo - 1].Replace($"\"{file.InputText}\"", $"\"{file.TranslatedText}\"");
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
                            }
                            else if (file.Control == "MsgBox" && lines[file.LineNo - 1].Contains($"{file.Control}(") && lines[file.LineNo - 1].Contains($"\"{file.InputText}\""))
                            {
                                lines[file.LineNo - 1] = lines[file.LineNo - 1].Replace($"\"{file.InputText}\"", $"\"{file.TranslatedText}\"");
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
                            }
                            else if (file.Control == "MsgBoxTitle" && lines[file.LineNo - 1].Contains("MsgBox(") && lines[file.LineNo - 1].Contains($"\"{file.InputText}\""))
                            {
                                lines[file.LineNo - 1] = lines[file.LineNo - 1].Replace($"\"{file.InputText}\"", $"\"{file.TranslatedText}\"");
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
                                    IsUpdate = false,
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

                var matchNotFoundExceptions = dbContext.MatchNotFoundExceptions.Local.ToList();

                foreach (var file in matchNotFoundExceptions)
                {
                    try
                    {
                        var lines = File.ReadLines(file.FileName).ToList();
                        if (lines.Where(x => x.Contains($".{file.Control} = \"{file.InputText}")).Count() > 0)
                        {
                            lines = lines.Select(x => x.Replace($".{file.Control} = \"{file.InputText}", $".{file.Control} = \"{file.TranslatedText}")).ToList(); ;
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

                            file.IsUpdate = true;
                            dbContext.Update(file);
                        }
                        else if (file.Control == "SetToolTip" && lines.Where(x => x.Contains($".{file.Control}(") && x.Contains($"\"{file.InputText}\"")).Count() > 0)
                        {
                            lines = lines.Select(x => x.Replace($"\"{file.InputText}\"", $"\"{file.TranslatedText}\"")).ToList();
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

                            file.IsUpdate = true;
                            dbContext.Update(file);
                        }
                        else if (file.Control == "MsgBox" && lines.Where(x => x.Contains($"{file.Control}(") && x.Contains($"\"{file.InputText}\"")).Count() > 0)
                        {
                            lines = lines.Select(x => x.Replace($"\"{file.InputText}\"", $"\"{file.TranslatedText}\"")).ToList();
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

                            file.IsUpdate = true;
                            dbContext.Update(file);
                        }
                        else if (file.Control == "MsgBoxTitle" && lines.Where(x => x.Contains("MsgBox(") && x.Contains($"\"{file.InputText}\"")).Count() > 0)
                        {
                            lines = lines.Select(x => x.Replace($"\"{file.InputText}\"", $"\"{file.TranslatedText}\"")).ToList();
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

                            file.IsUpdate = true;
                            dbContext.Update(file);
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
