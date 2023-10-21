using HoodWink.Models;
using HoodWink.Utils;

using System;
using System.IO;
using HoodWink.Services;

namespace HoodWink.Services
{
    public static class GeneratorService
    {

        public static string Generate(string file, Module Mod)
        {
            // bool status = false;
            string filename = null;

            // Encrypt Payload
            byte[] payload = File.ReadAllBytes(file);
            string encryptedPayload = Crypto.Encrypt(payload, out string keyBase64, out string ivBase64);
            byte[] decrypted = Crypto.Decrypt(encryptedPayload, keyBase64, ivBase64);

            // Decryption Test
            if (Crypto.Equals(ref payload, ref decrypted))
            {
                // Create filename
                filename = Mod.Name + "-" + Guid.NewGuid() + ".cs";

                // Payload Dir
                string curDir = Directory.GetCurrentDirectory(); // Change to project Directory
                string payloadDir = curDir + @"\Payloads\";

                // Create Target                
                string target = payloadDir + filename;

                // Create dir if needed                
                if (!Directory.Exists(payloadDir))
                {
                    Directory.CreateDirectory(payloadDir);
                }

                // Generate and write to Target
                try
                {
                    string a = Mod.Header;

                    string b = $"string b64 = \"{encryptedPayload}\";";
                    string c = $"string key = \"{keyBase64}\";";
                    string d = $"string iv  = \"{ivBase64}\";";

                    string e = Mod.Core;
                    string f = Mod.Footer;

                    string newLine = "\n";
                    string tab = "\t\t\t";
                    string build = a + b + newLine + tab + c + newLine + tab + d + newLine + tab + e + newLine + "\t\t" + f;

                    File.WriteAllText(target, build);
                    //   status = true;

                    WriteService.Success("Generated source: " + target);
                }
                catch (Exception e)
                {
                    WriteService.Error("Generation error: " + e);
                    //   status = false;
                    filename = null;
                }

                //  return status;
                return filename;

            }
            else
            {
                WriteService.ErrorExit("Failed Decryption Test");
                //  return status;
                return filename;
            }
        }
    }
}