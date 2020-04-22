using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

#nullable enable
namespace Karlosum
{
    public struct Settings
    {
        public Uri definitionUri;
        public EHashType hashType;
    }
    public static class Extensions
    {
        private const string DEFINITION_FILE = "definition.txt";

        /// <summary>
        /// From any byte array creates its hexadecimal presentation as a string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns>Hexadecimal presentation</returns>
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

        /// <summary>
        /// Log message to given logger
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="log">Delegate to custom logger.</param>
        public static void LogError(string message, Action<string>? log = null) =>
            (log ?? Console.Error.WriteLine)(message);

        public static HashSet<string>? RetrieveDefinitionSet(string filename = DEFINITION_FILE) => File.Exists(filename) ? new HashSet<string>(File.ReadAllLines(filename)) : null;

        public static string GenerateOutputFileName(string? basename = null) => String.Format("{0}.txt", (basename ?? Environment.MachineName));
        public static string DirectoryDoesNotExist(this DirectoryInfo dinfo) => $"Directory {dinfo} doesn't exist!";
    }


}