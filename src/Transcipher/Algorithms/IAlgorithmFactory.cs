using System;
using System.Collections.Generic;

namespace Transcipher.Algorithms
{
    public interface IAlgorithmFactory
    {
        IDictionary<string, Type> Get();
        IEncryptionAlgorithm CreateAlgorithm(string name);
    }
}