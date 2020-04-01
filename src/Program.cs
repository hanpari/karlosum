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
                description: "Basic functionality"
            );


            rc.AddOption(
                new Option(new string[] { "--output", "-o" }, description: "Output directory")
                {
                    Required = true,
                    Argument = new Argument<string>("output")
                }
            );
            rc.AddOption(
                new Option(new string[] { "--input", "-i" }, description: "Input directory")
                {
                    Required = true,
                    Argument = new Argument<string>("input")
                }
            );


            rc.Handler = CommandHandler.Create<DirectoryInfo, DirectoryInfo>(KarlosumCLI.Run);

            return await rc.InvokeAsync(args);
        }


    }
}