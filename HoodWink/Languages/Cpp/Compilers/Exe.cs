using System;
using System.Collections.Generic;

namespace HoodWink.Languages.Cpp.Compilers
{
    public class Exe : Models.Base.Compiler
    {
        // Example of Studios Compiler: run in powershell dev console "(get-command cl).path"
        // Or can use other, like Clang from LLVM
        public override string CompilerPath => @"C:\Program Files\Microsoft Visual Studio\2022\Community\VC\Tools\MSVC\14.36.32532\bin\HostX86\x86\cl.exe";

        public override string Compile(string sourcePath, List<string> dependencies)
        {
            Console.WriteLine($"Compiling: {sourcePath}");
            foreach (string dep in dependencies)
            {
                Console.WriteLine("File Dependency @ " + dep);
            }

            return $"./Path/To/CompiledLoader.exe";
        }
    }
}