using System;
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
            RootCommand rootCommand = new RootCommand(
                description: "Write hashes for files."
            );
            rootCommand.AddOption(
                new Option(new string[] { "--input", "-i" }, description: "Input directory")
                {
                    Required = true,
                    Argument = new Argument<string>("input")
                }
            );

            rootCommand.AddOption(
                new Option(new string[] { "--output", "-o" }, description: "Output directory")
                {
                    Required = true,
                    Argument = new Argument<string>("output")
                }
            );

            rootCommand.AddOption(
                new Option(new string[] { "--hashtype", "-t" }, description: "Hash type to use? Default MD5.")
                {
                    Required = false,
                    Argument = new Argument<EHashType>("eHashType"),
                }
            );
            rootCommand.AddOption(
                new Option(new string[] { "--recursive", "-r" }, description: "Search recursively? Default true. \n")
                {
                    Required = false,
                    Argument = new Argument<bool>("isRecursive"),

                }
            );
            rootCommand.AddOption(
                new Option(new string[] { "--pattern", "-p" }, description: "File pattern to search? Default all. \n")
                {
                    Required = false,
                    Argument = new Argument<string>("patternOfFiles")
                }
            );

            var downloadCommand = new Command("download", "Download definition");
            downloadCommand.AddArgument(
                new Argument<Uri>("definitionUri")
            );
            rootCommand.AddCommand(downloadCommand
            );
            downloadCommand.Handler = CommandHandler.Create<Uri>(KarlosumCLI.DownloadAsync);
            rootCommand.Handler = CommandHandler.Create<DirectoryInfo, DirectoryInfo, EHashType, bool, string>(KarlosumCLI.Run);

            return await rootCommand.InvokeAsync(args);
        }


    }
}