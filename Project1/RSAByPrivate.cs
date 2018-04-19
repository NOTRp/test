using System;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto.Encodings;
using Project1;
namespace test
{
    public class RSAByPrivate
    {
        public RSAByPrivate()
        {
        }

        public static RSAKEY MyKey;
        public static RSAKEY GetKey()
        {
            //RSA密钥对的构造器  
            RsaKeyPairGenerator keyGenerator = new RsaKeyPairGenerator();

            //RSA密钥构造器的参数  
            RsaKeyGenerationParameters param = new RsaKeyGenerationParameters(
                Org.BouncyCastle.Math.BigInteger.ValueOf(3),
                new Org.BouncyCastle.Security.SecureRandom(),
                1024,   //密钥长度  
                25);
            keyGenerator.Init(param);

            //keyGenerator.Init();

            //产生密钥对  
            AsymmetricCipherKeyPair keyPair = keyGenerator.GenerateKeyPair();
            //获取公钥和密钥  
            AsymmetricKeyParameter publicKey = keyPair.Public;
            AsymmetricKeyParameter privateKey = keyPair.Private;
            SubjectPublicKeyInfo subjectPublicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey);
            PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKey);


            Asn1Object asn1ObjectPublic = subjectPublicKeyInfo.ToAsn1Object();

            byte[] publicInfoByte = asn1ObjectPublic.GetEncoded("UTF-8");
            Asn1Object asn1ObjectPrivate = privateKeyInfo.ToAsn1Object();
            byte[] privateInfoByte = asn1ObjectPrivate.GetEncoded("UTF-8");

            RSAKEY item = new RSAKEY(Convert.ToBase64String(publicInfoByte), Convert.ToBase64String(privateInfoByte));

            return item;
        }
        private static AsymmetricKeyParameter GetPublicKeyParameter(string s)
        {
            s = s.Replace("\r", "").Replace("\n", "").Replace(" ", "");
            byte[] publicInfoByte = Convert.FromBase64String(s);
            Asn1Object pubKeyObj = Asn1Object.FromByteArray(publicInfoByte);//这里也可以从流中读取，从本地导入   
            AsymmetricKeyParameter pubKey = PublicKeyFactory.CreateKey(publicInfoByte);
            return pubKey;
        }
        private static AsymmetricKeyParameter GetPrivateKeyParameter(string s)
        {
            s = s.Replace("\r", "").Replace("\n", "").Replace(" ", "");
            byte[] privateInfoByte = Convert.FromBase64String(s);
            // Asn1Object priKeyObj = Asn1Object.FromByteArray(privateInfoByte);//这里也可以从流中读取，从本地导入   
            // PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKey);
            AsymmetricKeyParameter priKey = PrivateKeyFactory.CreateKey(privateInfoByte);
            return priKey;
        }
        public static string EncryptByPrivateKey(string s, string key)
        {
            //非对称加密算法，加解密用  
            IAsymmetricBlockCipher engine = new Pkcs1Encoding(new RsaEngine());
            //加密      
            try
            {
                engine.Init(true, GetPrivateKeyParameter(key));
                byte[] byteData = System.Text.Encoding.UTF8.GetBytes(s);
                //Console.WriteLine("密文（base64编码）:");

               var ResultData = engine.ProcessBlock(byteData, 0, byteData.Length);
                return Convert.ToBase64String(ResultData);
                // Console.WriteLine("密文（base64编码）:" + Convert.ToBase64String(testData) + Environment.NewLine);
            }
            catch (Exception ex)
            {
               
                return ex.Message;

            }
        }
        public static string DecryptByPublicKey(string s, string key)
        {
            s = s.Replace("\r", "").Replace("\n", "").Replace(" ", "");
            //非对称加密算法，加解密用  
            IAsymmetricBlockCipher engine = new Pkcs1Encoding(new RsaEngine());
            //解密             
            try
            {
                engine.Init(false, GetPublicKeyParameter(key));
                byte[] byteData = Convert.FromBase64String(s);
                var ResultData = engine.ProcessBlock(byteData, 0, byteData.Length);
                return System.Text.Encoding.UTF8.GetString(ResultData);

            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }
        public static string[] Encrypt(string s,string key)
        {
            int len = s.Length;
            len = ((len + 37) / 38);
            string[] ans = new string[len];
            int cnt = 0;
            for(int i = 0;i < s.Length; i+=38)
            {
                string tt = "";
                for(int j = 0;j < 38 && j+i < s.Length;j++)
                {
                    tt += s[i + j];
                }
                ans[cnt++] = EncryptByPrivateKey(tt, key);
            }
            return ans;
        }
        public static string Decrypt(string[] s, string key)
        {
            string ans = "";
            for(int i = 0;i < s.Length;i++)
            {
                ans += DecryptByPublicKey(s[i], key);
            }
            return ans;
        }
        //static void Main(string[] args)
        //{
        //    int cnt = 25;
        //    while (cnt++ < 27)
        //    {

        //        RSAKEY testkey = GetKey();
        //        Console.WriteLine(testkey.PublicKey);
        //        string s = "";
        //        for (int i = 0; i < cnt; i++) s = s + "呵呵哒";
        //        byte[] byteData = System.Text.Encoding.UTF8.GetBytes(s);
        //        Console.WriteLine(byteData.Length);
        //        string[] es = Encrypt(s, testkey.PrivateKey);
        //        //for (int i = 0; i < es.Length; i++)
        //        //    Console.WriteLine(es[i] + "--------------");
        //        string ds = Decrypt(es, testkey.PublicKey);
        //        Console.WriteLine(ds + "======================");

        //    }
        //    Console.ReadKey(true);
        //}
    }
}