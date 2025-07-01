using System.Security.Cryptography;
using System.Text;

namespace BackendProyectoFinal.Utils
{
    public class Encrypt
    {
        public byte[] Key {  get;}
        public byte[] IV { get; }

        public Encrypt()
        {
            Key = Encoding.UTF8.GetBytes("asdasdasdsqadasdada");
            IV = Encoding.UTF8.GetBytes("qweqweqweqwewqe");
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
