using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Karlosum
{
#nullable enable
    public static class Program
    {

        public static async Task<int> Main(params string[] args)
        {
            RootCommand rc = new RootCommand(
                description: "Popis"
            );
            rc.AddArgument(
                argument: new Argument<FileInfo>("fileinfo")
                {
                    Description = "Name of file",
                }
                );

            rc.AddOption(
                new Option(new string[] { "--output", "-o" }, description: "Output directory")
                {
                    Required = false,
                    Argument = new Argument<string>("output")
                }
            );
            rc.Handler = CommandHandler.Create<FileInfo, string>(SumIt);
            return await rc.InvokeAsync(args);
        }

        public static void SumIt(FileInfo fileinfo, string output = "nothing")
        {


            var hash = CalculateHash(File.ReadAllBytes(fileinfo.FullName));

            System.Console.WriteLine($"{hash} o: {output}");
        }

        static string CalculateHash(byte[] bytes, HashAlgorithm? algorithm = null)
        {
            var a = algorithm ?? MD5.Create();
            var sb = new StringBuilder();
            foreach (var byte_in_hash in a.ComputeHash(bytes))
            {
                sb.Append(byte_in_hash.ToString("x2"));
            }
            return sb.ToString();
        }

    }
}