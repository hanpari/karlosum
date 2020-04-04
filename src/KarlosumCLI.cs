using System.Linq.Expressions;
using System.Net.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System;
using System.CommandLine;
using System.Net;
using System.Threading.Tasks;
using static System.Console;
using static Karlosum.Extensions;


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


            try
            {

                if (!input.Exists)
                    throw new ArgumentException($"Directory {input.FullName} doesn't exist!");

                if (!output.Exists)
                    throw new ArgumentException($"Directory {output.FullName} doesnt exist!");

                if (input.Equals(output))
                    throw new ArgumentException("Input directory cannot be the same as output directory!");

                var con = new HashTokenCreator(eHashType);
                var options = new EnumerationOptions() { RecurseSubdirectories = isRecursive };
                string outputFile = Path.Join(output.FullName, $"{Environment.MachineName}.txt");

                if (File.Exists(outputFile))
                    throw new ArgumentException($"Output file: {outputFile} already exists!");

                using var tw = new StreamWriter(outputFile);
                foreach (var file in Directory.EnumerateFiles(input.FullName, searchPattern: patternOfFiles, enumerationOptions: options))
                {
                    tw.WriteLine(
                        con.CreateHashToken(File.ReadAllBytes(file))
                );

                }
            }
            catch (ArgumentException ex)
            {
                LogError(ex.Message);
                return 1;
            }
            return 0;
        }

        public static async Task DownloadAsync(Uri definitionUri)
        {
            var client = new HttpClient();
            var bytes = await client.GetByteArrayAsync(definitionUri);
            File.WriteAllBytes(
                "definition.txt",
                bytes
            );
        }
    }
}