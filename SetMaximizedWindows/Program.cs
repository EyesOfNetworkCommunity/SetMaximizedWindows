using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace SetMaximizedWindows
{
    class Program
    {
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, uint Msg);

        private const uint SW_MAXIMIZE = 0x03;

        static int Main(string[] args)
        {
            int iDebug;
            int iPID;
            bool BoolSetForegroundWindow;
            Process TargetedProcess;

            if (args.Length < 1)  // Warning : Index was out of the bounds of the array
            {
                Console.Write("Usage:\n");
                Console.Write("SetMaximizedWindows.exe PID Flag\n");
                Console.Write("Flag:\n");
                Console.Write("\t 0: Short output, no debug\n");
                Console.Write("\t 1: Full debug\n");
                Console.Write("Ex: SetMaximizedWindows.exe 14522 1\n");
                return 1;
            }

            if (args.Length < 2)  // Warning : Index was out of the bounds of the array
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
            if (iDebug > 0)
            {
                Console.Write("Debug mode\n");
                Console.Write("PID:" + iPID.ToString() + "\n");
            }

            if (Process.GetProcesses().Any(x => x.Id == iPID))
            {
                TargetedProcess = Process.GetProcessById(iPID);
            }
            else
            {
                Console.Write("Could not find the PID " + iPID.ToString() + " in current process list.\n");
                return 2;
            }

            if (iDebug > 0)
            {
                Console.Write("TargetProcess:" + TargetedProcess.ToString() + "\n");
            }
            BoolSetForegroundWindow = SetForegroundWindow(TargetedProcess.MainWindowHandle);
            if (iDebug > 0)
            {
                Console.Write("BoolSetForegroundWindow:" + BoolSetForegroundWindow.ToString() + "\n");
            }

            if (BoolSetForegroundWindow == true)
            {
                return (ShowWindow(TargetedProcess.MainWindowHandle, SW_MAXIMIZE));
            }
            else
            {
                Console.Write("Error could not handle pid " + iPID.ToString() + "\n");
                return 2;
            }
        }
    }
}