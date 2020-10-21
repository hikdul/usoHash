using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace funcionHash.Helpers
{
    public static class AES
    {


        /*
        * Texto a cifrar: el texto que queramos cifrar o encriptar con AES.
        * Contraseña o palabra de paso: texto que se usará para generar el algoritmo de cifrado.
        * valorRGBSalt: una cadena de texto cualquiera.
        * Algoritmo de cifrado: puede ser "MD5" ó "SHA1".
        * Iteraciones: número de iteraciones.
        * Vector inicial: un texto o número de 16 bytes (16 caracteres)
        * Tamaño clave: puede ser 128, 192 o 256.
        * */

        public static string cifrarTextoAES(string textoCifrar, string palabraPaso,
          string valorRGBSalt, string algoritmoEncriptacionHASH,
          int iteraciones, string vectorInicial, int tamanoClave)
        {
            try
            {
                byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(vectorInicial);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(valorRGBSalt);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(textoCifrar);

                PasswordDeriveBytes password =
                    new PasswordDeriveBytes(palabraPaso, saltValueBytes,
                        algoritmoEncriptacionHASH, iteraciones);

                byte[] keyBytes = password.GetBytes(tamanoClave / 8);

                RijndaelManaged symmetricKey = new RijndaelManaged();

                symmetricKey.Mode = CipherMode.CBC;

                ICryptoTransform encryptor =
                    symmetricKey.CreateEncryptor(keyBytes, InitialVectorBytes);

                MemoryStream memoryStream = new MemoryStream();

                CryptoStream cryptoStream =
                    new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

                cryptoStream.FlushFinalBlock();

                byte[] cipherTextBytes = memoryStream.ToArray();

                memoryStream.Close();
                cryptoStream.Close();

                string textoCifradoFinal = Convert.ToBase64String(cipherTextBytes);

                return textoCifradoFinal;
            }
            catch
            {
                return null;
            }
        }

        /*
         
        * Texto a descifrar: el texto cifrado previamente a descrifrar por la función.
        * Contraseña o palabra de paso: texto que se usará para generar el algoritmo de descifrado, debe coincidir con el que se usó para el cifrado.
        * valorRGBSalt: una cadena de texto cualquiera, debe coincidir con el que se usó para el cifrado.
        * Algoritmo de cifrado: puede ser "MD5" ó "SHA1", debe coincidir con el que se usó para el cifrado.
        * Iteraciones: número de iteraciones, debe coincidir con el que se usó para el cifrado.
        * Vector inicial: un texto o número de 16 bytes (16 caracteres), debe coincidir con el que se usó para el cifrado.
        * Tamaño clave: puede ser 128, 192 o 256, debe coincidir con el que se usó para el cifrado.
         
         */
        public static string descifrarTextoAES(string textoCifrado, string palabraPaso,
            string valorRGBSalt, string algoritmoEncriptacionHASH,
            int iteraciones, string vectorInicial, int tamanoClave)
        {
            try
            {
                byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(vectorInicial);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(valorRGBSalt);

                byte[] cipherTextBytes = Convert.FromBase64String(textoCifrado);

                PasswordDeriveBytes password =
                    new PasswordDeriveBytes(palabraPaso, saltValueBytes,
                        algoritmoEncriptacionHASH, iteraciones);

                byte[] keyBytes = password.GetBytes(tamanoClave / 8);

                RijndaelManaged symmetricKey = new RijndaelManaged();

                symmetricKey.Mode = CipherMode.CBC;

                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, InitialVectorBytes);

                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

                byte[] plainTextBytes = new byte[cipherTextBytes.Length];

                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                memoryStream.Close();
                cryptoStream.Close();

                string textoDescifradoFinal = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

                return textoDescifradoFinal;
            }
            catch
            {
                return null;
            }
        }
    }


}
