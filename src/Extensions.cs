using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

#nullable enable
namespace Karlosum
{
    public static class Extensions
    {
        private const string DEFINITION_FILE = "definition.txt";

        public static string ToHexadecimalString(this byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        public static IEnumerable<string> EnumerateFiles(this DirectoryInfo directory, string? pattern, EnumerationOptions? options)
        {
            return Directory.EnumerateFiles(directory.FullName, pattern, options);
        }

        public static void LogError(string message, Action<string>? log = null) =>
            (log ?? Console.Error.WriteLine)(message);

        public static HashSet<string>? RetrieveDefinitionSet(string filename = DEFINITION_FILE) => File.Exists(filename) ? new HashSet<string>(File.ReadAllLines(filename)) : null;

        public static string GenerateOutputFileName(string? basename = null) => String.Format("{0}.txt", (basename ?? Environment.MachineName));
        public static string DirectoryDoesNotExist(this DirectoryInfo dinfo) => $"Directory {dinfo} doesn't exist!";
    }


    public delegate System.Security.Cryptography.HashAlgorithm ObtainHashAlgorithm();

}