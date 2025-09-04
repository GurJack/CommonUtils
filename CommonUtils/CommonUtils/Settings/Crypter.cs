//using System;
//using System.IO;
//using System.Security;
//using System.Security.Cryptography;
//using System.Text;

//namespace CommonUtils.Settings
//{
//    public static class Crypter
//    {
//        private static string _key =
//            "<RSAKeyValue><Modulus>qhYoig4XmvM1p0KbQuQZyDP5SzzZxriCjy4DYKM5/v6W2VE+pzTEEQiOGe+bTSiGY6uslfJZJMMyMV0lQbSjSPqV12nfJdlwhxhXgzsbL5OD13Jl+RaFOXTEPUqwlMCa7wnrmx5dj9h8ecLnUiQVAeq1oPKymk41yuT+MSt0k7U=</Modulus><Exponent>AQAB</Exponent><P>0MSztVWgjZTw+GvXgWZifrTGSDhTLBZQI2gfeIn32IwhkYw87eyzZcO4yCELNJiw2yWVkIwvzLEFbmp35hrNIQ==</P><Q>0JEcV5OGpBxfUI5kERCYpPDja93X/2w9mSBSZlsB1HfskS9qgzeoATRVoK2Y+WMH47Hz58ru4RtowGqBU0nAFQ==</Q><DP>Yov3qKl62FpuRVXirJp/8/+xeXXdDPqbaZtE/8lgzT+YuPPR7x3EsUzqdM3kVDefhFMBfvItvhnxzmVDo8MTAQ==</DP><DQ>ODvHzn0CQmE7+bZKmKdG4MHqL30i7cU7Xnvue5ZyCd1DtWl5aGOrMpfvtmDX6/WdfPDP+GEowxzmw2pz8AbkeQ==</DQ><InverseQ>QzGbtsNgHsK/BvgP0FN3tmiF9R1k/aGqIwLd043Mb5dZ23xcZ3U+GLrXLGweIEpIoe+W2/+o9zFto9XhisDJBA==</InverseQ><D>DPf1jVk3uYvTynYMqpQD1z0HRBJWtI06/znN7h9j+6pjzBwiv9MJJBdX5mhbmg19+bmMRj6dR21OtW7ZuWHRlU4b0LPo2crmjIJRJXaZVpo1AZ3iLdw+buN0Q7VrZ96dgXZ/NIA/vVzucNUEzrbWBLtAit+8e/emMPh+uhJN4IE=</D></RSAKeyValue>";

//        private static byte[] _aesKey;
//        private static byte[] _aesIV;

//        internal static string Encrypt(string value)
//        {
            
//            var rsa = new RSACryptoServiceProvider();
//            rsa.FromXmlString(_key);
//            return Convert.ToBase64String(rsa.Encrypt(GetByte(value), false));
//        }



//        private static void GetKeys(string pass, out byte[] key, out byte[] IV)
//        {
//            var keyInfo = ASCIIEncoding.ASCII.GetBytes(pass);
//            key=new byte[32];
//            IV=new byte[16];
//            byte currRound = 1;
//            int currPos = 0;
//            for (int i = 0; i < key.Length; i++)
//            {
//                if(keyInfo[currPos] - currRound > 0)
//                    key[i]= (byte) (keyInfo[currPos] - currRound);
//                else
//                    key[i] = (byte)(255+ (keyInfo[currPos] - currRound));
//                currPos++;
//                if (currPos >= keyInfo.Length)
//                {
//                    currPos = 0;
//                    currRound++;
//                }
//            }
//            for (int i = 0; i < IV.Length; i++)
//            {
//                if (keyInfo[currPos] - currRound > 0)
//                    IV[i] = (byte)(keyInfo[currPos] - currRound);
//                else
//                    IV[i] = (byte)(255 + (keyInfo[currPos] - currRound));
//                currPos++;
//                if (currPos >= keyInfo.Length)
//                {
//                    currPos = 0;
//                    currRound++;
//                }
//            }

//        }

//        public static string Encrypt1(string value)
//        {
//            if (value == null || value.Length <= 0)
//                throw new ArgumentNullException("value");
//            using (var aes = new AesCryptoServiceProvider())
//            {
//                if (_aesKey == null || _aesIV == null)
//                    throw new ApplicationException("Пароль не задан.");
//                aes.Key = _aesKey;
//                aes.IV = _aesIV;


//                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

//                // Create the streams used for encryption.
//                using (MemoryStream msEncrypt = new MemoryStream())
//                {
//                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
//                    {
//                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
//                        {

//                            //Write all data to the stream.
//                            swEncrypt.Write(value);
//                        }
                        
//                        return Convert.ToBase64String(msEncrypt.ToArray());
//                    }
//                }

                
//            }
            
            
//        }

//        public static void CalcKey(string pass)
//        {
//            GetKeys(pass,out _aesKey, out _aesIV);    
//        }

//        public static void SetPassword(string pass)
//        {
//            CalcKey(pass);
//        }

//        public static string Decrypt1(string value)
//        {
//            if (value == null || value.Length <= 0)
//                throw new ArgumentNullException("value");
            
//            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
//            {
//                if(_aesKey == null || _aesIV == null)
//                    throw new ApplicationException("Пароль не задан.");
//                aes.Key = _aesKey;
//                aes.IV = _aesIV;

//                // Create a decrytor to perform the stream transform.
//                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

//                // Create the streams used for decryption.
//                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(value)))
//                {
//                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
//                    {
//                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
//                        {

//                            // Read the decrypted bytes from the decrypting stream
//                            // and place them in a string.
//                            return srDecrypt.ReadToEnd();
//                        }
//                    }
//                }

//            }

            
            
//        }


//        internal static string Decrypt(string value)
//        {
//            var rsa = new RSACryptoServiceProvider();
//            rsa.FromXmlString(_key);
//            var result = rsa.Decrypt(Convert.FromBase64String(value), false);
//            return GetString(result);
//        }

//        private static byte[] GetByte(string value)
//        {
//            return Encoding.UTF8.GetBytes(value);
//        }

//        private static string GetString(byte[] value)
//        {
//            return Encoding.UTF8.GetString(value);
//        }
//    }
//}
