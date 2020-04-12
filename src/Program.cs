using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
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



            Option inputOption = new Option(new string[] { "--input", "-i" }, description: "Input directory")
            {
                Required = true,
                Argument = new Argument<string>("input")
            };
            inputOption.AddValidator(DirectoryValidator);
            rootCommand.AddOption(
                inputOption
            );

            var outputOption =
                new Option(new string[] { "--output", "-o" }, description: "Output directory")
                {
                    Required = true,
                    Argument = new Argument<string>("output"),
                };
            outputOption.AddValidator(DirectoryValidator);

            rootCommand.AddOption(outputOption);

            rootCommand.AddOption(
                new Option(new string[] { "--hashtype", "-t" }, description: "Hash type to use? Default MD5.")
                {
                    Required = false,
                    Argument = new Argument<EHashType>("eHashType", () => EHashType.MD5),
                }
            );
            rootCommand.AddOption(
                new Option(new string[] { "--recursive", "-r" }, description: "Search recursively? Default true. \n")
                {
                    Required = false,
                    Argument = new Argument<bool>("isRecursive", () => true),
                }
            );
            rootCommand.AddOption(
                new Option(new string[] { "--pattern", "-p" }, description: "File pattern to search? Default all. \n")
                {
                    Required = false,
                    Argument = new Argument<Regex>("patternOfFiles", () => new Regex(@"(\w*.exe$|\w*.dat$)")),
                }
            );

            var downloadCommand = new Command("download", "Download definition");
            downloadCommand.AddArgument(
                new Argument<Uri>("definitionUri")
            );
            rootCommand.AddCommand(downloadCommand
            );
            downloadCommand.Handler = CommandHandler.Create<Uri>(KarlosumCLI.DownloadAsync);
            rootCommand.Handler = CommandHandler.Create<DirectoryInfo, DirectoryInfo, EHashType, bool, Regex>(KarlosumCLI.Run);

            return await rootCommand.InvokeAsync(args);
        }

        private static string? DirectoryValidator(OptionResult r)
        {
            var dir = r.GetValueOrDefault<DirectoryInfo>();
            return dir.Exists ? null : $"Directory {dir} doesn't exist!";
        }


    }
}