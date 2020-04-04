using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

#nullable enable
namespace Karlosum
{
    public static class Extensions
    {
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
    }
}