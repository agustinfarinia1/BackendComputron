using BackendProyectoFinal.Configurations;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace BackendProyectoFinal.Services
{
    public class EncryptService
    {
        private byte[] Key {  get;}
        private byte[] IV { get; }

        public EncryptService(IOptions<EncryptConfiguration> config)
        {
            var fraseFinal = config.Value.PrivateKey1 + config.Value.PrivateKey2;

            // Salt configurado desde .Env
            var salt = Encoding.UTF8.GetBytes(config.Value.Salt);

            // Usamos PBKDF2 para derivar 32 bytes (AES-256) + 16 bytes (IV)
            var keyDeriver = new Rfc2898DeriveBytes(fraseFinal, salt, 100_000, HashAlgorithmName.SHA256);

            Key = keyDeriver.GetBytes(32); // AES-256
            IV = keyDeriver.GetBytes(16);  // IV (AES usa bloque de 16 bytes)
        }

        public string EncryptData(string plainText) 
        {
            using Aes aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;

            ICryptoTransform encryptor = aes.CreateEncryptor();

            using MemoryStream msEncrypt = new MemoryStream();
            using CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt)) 
            { 
                swEncrypt.Write(plainText);
            }

            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        public string Decrypt(string cypheredText)
        {
            using Aes aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;

            ICryptoTransform decryptor = aes.CreateDecryptor();

            byte[] cypheredBytes = Convert.FromBase64String(cypheredText);
            using MemoryStream msEncrypt = new MemoryStream(cypheredBytes);
            using CryptoStream csEncrypt = new CryptoStream(msEncrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srEncrypt = new StreamReader(csEncrypt);

            return srEncrypt.ReadToEnd();
        }
    }
}
