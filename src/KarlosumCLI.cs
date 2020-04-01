using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System;
using System.CommandLine;
using static System.Console;

#nullable enable
namespace Karlosum
{
    public class KarlosumCLI
    {
        public static int Run(DirectoryInfo input,
                              DirectoryInfo output,
                              EHashType eHashType = EHashType.MD5,
                              bool isRecursive = true,
                              string patternOfFiles = "*")
        {
            if (!input.Exists)
            {
                Error.WriteLine($"Directory {input.FullName} doesnt exist! Exiting...");
                return 1;
            }
            if (input.Equals(output))
            {
                Error.WriteLine("Input directory cannot be the same as output directory! Exiting...");
                return 1;
            }

            var con = new Converter(eHashType);
            var options = new EnumerationOptions() { RecurseSubdirectories = isRecursive };
            string outputFile = Path.Join(output.FullName, $"{Environment.MachineName}.txt");

            if (File.Exists(outputFile))
            {
                Error.WriteLine($"{outputFile} already exists. Exiting...");
                return 1;
            }

            using (var tw = new StreamWriter(outputFile))
            {

                foreach (var file in Directory.EnumerateFiles(input.FullName,
                                                              searchPattern: patternOfFiles,
                                                              enumerationOptions: options))
                {
                    Console.WriteLine(file);
                    tw.WriteLine(
                        con.CreateHashToken(File.ReadAllBytes(file))
                );

                }
            }
            return 0;
        }
    }
}