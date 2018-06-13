using System;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;
using System.Collections.Specialized;


namespace TechmerVision.Providers
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   An encryption provider. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class EncryptionProvider
    {
        private string _keyValue = null;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public EncryptionProvider() { 
            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            _keyValue = appSettings["keyValue"].ToString();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Encrypts. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="input">    String to encrypt. </param>
        ///
        /// <returns>   Encrypted string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Encrypt(string input) {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8Encoding.UTF8.GetBytes(_keyValue));
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = TDESKey;
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Decrypts. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="input">    Encrypted String </param>
        ///
        /// <returns>   Decrypted string </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Decrypt(string input) {
            byte[] inputArray = Convert.FromBase64String(input);
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8Encoding.UTF8.GetBytes(_keyValue));
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = TDESKey;
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}