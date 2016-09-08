using System;
using System.Security.Cryptography;
using System.Text;

namespace Transcipher.Algorithms.Implementations
{
    public class RsaEncryption : IEncryptionAlgorithm
    {
        private bool _optimalAsymmetricEncryptionPadding = false;
        private string _privateKey = "<RSAKeyValue><Modulus>zECQfsZ4p+QemG0OqsqWUAa/lo/LX0DBcEoOPA+TK1V48oGfPtbM0uadeekvPo/dn7ILR8JLuWnt9UrBsyeCz8odmUdYD58jZ2pBttC2E/hQVhBuI6sdD9tJQjSLN4F/5hx37s0Xpq7yVjoq3wqokBvRFha6CLT1CmLLMJ9nd0E=</Modulus><Exponent>AQAB</Exponent><P>/Tqf/5+fkxKMMFFfvqZIa1g4ybJxw0nYTtgRmB4clDc68DfeyxMFdZzsgonfLaZ0QpZFtV1yJ4OEZI2ojKYbGw==</P><Q>zny9ZmfZORpOANHddP27lni8J/x0+XAssMWNNfyHyZ+MKRmcCR1r5NaujR83jYFv7ZTf6MQj65rV/LqZ0U9g0w==</Q><DP>z2koSejFfGIxvxW3tWFfacT95n6pXYpriNDDQHRRdjjypnUsn+q5iwb4VUd2LF7tVOjeudmLOcAoPXAmAOXAxw==</DP><DQ>o8RXS0MOC7YLK6dHJySehQcY4/XcqIEJUOI9zxWMdKLvvSEsmiYVjpeeNRsPKlIAfcPXmnsFqjVRhnJQ0KtONQ==</DQ><InverseQ>q5MkgNW2j5C/PQL2t4BsoHxqdZRmYA38gNjANfd+9mG0iHJM9PVmZ7Eg8vEGcBYN+mBfhNziFEnYIkiP203sVA==</InverseQ><D>jMuUz08QrOrbDEdg7OppYxDD0I2eqzKAJV9nII+76wsoLEKd+zsmRdsign7zSjTxmLuqevS8LjuPBJ9blpuN12GeJ3jRjKYvvnm5QCu937MTa2c+fEn3i+yRDDn5L/RNCEurnhXdiy2LjolmnSXwlJID+ReoPpzA2OKx1nrWj2E=</D></RSAKeyValue>";
        private string _publicKey = "<RSAKeyValue><Modulus>zECQfsZ4p+QemG0OqsqWUAa/lo/LX0DBcEoOPA+TK1V48oGfPtbM0uadeekvPo/dn7ILR8JLuWnt9UrBsyeCz8odmUdYD58jZ2pBttC2E/hQVhBuI6sdD9tJQjSLN4F/5hx37s0Xpq7yVjoq3wqokBvRFha6CLT1CmLLMJ9nd0E=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        public string Encrypt(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return String.Empty;
            }

            var bytes = ASCIIEncoding.ASCII.GetBytes(input);
            var encryptedBytes = new byte[] { };

            using (var provider = new RSACryptoServiceProvider())
            {
                provider.FromXmlString(_publicKey);
                encryptedBytes = provider.Encrypt(bytes, _optimalAsymmetricEncryptionPadding);
            }
            return Convert.ToBase64String(encryptedBytes);
        }

        public string Decrypt(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return String.Empty;
            }

            var bytes = Convert.FromBase64String(input);
            var decryptedBytes = new byte[] { };

            using (var provider = new RSACryptoServiceProvider())
            {
                provider.FromXmlString(_privateKey);
                decryptedBytes = provider.Decrypt(bytes, _optimalAsymmetricEncryptionPadding);
            }

            return ASCIIEncoding.ASCII.GetString(decryptedBytes);
        }
    }
}