using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Decryptor
{
    class Program
    {
        static byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };



        static void DecryptAll()
        {
            // this is the final seed value that creates the right password
            int SEED_VALUE = -1;

            // root folder with locked files - make sure this is R/W!

            var root = "F:\\DiskCopy\\";

            var files = Directory.EnumerateFiles(root, "*.locked", SearchOption.AllDirectories);
            var password = CreatePassword(15, SEED_VALUE);
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            var pwBytes = SHA256.Create().ComputeHash(bytes);


            foreach (var file in files)
            {

                var data = File.ReadAllBytes(file);
                var decrypted = AES_Decrypt(data, pwBytes);
                File.WriteAllBytes(file.Replace(".locked", ""), decrypted);
            }


        }
        static void Main(string[] args)
        {
            BruteForce();

            //DecryptAll();
            

        }

        static void BruteForce()
        {
            
            var path = "J:\\Downloads\\Baby Growth and Development Month by Month   What to Expect_files\\icong1.png.locked";

            var bytesToBeDecrypted = File.ReadAllBytes(path);
            byte[] PNG_HEADER = new byte[8] { 137, 80, 78, 71, 13, 10, 26, 10 };


            var startTicks = 28468 * 1000;
            Parallel.ForEach(Enumerable.Range(startTicks - 300000, startTicks + 300000), new ParallelOptions() { MaxDegreeOfParallelism = 4 }, (x, l, i) =>
                {

                    byte[] bytes2 = { 0, 0, 0, 0, 0, 0, 0, 0 };

                    var password = CreatePassword(15, x);

                    if (i % 100 == 0)
                    {
                        Console.WriteLine($"{i}:{x}:{password}");
                    }
                    byte[] bytes = Encoding.UTF8.GetBytes(password);
                    bytes = SHA256.Create().ComputeHash(bytes);
                    try
                    {
                        var returnBytes = AES_Decrypt(bytesToBeDecrypted, bytes);
                        Array.Copy(returnBytes, bytes2, 8);
                    }
                    catch { return; }
                    Console.WriteLine($"Successful decryption with seed {x} password {password}");
                    Console.WriteLine(Encoding.UTF8.GetString(bytes2, 0, 8));

                    if (bytes2.SequenceEqual(PNG_HEADER))
                    {
                        Console.WriteLine("Found it!" + password);
                        Console.Beep(); Console.Beep(); Console.Beep(); l.Stop();
                    }
                });
            Console.ReadLine();
        }
        public static string CreatePassword(int length, int seed)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random(seed);
            while (0 < length--)
            {
                stringBuilder.Append("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890*!=&?&/"[random.Next("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890*!=&?&/".Length)]);
            }
            return stringBuilder.ToString();
        }

        public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] result = null;
           
            using (MemoryStream memoryStream = new MemoryStream(1024))
            {
                using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
                {
                    rijndaelManaged.KeySize = 256;
                    rijndaelManaged.BlockSize = 128;
                    Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(256 / 8);
                    rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(128 / 8);
                    rijndaelManaged.Mode = CipherMode.CBC;
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cryptoStream.Close();
                    }
                    result = memoryStream.ToArray();
                }
            }
            return result;
        }
    }
}
