using System;
using Transcipher.Algorithms;
using Transcipher.Algorithms.Implementations;
using Xunit;

namespace Transcipher.Tests.Algorithms
{
    public class RsaEncryptionTests
    {
        private readonly IEncryptionAlgorithm _rsaAlgorithm = new RsaEncryption();

        [Fact]
        public void Encrypt_EmptyInput_ShouldReturnEmptyString()
        {
            var text = String.Empty;

            var encryptedData = _rsaAlgorithm.Encrypt(text);

            Assert.Equal(String.Empty, encryptedData);
        }

        [Fact]
        public void Decrypt_EmptyInput_ShouldReturnEmptyString()
        {
            var text = String.Empty;

            var decryptedData = _rsaAlgorithm.Decrypt(text);

            Assert.Equal(String.Empty, decryptedData);
        }

        [Fact]
        public void Encryption_Decryption_Correctness()
        {
            var text = "TEST message For encryption 1234";

            var encryptedData = _rsaAlgorithm.Encrypt(text);
            var decryptedData = _rsaAlgorithm.Decrypt(encryptedData);

            Assert.Equal(text, decryptedData);
        }
    }
}