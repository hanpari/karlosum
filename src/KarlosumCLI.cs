using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System;
using System.CommandLine;

#nullable enable
namespace Karlosum
{
    public class KarlosumCLI
    {
        public static int Run(DirectoryInfo input, DirectoryInfo output)
        {
            if (!input.Exists)
            {
                throw new InvalidEnumArgumentException(message: $"{input.FullName} doesnt exist!");
            }

            var con = new Converter(EHashType.MD5);
            var options = new EnumerationOptions() { RecurseSubdirectories = true };
            string outputFile = Path.Join(output.FullName, "log.txt");

            using (var tw = new StreamWriter(outputFile))
            {

                foreach (var file in Directory.EnumerateFiles(input.FullName, "*.txt", options))
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