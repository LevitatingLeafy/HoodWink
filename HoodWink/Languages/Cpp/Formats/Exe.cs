using System;
using System.Collections.Generic;

namespace HoodWink.Languages.Cpp.Formats
{
    public class Exe : Models.Base.FormatExe
    {
        public override List<string> FileDependencies => throw new NotImplementedException();

        public override string Using => throw new NotImplementedException();

        public override string NamespaceAndClassHeader => throw new NotImplementedException();
        public override string MainHeader => throw new NotImplementedException();

        public override string MainBody => throw new NotImplementedException();

        public override string MainFooter => throw new NotImplementedException();

        public override string NamespaceAndClassFooter => throw new NotImplementedException();
    }
}