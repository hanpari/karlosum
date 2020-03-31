using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

#nullable enable
namespace Karlosum
{

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
            rc.AddOption(
                new Option(new string[] { "--hash", "-h" }, description: "Hash")
                {
                    Required = false,
                    Argument = new Argument<EHashType>("hash")
                }
            );
            rc.Handler = CommandHandler.Create<FileInfo, string, EHashType>(SumIt);
            return await rc.InvokeAsync(args);
        }

        public static void SumIt(FileInfo fileinfo, string output = "nothing", EHashType hashes = EHashType.MD5)
        {


            var hash = CalculateHash(File.ReadAllBytes(fileinfo.FullName));

            System.Console.WriteLine($"{hashes} o: {output}");
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