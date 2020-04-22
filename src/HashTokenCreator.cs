using System.Security.Cryptography;

#nullable enable
namespace Karlosum
{
    public class HashTokenCreator
    {
        public HashTokenCreator(EHashType eHashType = EHashType.MD5)
        {
            HashAlgor = eHashType switch
            {
                EHashType.MD5 => MD5.Create(),
                EHashType.SHA1 => SHA1.Create(),
                EHashType.SHA256 => SHA256.Create(),
                EHashType.SHA512 => SHA512.Create(),
                _ => throw new System.ArgumentException($"Not implemented hash {eHashType}!")
            };
        }

        HashAlgorithm HashAlgor { get; }

        public byte[] ComputeHash(byte[] buffer) => HashAlgor.ComputeHash(buffer);

        /// <summary>
        /// From given byte array creates a hexadecimal string
        /// representation of its hash.
        /// </summary>
        /// <param name="buffer">Typically, all bytes from file, but it could by arbitrary byte array.</param>
        /// <returns>
        /// Hexadecimal representation of a computed hash.
        /// </returns>
        public string CreateHashToken(byte[] buffer) => ComputeHash(buffer).ToHexadecimalString();

    }
}