using System.Security.Cryptography;

#nullable enable
namespace Karlosum
{
    class Converter
    {
        public Converter(EHashType eHashType = EHashType.MD5)
        {
            HashAlgor = eHashType switch
            {
                EHashType.MD5 => MD5.Create(),
                EHashType.SHA1 => SHA1.Create(),
                EHashType.SHA256 => SHA256.Create(),
                EHashType.SHA512 => SHA512.Create(),

                _ => throw new System.Exception(string.Format("Not implemented hash {0}!", eHashType))
            };
        }

        HashAlgorithm HashAlgor { get; }

        public string CreateHashToken(byte[] buffer) => HashAlgor.ComputeHash(buffer).ToHexadecimalString();


    }
}