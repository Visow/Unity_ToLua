using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Cryptography;


namespace LibServer.Utility
{
    public static class Security
    {
        private static string TOKEN_PRIVATE_KEY = "SJHNV*SA&^DYAGBG^@Q";
        public static string MD5(string value)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] palindata = Encoding.Default.GetBytes(value + TOKEN_PRIVATE_KEY);//将要加密的字符串转换为字节数组
            byte[] encryptdata = md5.ComputeHash(palindata);//将字符串加密后也转换为字符数组
            return Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为加密字符串
        }
    }
}
