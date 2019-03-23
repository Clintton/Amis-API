using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;

namespace AmisWebService
{
    public class Util
    {
        public static string CriptografarSenha(string senha)
        {
            try
            {
                UnicodeEncoding encode = new UnicodeEncoding();
                byte[] hashBytes, messageBytes = encode.GetBytes(senha);

                SHA512Managed sHA512Managed = new SHA512Managed();
                var stringHash = "";
                hashBytes = sHA512Managed.ComputeHash(messageBytes);

                foreach (byte b in hashBytes)
                {
                    stringHash += String.Format("{0:x2}", b);
                }

                return stringHash;
            }
            catch (Exception)
            { 

                throw;
            }

        }
    }
}