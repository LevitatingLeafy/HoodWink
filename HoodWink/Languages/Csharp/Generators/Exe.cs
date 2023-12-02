﻿using HoodWink.Services;
using System;
using System.IO;
using System.Text;

namespace HoodWink.Languages.Csharp.Generators
{
    public class Exe : Models.Base.Generator
    {
        public override string Description => "Default Generator for Exe";
        public override string ProjectPath => throw new NotImplementedException(); // Not Used
        public override string Gen(ref string targetSourcePath, ref string file, ref Models.Base.FormatExe formatInstance, ref Models.Base.Technique techniqueInstance, ref string protection, ref Models.Base.Protections protectionInstance, ref Models.Base.Extras extraInstance)
        {
            string generatedPath = null;
            string newLine = "\n";
            string tab = "\t";

            // Generate File
            try
            {
                // Using
                string gen = "";
                gen += formatInstance.Using;
                gen += newLine;
                gen += extraInstance.Using;
                gen += newLine;
                gen += protectionInstance.Using;
                gen += newLine;
                gen += techniqueInstance.Using;
                gen += newLine;
                gen += newLine;

                // Namespace and Class Header
                gen += formatInstance.NamespaceAndClassHeader;
                gen += newLine;

                // Api Imports
                gen += techniqueInstance.ApiImports;
                gen += newLine;

                // Main Header
                gen += formatInstance.MainHeader;
                gen += newLine;

                // Main Logic
                gen += formatInstance.MainBody;
                gen += newLine;
                
                // Protection
                byte[] payload = File.ReadAllBytes(file);
                if (protection != "None")
                {
                    string encryptedPayload = CryptoService.Encrypt(payload, out string keyBase64, out string ivBase64);
                    //Console.WriteLine($"Decrypted : {BitConverter.ToString(CryptoService.Decrypt(encryptedPayload, keyBase64, ivBase64))}");   // Debug
                    //Console.WriteLine($"Decrypted : {Encoding.UTF8.GetString(CryptoService.Decrypt(encryptedPayload, keyBase64, ivBase64))}"); // Debug
                    gen += $"string b64 = \"{encryptedPayload}\";";
                    gen += newLine;
                    gen += $"string key = \"{keyBase64}\";";
                    gen += newLine;
                    gen += $"string iv  = \"{ivBase64}\";";
                    gen += newLine;
                }
                else // No Protection
                {
                    string encryptedPayload = CryptoService.Encode(payload);
                    gen += $"string b64 = \"{encryptedPayload}\";";
                    gen += newLine;
                }
                gen += extraInstance.MainLogic;
                gen += newLine;
                gen += protectionInstance.MainLogic; // decryption happens here
                gen += newLine;
                gen += techniqueInstance.MainLogic; // uses decrypted payload
                gen += newLine;
                gen += newLine;

                // Main Footer
                gen += formatInstance.MainFooter;
                gen += newLine;
                gen += newLine;

                // Additional Functions
                gen += extraInstance.AdditionalFunctions;
                gen += newLine;
                gen += protectionInstance.AdditionalFunctions;
                gen += newLine;
                gen += techniqueInstance.AdditionalFunctions;
                gen += newLine;

                // Namespace and Class Header
                gen += formatInstance.NamespaceAndClassFooter;
                gen += newLine;

                // Save to file
                File.WriteAllText(targetSourcePath, gen);
                // return path
                generatedPath = targetSourcePath;
            }
            catch (Exception ex)
            {
                WriteService.Error(ex.Message);
            }

            return generatedPath;
        }
    }
}