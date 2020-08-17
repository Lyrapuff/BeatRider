﻿using System;
using System.IO;

namespace YouTubeSearch
{
    public class Log
    {
        private static bool isEnabled;

        public static bool getMode()
        {
            return isEnabled;
        }

        /// <summary>
        ///     Enable / Disable loggin. NOTE: If you enable logging you have to set
        ///     a folder variable at Helper.Folder!
        /// </summary>
        /// <param name="Mode"></param>
        /// <returns></returns>
        public static bool setMode(bool Mode)
        {
            return isEnabled = Mode;
        }

        public static void println(string Folder, string Message)
        {
#if DEBUG
            Console.WriteLine("[" + DateTime.Now + "]" + " " + Message);
#endif

            try
            {
                // Data folder path
                var dataPath = Folder;

                if (!Directory.Exists(dataPath))
                {
                    Directory.CreateDirectory(dataPath);
                }

                // Logfile
                dataPath = Path.Combine(dataPath, "log");

                if (!Directory.Exists(dataPath))
                {
                    Directory.CreateDirectory(dataPath);
                }

                if (!File.Exists(Path.Combine(dataPath, "log.txt")))
                {
                    using (File.Create(Path.Combine(dataPath, "log.txt")))
                    {
                    }

                    ;
                }

                File.AppendAllText(Path.Combine(dataPath, "log.txt"),
                    "[" + DateTime.Now + "]" + " " + Message + Environment.NewLine);
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine("[" + DateTime.Now + "]" + " " + ex);
#endif
                throw new Exception(ex.ToString());
            }
        }
    }
}