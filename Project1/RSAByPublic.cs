using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace Project1
{
    class RSAByPublic
    {
        public static RSAKEY CreateRSAKey()
        {
            //创建RSA对象
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //生成RSA[公钥私钥]
            string privateKey = rsa.ToXmlString(true);
            string publicKey = rsa.ToXmlString(false);
            RSAKEY item = new RSAKEY(publicKey, privateKey);
            return item;
        }

        /// 使用RSA实现加密
     
        public static string RSAEncrypt(string data,string publicKey)
        {
            //创建RSA对象并载入[公钥]
            RSACryptoServiceProvider rsaPublic = new RSACryptoServiceProvider();
            rsaPublic.FromXmlString(publicKey);
            //对数据进行加密
            byte[] publicValue = rsaPublic.Encrypt(Encoding.UTF8.GetBytes(data), false);
            string publicStr = Convert.ToBase64String(publicValue);//使用Base64将byte转换为string
            return publicStr;
        }

        public static string RSADecrypt(string data, string privateKey)
        {
            //创建RSA对象并载入[私钥]
            RSACryptoServiceProvider rsaPrivate = new RSACryptoServiceProvider();
            rsaPrivate.FromXmlString(privateKey);
            //对数据进行解密
            byte[] privateValue = rsaPrivate.Decrypt(Convert.FromBase64String(data), false);//使用Base64将string转换为byte
            string privateStr = Encoding.UTF8.GetString(privateValue);
            return privateStr;
        }

        public static string[] Encrypt(string s, string key)
        {
            int len = s.Length;
            len = ((len + 37) / 38);
            string[] ans = new string[len];
            int cnt = 0;
            for (int i = 0; i < s.Length; i += 38)
            {
                string tt = "";
                for (int j = 0; j < 38 && j + i < s.Length; j++)
                {
                    tt += s[i + j];
                }
                ans[cnt++] = RSAEncrypt(tt, key);
            }
            return ans;
        }
        public static string Decrypt(string[] s, string key)
        {
            string ans = "";
            for (int i = 0; i < s.Length; i++)
            {
                ans += RSADecrypt(s[i], key);
            }
            return ans;
        }
        //static void Main(string[] args)
        //{
        //    int cnt = 25;
        //    while (cnt++ < 40)
        //    {

        //        RSAKEY testkey = CreateRSAKey();
        //        Console.WriteLine(testkey.PublicKey);
        //        string s = "";
        //        for (int i = 0; i < cnt; i++) s = s + "呵";
        //        byte[] byteData = System.Text.Encoding.UTF8.GetBytes(s);
        //        Console.WriteLine(byteData.Length);

        //        string[] es = Encrypt(s, testkey.PublicKey);
        //        //for (int i = 0; i < es.Length; i++)
        //        //    Console.WriteLine(es[i] + "--------------");
        //        string ds = Decrypt(es, testkey.PrivateKey);
        //        Console.WriteLine(ds + "======================");

        //    }
        //    Console.ReadKey(true);
        //}
    }
}
