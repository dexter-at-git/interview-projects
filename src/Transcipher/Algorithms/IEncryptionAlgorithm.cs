namespace Transcipher.Algorithms
{
    public interface IEncryptionAlgorithm
    {
        string Encrypt(string input);
        string Decrypt(string input);
    }
}