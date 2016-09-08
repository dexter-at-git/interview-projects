using System.Collections.Generic;

namespace Transcipher.Services
{
    public interface ITranscipherService
    {
        IEnumerable<string> GetEncryptionAlgorithms();
        string Encrypt(string input, string algorithm);
        string Decrypt(string input, string algorithm);
    }
}