﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Parosan.Model;
namespace Parosan.Service
{
    class CryptoService
    {
        private string key = UserModel.key;
        private string iv = UserModel.iv;


        public string textEncrytion(string clearText)
        {

            byte[] encryptedTextByte = aesEncrypt(Encoding.UTF8.GetBytes(clearText));
            return Convert.ToBase64String(encryptedTextByte) ;
        }
        public string textDecrytion(string encryptedText)
        {

            byte[] clearTextByte = aesDecrypt(Convert.FromBase64String(encryptedText));
            return Encoding.UTF8.GetString(clearTextByte);
        }


        private byte[] aesEncrypt(byte[] clearText)
        {
            AesCryptoServiceProvider acsp = new AesCryptoServiceProvider();
          

            try
            {
                acsp.Key = Encoding.ASCII.GetBytes(key);// set key

            }
            catch
            {
                return Encoding.ASCII.GetBytes("Invalid Key Size !");
            }

            try
            {
                acsp.IV = Encoding.ASCII.GetBytes(iv); // set iv
            }
            catch
            {
                return Encoding.ASCII.GetBytes("Invalid IV Size !");
            }

            acsp.Mode = CipherMode.CBC; // set cipher mode cbc

            //create encrytor
            ICryptoTransform cryptoTransform = acsp.CreateEncryptor();

            //encrypt bytes
            byte[] cipherTextBytes = cryptoTransform.TransformFinalBlock((clearText), 0, clearText.Length);

            return cipherTextBytes;

        }

        private byte[] aesDecrypt(byte[] cipherText)
        {
            AesCryptoServiceProvider acsp = new AesCryptoServiceProvider();
            try
            {
                acsp.Key = Encoding.ASCII.GetBytes(key);// set key

            }
            catch
            {
                return Encoding.ASCII.GetBytes("Invalid Key Size !");
            }

            try
            {
                acsp.IV = Encoding.ASCII.GetBytes(iv); // set iv
            }
            catch
            {
                return Encoding.ASCII.GetBytes("Invalid IV Size !");
            }

            acsp.Mode = CipherMode.CBC; // set cipher mode cbc

            //create decrytor
            ICryptoTransform cryptoTransform = acsp.CreateDecryptor();

            //decrypt bytes
            byte[] clearTextBytes = cryptoTransform.TransformFinalBlock(cipherText, 0, cipherText.Length);

            return clearTextBytes;


        }

    }
}