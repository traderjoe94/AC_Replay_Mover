using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace AC_Replay_Mover
{
    class Program
    {
            // Read Application Settings from App.config file
        private static NameValueCollection appSettings = ConfigurationManager.AppSettings;
        //TODO: Minimizing / Shrinking to taskbar
        //TODO: Icon
        static void Main(string[] args)
        {
            // Create a new FileSystemWatcher and set its properties.
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                // Set Watcher Path according to App Settings
                watcher.Path = appSettings["sourceDirectory"];
                Log("WATCHED PATH: " + watcher.Path, LogType.Information);

                // Only watch .acreplay files
                watcher.Filter = "*.acreplay";

                // Add event handler.
                watcher.Created += OnCreated;

                // Begin watching.
                watcher.EnableRaisingEvents = true;

                // Run indefinitely.
                while (true) ;

            }
        }

        // Define the event handler.
        private static void OnCreated(object source, FileSystemEventArgs e)
        {
            // file created
            Log($"File: {e.FullPath} {e.ChangeType}", LogType.Information);
            try
            {
                // copy file from source to destination
                File.Copy(e.FullPath, appSettings["destinationDirectory"] + e.Name);
                Log("File " + e.Name + " copied to destination " + appSettings["destinationDirectory"], LogType.Information);
                // delete file from source
                File.Delete(e.FullPath);
                Log("File " + e.Name + " deleted from source directory.", LogType.Information);
            }
            catch (IOException copyError)
            {
                Log(copyError.Message, LogType.Error);
            }

        }
        /// <summary>
        /// Fügt einen Log-Eintrag entsprechend der angegebenen Message und des angegebenen LogTypes in die Log-Datei ein (im Ordner der ausgeführten .exe)
        /// </summary>
        /// <param name="message"></param> die zu loggende Nachricht.
        /// <param name="type"></param> der zu verwendende LogType.
        private static void Log(string message, LogType type)
        {
            Trace.Listeners.Add(new TextWriterTraceListener("TextWriterOutput.log", "myListener"));
            Console.WriteLine(DateTime.Now + " : " + message);
            switch (type)
            {
                case LogType.Information:
                    Trace.TraceInformation(DateTime.Now + " : " + message);
                    Trace.Flush();
                    break;
                case LogType.Error:
                    Trace.TraceError(DateTime.Now + " : " + message);
                    Trace.Flush();
                    break;
                case LogType.Warning:
                    Trace.TraceWarning(DateTime.Now + " : " + message);
                    Trace.Flush();
                    break;
                default:
                    Trace.TraceError("Something went wrong with the logger");
                    Trace.Flush();
                    break;
            }
        }
    }
    enum LogType
    {
        Information,
        Error,
        Warning
    }
}

