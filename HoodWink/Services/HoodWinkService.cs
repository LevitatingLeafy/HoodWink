using HoodWink.Models;
using HoodWink.Modules;
using HoodWink.Utils;

using System;
using HoodWink.Services;

namespace HoodWink.Services
{
    public static class HoodWinkService
    {
        public static void Run(string file, Models.Module mod)
        {
            WriteService.Progress(mod.Name);

            string sourceFile = null;
            // Generate
            sourceFile = GeneratorService.Generate(file, mod);

            // Compile
            if (sourceFile != null)
            {
                string compiledSource = CompilerService.Compile(sourceFile);
                //Console.WriteLine();

                if (compiledSource != null)
                {
                    // Suggested PS
                    CommandService.Suggest(compiledSource, mod.Type);
                }
                else
                {
                    WriteService.ErrorExit("Compilation Failed");
                }
                Console.WriteLine();
            }
        }
    }
}