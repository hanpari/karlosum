using System.Linq;
using System.Text.RegularExpressions;
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
                              Regex? patternOfFiles = null)
        {


            try
            {

                if (!input.Exists)
                    throw new ArgumentException(input.DirectoryDoesNotExist());

                if (!output.Exists)
                    throw new ArgumentException(output.DirectoryDoesNotExist());

                if (input.Equals(output))
                    throw new ArgumentException("Input directory cannot be the same as output directory!");

                var con = new HashTokenCreator(eHashType);
                var options = new EnumerationOptions() { RecurseSubdirectories = isRecursive };
                string outputFile = Path.Join(output.FullName, Extensions.GenerateOutputFileName());

                using var tw = new StreamWriter(outputFile);

                var files = Directory.EnumerateFiles(input.FullName, "*", enumerationOptions: options);

                if (patternOfFiles != null)
                {
                    files = files.Where(x => patternOfFiles.IsMatch(x));
                }

                foreach (string file in files)
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
    }
}