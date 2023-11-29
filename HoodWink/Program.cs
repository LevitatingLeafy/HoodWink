using HoodWink.Services;
using System;

namespace HoodWink
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Usage();
                System.Environment.Exit(1);
            }

            string file = null;
            string lang = null;
            string form = null;
            string tech = null;
            string prot = null;
            string extr = null;  // Will be List later

            // Parse Args
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-file" && args.Length >= i + 1)
                {
                    file = args[i + 1];
                }
                else if (args[i] == "-lang" && args.Length >= i + 1)
                {
                    lang = args[i + 1];
                }
                else if (args[i] == "-form" && args.Length >= i + 1)
                {
                    form = args[i + 1];
                }
                else if (args[i] == "-tech" && args.Length >= i + 1)
                {
                    tech = args[i + 1];
                }
                else if (args[i] == "-prot" && args.Length >= i + 1)
                {
                    prot = args[i + 1];
                }
                else if (args[i] == "-extr" && args.Length >= i + 1)
                {
                    extr = args[i + 1];
                }
                else if (args[i] == "-langs")
                {
                    PrintService.PrintLanguages();
                    System.Environment.Exit(0);
                }
                else if (args[i] == "-showall")
                {
                    PrintService.PrintAllModules();
                    System.Environment.Exit(0);
                }
                else if (args[i] == "-show" && args.Length >= i + 2)
                {
                    Console.WriteLine($"args.length : {args.Length}");
                    PrintService.PrintLanguageModules(args[i + 1]);                    
                    System.Environment.Exit(0);
                }                
                else if (args[i] == "-descall")
                {
                    PrintService.PrintAllModulesDescriptions();
                    System.Environment.Exit(0);
                }
                else if (args[i] == "-desc" && args.Length >= i + 2)
                {
                    PrintService.PrintLanguageModulesDescriptions(args[i + 1]);
                    System.Environment.Exit(0);
                }
            }

            // Check if args set
            if (file is null || lang is null || form is null || tech is null || prot is null || extr is null)
            {
                WriteService.Error("Args Error");
                Usage();
                System.Environment.Exit(0);
            }

            // Build
            WinkService.BuildExe(file, lang, form, extr, prot, tech);
        }

        // Usage
        private static void Usage()
        {
            WriteService.Header("Show Modules: ");
            WriteService.Info(@"    .\HoodWink.exe  -langs        :  Show all Languages");
            WriteService.Info(@"    .\HoodWink.exe  -showall      :  Show all Modules");
            WriteService.Info(@"    .\HoodWink.exe  -show <lang>  :  Show Modules for lang");
            WriteService.Info(@"    .\HoodWink.exe  -descall      :  Show all Modules + Descriptions");
            WriteService.Info(@"    .\HoodWink.exe  -desc <lang>  :  Show Modules + Descriptions for lang");
            WriteService.Header("Syntax: ");
            WriteService.Info(@"    .\HoodWink.exe -file <name> -lang <name> -form <name> -extr <name> -prot <name> -tech <name> ");
            WriteService.Header("Example: ");
            WriteService.Info(@"    .\HoodWink.exe -file C:\Payloads\msf.bin -lang Csharp -form Exe -extr AmsiBypass -prot Aes256 -tech Spawn_QueueApc");
        }
    }
}