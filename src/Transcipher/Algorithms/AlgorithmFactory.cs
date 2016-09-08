using System;
using System.Collections.Generic;
using Transcipher.Algorithms.Implementations;

namespace Transcipher.Algorithms
{
    public class AlgorithmFactory : IAlgorithmFactory
    {
        private static readonly IDictionary<string, Type> _types = new Dictionary<string, Type>();

        static AlgorithmFactory()
        {
            _types.Add("RSA", typeof(RsaEncryption));
            _types.Add("Aes", typeof(AesEncryption));
            _types.Add("Morse", typeof(MorseEncryption));
        }

        public IDictionary<string, Type> Get()
        {
            return _types;
        }

        public IEncryptionAlgorithm CreateAlgorithm(string name)
        {
            Type algorithmType;
            if (_types.TryGetValue(name, out algorithmType))
            {
                return (IEncryptionAlgorithm)Activator.CreateInstance(algorithmType);
            }

            throw new ArgumentException(String.Format("No such algorithm: {0}", name));
        }
    }
}