using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTreePlanner
{
    public class BTLogger
    {
        public static bool GloballActive { get; private set; } = true;
        public static string LogFilePath { get; private set; } = null;
        public static string LogFileDirectory { get; private set; } = null;
        public bool IsActive { get; set; }
        public bool SaveToFile { get; set; }
        public bool ShowInConsole { get; set; }

        private static Queue<string> _logs = new();
        private static bool isWriting = false;
        private string parentScript;
        public BTLogger(string parent, bool showinconsole = true, bool isactive = true,bool savetofile = true) 
        {
            parentScript = parent;
            IsActive = isactive;
            ShowInConsole = showinconsole;
            SaveToFile = savetofile;

            if(LogFilePath == null)
            {
                LogFileDirectory = $@"{Application.dataPath}/Logs";
                DateTime now = DateTime.Now;
                string LogsName = $"LOG{now.Year}.{now.Month}.{now.Day}.{now.Hour}.{now.Minute}.{now.Second}.txt";
                LogFilePath = $@"{LogFileDirectory}/{LogsName}";
                Debug.Log(LogFilePath);
            }

            if(!Directory.Exists(LogFileDirectory))
            {
                Directory.CreateDirectory(LogFileDirectory);
            }
        }

        public void Log(string Method, string message = "")
        {
            if (!GloballActive)
            {
                return;
            }
            if (!IsActive)
            {
                return;
            }

            string timenow = $"[{DateTime.Now}]";
            string logmessage = $"{timenow}:{parentScript}:{Method}:{message}";

            if (ShowInConsole)
            {
                Debug.Log(logmessage);
            }

            if (SaveToFile)
            {
                _logs.Enqueue(logmessage);
            }

            if (!isWriting)
            {
                _ = WriteLogs();
            }
        }

        private async static Task WriteLogs()
        {
            isWriting = true;

            if (!File.Exists(LogFilePath))
            {
                File.Create(LogFilePath).Close();
            }
            try
            {
                using(StreamWriter str = new StreamWriter(LogFilePath,true))
                {
                    while(_logs.Count > 0)
                    {
                        await str.WriteLineAsync(_logs.Dequeue());
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.LogException(ex);
            }
            isWriting = false;
        }

    }
}
