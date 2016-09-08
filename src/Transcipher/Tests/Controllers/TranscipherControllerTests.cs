using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Transcipher.Controllers;
using Transcipher.Models;
using Transcipher.Services;
using Xunit;

namespace Transcipher.Tests.Controllers
{
    public class TranscipherControllerTests
    {
        private readonly Mock<ITranscipherService> _transcipherServiceMock = new Mock<ITranscipherService>();
        private readonly TranscipherController _transcipherController;

        public TranscipherControllerTests()
        {
            _transcipherController = new TranscipherController(_transcipherServiceMock.Object);
        }

        [Fact]
        public void GetAlgorithms_ReturnOk()
        {
            _transcipherServiceMock.Setup(x => x.GetEncryptionAlgorithms()).Returns(It.IsAny<IEnumerable<string>>());

            var actionResult = _transcipherController.GetAlgorithms();

            Assert.NotNull(actionResult);
            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetAlgorithms_Exception_ReturnInternalServerError()
        {
            _transcipherServiceMock.Setup(x => x.GetEncryptionAlgorithms()).Throws(new Exception());

            IActionResult actionResult = _transcipherController.GetAlgorithms();

            Assert.NotNull(actionResult);
            var result = Assert.IsType<ObjectResult>(actionResult);
            Assert.IsType<ObjectResult>(actionResult);
            Assert.Equal(500, result.StatusCode);
            Assert.IsType<string>(result.Value);
        }

        [Fact]
        public void Encrypt_ReturnOk()
        {
            _transcipherServiceMock.Setup(x => x.Encrypt(It.IsAny<string>(), It.IsAny<string>())).Returns(It.IsAny<string>());

            var actionResult = _transcipherController.Encrypt(new ProcessingData());

            Assert.NotNull(actionResult);
            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void Encrypt_Exception_ReturnInternalServerError()
        {
            _transcipherServiceMock.Setup(x => x.Encrypt(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());

            IActionResult actionResult = _transcipherController.Encrypt(It.IsAny<ProcessingData>());

            Assert.NotNull(actionResult);
            var result = Assert.IsType<ObjectResult>(actionResult);
            Assert.IsType<ObjectResult>(actionResult);
            Assert.Equal(500, result.StatusCode);
            Assert.IsType<string>(result.Value);
        }

        [Fact]
        public void Decrypt_ReturnOk()
        {
            _transcipherServiceMock.Setup(x => x.Decrypt(It.IsAny<string>(), It.IsAny<string>())).Returns(It.IsAny<string>());

            var actionResult = _transcipherController.Decrypt(new ProcessingData());

            Assert.NotNull(actionResult);
            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void Decryptt_Exception_ReturnInternalServerError()
        {
            _transcipherServiceMock.Setup(x => x.Decrypt(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());

            IActionResult actionResult = _transcipherController.Decrypt(It.IsAny<ProcessingData>());

            Assert.NotNull(actionResult);
            var result = Assert.IsType<ObjectResult>(actionResult);
            Assert.IsType<ObjectResult>(actionResult);
            Assert.Equal(500, result.StatusCode);
            Assert.IsType<string>(result.Value);
        }
    }
}
