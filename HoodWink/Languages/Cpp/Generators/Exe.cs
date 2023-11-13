using System;

namespace HoodWink.Languages.Cpp.Generators
{
    public class Exe : Models.Base.Generator
    {
        public override string ProjectPath => throw new NotImplementedException();
        public override string Gen(ref string targetSourcePath, ref string file, ref Models.Base.FormatExe formatInstance, ref Models.Base.Technique techniqueInstance, ref Models.Base.Protections protectionInstance, ref Models.Base.Extras extraInstance)
        {
            Console.WriteLine("Not Implemented");

            return @"./Path/To/GeneratedFile.cpp";
        }
    }
}