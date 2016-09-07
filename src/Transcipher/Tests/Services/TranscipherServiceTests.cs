using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Transcipher.Algorithms;
using Transcipher.Controllers;
using Transcipher.Services;
using Xunit;

namespace Transcipher.Tests.Services
{
    public class TranscipherServiceTests
    {
        private Mock<IAlgorithmFactory> _encryptionFactoryMock = new Mock<IAlgorithmFactory>();
        private Mock<IEncryptionAlgorithm> _encryptionAlgorithmMock = new Mock<IEncryptionAlgorithm>();
        private TranscipherService _encryptionService;
        private string _fakeAlgorithmName = "fakealgo";
        private string _existingAlgorithmName = "algo2";
        private string _message = "test message";
        private string _encrypted = "encrypted";
        private string _decrypted = "decrypted";
        
        public TranscipherServiceTests()
        {
            _encryptionFactoryMock.Setup(x => x.Get()).Returns(new Dictionary<string, Type>()
            {
                { "algo1", It.IsAny<Type>()},
                { _existingAlgorithmName, It.IsAny<Type>()}
            });

            _encryptionAlgorithmMock.Setup(x => x.Encrypt(It.IsAny<string>())).Returns(_encrypted);
            _encryptionAlgorithmMock.Setup(x => x.Decrypt(It.IsAny<string>())).Returns(_decrypted);
            _encryptionFactoryMock.Setup(x => x.CreateAlgorithm(_fakeAlgorithmName)).Throws(new ArgumentException(It.IsAny<string>()));
            _encryptionFactoryMock.Setup(x => x.CreateAlgorithm(_existingAlgorithmName)).Returns(_encryptionAlgorithmMock.Object);
            _encryptionService = new TranscipherService(_encryptionFactoryMock.Object);
        }

        [Fact]
        public void GetEncryptionAlgorithms_ShouldReturnAlgorithmNames()
        {
            var algoNames = new[] { "algo1", _existingAlgorithmName };

            var algorithms = _encryptionService.GetEncryptionAlgorithms().ToList();

            Assert.True(!algorithms.Except(algoNames).Any() && algorithms.Count == algoNames.Length);
        }

        [Fact]
        public void Encrypt_NonExistingAlgoritm_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => _encryptionService.Encrypt(_message, _fakeAlgorithmName));
        }

        [Fact]
        public void Encrypt_ExistingAlgoritm_ShouldCallEncryptionMethod()
        {
            var encrypted = _encryptionService.Encrypt(_message, _existingAlgorithmName);

            Assert.Equal(_encrypted, encrypted);
        }

        [Fact]
        public void Decrypt_NonExistingAlgoritm_ShouldCallThrowException()
        {
            Assert.Throws<ArgumentException>(() => _encryptionService.Decrypt(_message, _fakeAlgorithmName));
        }

        [Fact]
        public void Decrypt_ExistingAlgoritm_ShouldCallDecryptionMethod()
        {
            var decrypted = _encryptionService.Decrypt(_message, _existingAlgorithmName);

            Assert.Equal(_decrypted, decrypted);
        }
    }
}
