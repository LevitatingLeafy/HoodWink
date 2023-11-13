﻿namespace HoodWink.Models.Base
{
    public abstract class Generator
    {
        public abstract string ProjectPath { get; } // Not Used

        public abstract string Gen(ref string targetSourcePath, ref string file, ref Models.Base.FormatExe formatInstance, ref Models.Base.Technique techniqueInstance, ref Models.Base.Protections protectionInstance, ref Models.Base.Extras extraInstance);
    }
}