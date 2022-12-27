using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using TextFinder.Models;

namespace TextFinder
{
    class Program
    {
        static void Main(string[] args)
        {            
            //Console.WriteLine("Enter Folder Path:");
            //string path = Console.ReadLine();

            //var startDate = DateTime.Now;

            //Console.WriteLine("Enter text to search");
            //var textToSearch = Console.ReadLine();

            //var textFinder = new TextFinder();
            //textFinder.ExtractText(path, textToSearch);

            //var endDate = DateTime.Now;
            //Console.WriteLine($"Start time {startDate}");
            //Console.WriteLine($"End time {endDate}");
            //Console.WriteLine($"Time diff {endDate.Subtract(startDate).TotalMilliseconds}");

            Console.WriteLine("Enter CSV File Path:");
            string CSVPath = Console.ReadLine();
            if (File.Exists(CSVPath))
            {
                TextUpdater textUpdater = new TextUpdater();
                textUpdater.UpdateFiles(CSVPath);
            }
            else
            {
                Console.WriteLine("Invalid path for CSV.");
            }

        }
    }
}
