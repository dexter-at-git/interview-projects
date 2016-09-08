using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Transcipher.Models;
using Transcipher.Services;

namespace Transcipher.Controllers
{
    [Route("api/[controller]")]
    public class TranscipherController : Controller
    {
        private readonly ITranscipherService _transcipherService;

        public TranscipherController(ITranscipherService transcipherService)
        {
            _transcipherService = transcipherService;
        }

        [AllowAnonymous]
        [Route("algorithms")]
        [HttpGet]
        public IActionResult GetAlgorithms()
        {
            try
            {
                var algorithmList = _transcipherService.GetEncryptionAlgorithms();

                return this.Ok(algorithmList);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [Route("encrypt")]
        [HttpPost]
        public IActionResult Encrypt([FromBody]ProcessingData encryptionData)
        {
            try
            {
                var encryptedText = _transcipherService.Encrypt(encryptionData.Text, encryptionData.Algorithm);

                return this.Ok(encryptedText);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [AllowAnonymous]
        [Route("decrypt")]
        [HttpPost]
        public IActionResult Decrypt([FromBody]ProcessingData encryptionData)
        {
            try
            {
                var decryptedText = _transcipherService.Decrypt(encryptionData.Text, encryptionData.Algorithm);

                return this.Ok(decryptedText);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
