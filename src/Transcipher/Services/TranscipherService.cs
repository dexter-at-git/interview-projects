using System.Collections.Generic;
using System.Linq;
using Transcipher.Algorithms;

namespace Transcipher.Services
{
    public class TranscipherService : ITranscipherService
    {
        private readonly IAlgorithmFactory _algorithmFactory;

        public TranscipherService(IAlgorithmFactory algorithmFactory)
        {
            _algorithmFactory = algorithmFactory;
        }

        public string Encrypt(string input, string algorithmName)
        {
            var algorithm = _algorithmFactory.CreateAlgorithm(algorithmName);
            var encryptedString = algorithm.Encrypt(input);
            return encryptedString;
        }

        public string Decrypt(string input, string algorithmName)
        {
            var algorithm = _algorithmFactory.CreateAlgorithm(algorithmName);
            var encryptedString = algorithm.Decrypt(input);
            return encryptedString;
        }
        
        public IEnumerable<string> GetEncryptionAlgorithms()
        {
            var algorithms = _algorithmFactory.Get();
            return algorithms.Select(x => x.Key);
        }
    }
}