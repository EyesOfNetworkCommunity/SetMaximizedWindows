using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace SetActiveWindows
{
    class Program
    {
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, uint Msg);

        private const uint SW_RESTORE = 0x09;

        static int Main(string[] args)
        {
            int iDebug;
            int iPID;
            Process TargetedProcess;

            if (args.Length < 1)  // Warning : Index was out of the bounds of the array
            {
                Console.Write("Usage:\n");
                Console.Write("SetActiveWindows.exe PID Flag\n");
                Console.Write("Flag:\n");
                Console.Write("\t 0: Short output, no debug\n");
                Console.Write("\t 1: Full debug\n");
                Console.Write("Ex: SetActiveWindows.exe 14522 1\n");
                return 1;
            }

            if (args.Length < 1)  // Warning : Index was out of the bounds of the array
            {
                iDebug = 0;
            }
            else
            {
                iDebug = 0;
                if (Convert.ToInt32(args[1]) == 1)
                {
                    iDebug = Convert.ToInt32(args[1]);
                }
            }

            iPID = Convert.ToInt32(args[0]);
            TargetedProcess = Process.GetProcessById(iPID);
            SetForegroundWindow(TargetedProcess.MainWindowHandle);
            ShowWindow(TargetedProcess.MainWindowHandle, SW_RESTORE);
            return 0;
        }
    }
}