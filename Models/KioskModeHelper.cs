﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GymeeDestkopApp.Models
{
    public class KioskModeHelper
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool PostMessage(IntPtr hWnd, [MarshalAs(UnmanagedType.U4)] uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        const int WM_USER = 0x0400; //http://msdn.microsoft.com/en-us/library/windows/desktop/ms644931(v=vs.85).aspx

        public static void BeginKioskMode()
        {
            //var explorers = Process.GetProcessesByName("explorer");
            //foreach (var thisExplorer in explorers)
            //{
            //    thisExplorer.Kill();
            //}
            // Create a ProcessStartInfo, otherwise Explorer comes back to haunt you.
            ProcessStartInfo TaskKillPSI = new ProcessStartInfo("taskkill", "/F /IM explorer.exe");
            // Don't show a window
            TaskKillPSI.WindowStyle = ProcessWindowStyle.Hidden;
            // Create and start the process, then wait for it to exit.
            Process process = new Process();
            process.StartInfo = TaskKillPSI;
            process.Start();
            process.WaitForExit();
        }

        public static void StartExplorerAndExit()
        {
            int retry = 10;
            try
            {
                var ptr = FindWindow("Shell_TrayWnd", null);
                Console.WriteLine("INIT PTR: {0}", ptr.ToInt32());
                PostMessage(ptr, WM_USER + 436, (IntPtr)0, (IntPtr)0);

                do
                {
                    ptr = FindWindow("Shell_TrayWnd", null);
                    Console.WriteLine("PTR: {0}", ptr.ToInt32());

                    if (ptr.ToInt32() == 0)
                    {
                        Console.WriteLine("Success. Breaking out of loop.");
                        break;
                    }

                    System.Threading.Thread.Sleep(1000);
                    retry -= 1;
                } while (retry > 10);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} {1}", ex.Message, ex.StackTrace);
            }
            string explorer = string.Format("{0}\\{1}", Environment.GetEnvironmentVariable("WINDIR"), "explorer.exe");
            Process process = new Process();
            process.StartInfo.FileName = explorer;
            process.StartInfo.UseShellExecute = true;
            process.Start();
            process.WaitForExit();
           // Process.GetCurrentProcess().Kill();
        }
    }
}
