using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using PgpCore;

namespace PGP.CoderPGP
{
    public class GoPGP
    {
        private PgpCore.PGP PGP = new PgpCore.PGP();

        private string WayPublicKey;
        private string WayPrivateKey;

        private string WayBufferOf;
        private string WayBufferIn;

        private string WayFileEmail;
        private string WayFilePass;

        private string WayDir;

        private string Email;
        private string Pass;

        public GoPGP(string Email, string Pass, string PublicKey, string PrivateKey)
        {
            this.Email = Email;
            this.Pass = Pass;
            WayDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            WayPublicKey = Path.Combine(WayDir, "public.gopgp");
            WayPrivateKey = Path.Combine(WayDir, "private.gopgp");

            WayBufferOf = Path.Combine(WayDir, "BufferOf");
            WayBufferIn = Path.Combine(WayDir, "BufferIn");

            WayFileEmail = Path.Combine(WayDir, "Email");
            WayFilePass = Path.Combine(WayDir, "Pass");


            File.WriteAllText(WayPublicKey, PublicKey);
            File.WriteAllText(WayPrivateKey, PrivateKey);

            File.WriteAllText(WayBufferOf, "");
            File.WriteAllText(WayBufferIn, "");

            File.WriteAllText(WayFileEmail, Email);
            File.WriteAllText(WayFilePass, Pass);
        }


        public GoPGP(string Email, string Pass)
        {
            this.Email = Email;
            this.Pass = Pass;

            WayDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            WayPublicKey = Path.Combine(WayDir, "public.gopgp");
            WayPrivateKey = Path.Combine(WayDir, "private.gopgp");

            WayBufferOf = Path.Combine(WayDir, "BufferOf");
            WayBufferIn = Path.Combine(WayDir, "BufferIn");
            
            WayFileEmail = Path.Combine(WayDir, "Email");
            WayFilePass = Path.Combine(WayDir, "Pass");

            File.WriteAllText(WayBufferOf, "");
            File.WriteAllText(WayBufferIn, "");

            File.WriteAllText(WayFileEmail, Email);
            File.WriteAllText(WayFilePass, Pass);
        }

        public GoPGP()
        {
            WayDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            WayPublicKey = Path.Combine(WayDir, "public.gopgp");
            WayPrivateKey = Path.Combine(WayDir, "private.gopgp");

            WayBufferOf = Path.Combine(WayDir, "BufferOf");
            WayBufferIn = Path.Combine(WayDir, "BufferIn");

            WayFileEmail = Path.Combine(WayDir, "Email");
            WayFilePass = Path.Combine(WayDir, "Pass");

            Email = File.ReadAllText(WayFileEmail);
            Pass = File.ReadAllText(WayFilePass);
        }

        public void CreatKeys()
        {
            File.WriteAllText(WayPublicKey, "");
            File.WriteAllText(WayPrivateKey, "");

            PGP.GenerateKey(WayPublicKey, WayPrivateKey, Email, Pass);
        }

        public bool ValidationKey()
        {
            string TestASC = WayDir + "\\TEST\\TEST.asc";

            DirectoryInfo dirInfo = new DirectoryInfo(WayDir + "\\TEST\\");
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            if (!File.Exists(TestASC))
            {
                File.WriteAllText(TestASC, "TEST: 1234567890\n test: qwertyuiop \n тест: йцукенгшщзх");
            }

            try
            {
                string WayPGP = TestASC + ".pgp";
                PGP.EncryptFile(TestASC, WayPGP, WayPublicKey, true, false);
                DecodeFile(WayPGP);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public string EncodeFile(string WayContent)//Косяк здесь
        {
            PGP.EncryptFile(WayContent, WayContent + ".pgp", WayPublicKey, true, false);
            File.Delete(WayContent);
            return WayContent + ".pgp";// Возвращаем путь с .pgp
        }

        public string DecodeFile(string EncryptedWayContent)
        {
            string WayContent = EncryptedWayContent;//Дублируем путь
            int Idot = WayContent.LastIndexOf('.');//находим вхождение расширения
            WayContent = WayContent.Substring(0, Idot);//Отризаем расширение

            PGP.DecryptFile(EncryptedWayContent, WayContent, WayPrivateKey, Pass);//Расшифровываем 
            File.Delete(EncryptedWayContent);

            return WayContent;
        }

        public string EncodeText(string Text)
        {
            File.WriteAllText(WayBufferOf, Text);
            PGP.EncryptFile(WayBufferOf, WayBufferIn, WayPublicKey, true, true);
            return File.ReadAllText(WayBufferIn);
        }

        public string DecryptText(string Text)
        {
            File.WriteAllText(WayBufferOf, Text);
            PGP.DecryptFile(WayBufferOf, WayBufferIn, WayPrivateKey, Pass);
            return File.ReadAllText(WayBufferIn);
        }
    }
}
