using System;
using System.Collections.Generic;

namespace HoodWink.Languages.Cpp.Techniques
{
    public class Inline : Models.Base.Technique
    {
        public override List<string> FileDependencies => new List<string> { "One.cs", "Two.cs", "Three.cs" };

        public override string Using => throw new NotImplementedException();

        public override string ApiImports => throw new NotImplementedException();

        public override string MainLogic => throw new NotImplementedException();

        public override string AdditionalFunctions => throw new NotImplementedException();
    }
}