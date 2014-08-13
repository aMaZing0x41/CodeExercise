using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

using EventMonitor.Events;

namespace EventMonitor
{
    /// <summary>
    /// Monitors a directory for newly created files, sends the new files to processing, 
    /// captures some timing, and reports processing status every second.
    /// </summary>
    public class DirectoryMonitor : IDisposable
    {
        private Thread _monitorThread;
        private FileSystemWatcher _watcher;
        private string _path;
        private long _count = 0;
        private long _total = 0;
        private object _locker = new object();

        public DirectoryMonitor(string path, string filter = "*.json")
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            if (!path.EndsWith("\\"))
            {
                path += "\\";
            }

            _path = path;
            _watcher = new FileSystemWatcher(_path, filter);
            _watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.CreationTime;

            _watcher.Created += new FileSystemEventHandler(OnFileCreate);
            _watcher.EnableRaisingEvents = true;

            //ASSUME: Do not process files on startup.

            _monitorThread = new Thread(MonitorThreadStart);
            _monitorThread.Start();
        }

        /// <summary>
        /// Gets the average processing for all the events processed.
        /// </summary>
        /// <returns>Average time in ms that one event takes to process.</returns>
        private double GetAverageProcessingTime()
        {
            lock (_locker)
            {
                if (_count > 0)
                {
                    return _total / ((double)_count);
                }
                else
                {
                    return 0d;
                }
            }
        }

        /// <summary>
        /// Gets fired every time a new file is created in _path. Builds a path and calls the 
        /// EventProcessor to process the file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFileCreate(object sender, FileSystemEventArgs e)
        {
            var timer = System.Diagnostics.Stopwatch.StartNew();
            string path = Path.Combine(_path, e.Name);
            if (File.Exists(path))
            {
                System.Diagnostics.Debug.WriteLine(string.Format("File Created: {0}", path));
                EventProcessor.Process(path);

                lock (_locker)
                {
                    _total += timer.ElapsedMilliseconds;
                    _count++;
                }
            }
            else
            {
                Console.WriteLine("File does not exist. Skipping: " + path);
            }
        }

        /// <summary>
        /// Monitor thread that reports status of event processing every second.
        /// </summary>
        private void MonitorThreadStart()
        {
            while (true)
            {
                Thread.Sleep(1000);

                //"EventCnt: 1, ImgCnt:0, AlarmCnt:0, avgProcessingTime: 10ms"
                // ASSUME: EventCnt is total events processed; ImgCnt, AlarmCnt, and DoorCnt are counts
                // for each type of event.
                Console.WriteLine(
                    string.Format("EventCnt: {0}, ImgCnt: {1}, AlarmCnt: {2}, DoorCnt: {3}, avgProcessingTime: {4}ms",
                    SecurityEvent.Count,
                    Image.Count,
                    Alarm.Count,
                    Door.Count,
                    GetAverageProcessingTime()));
            }
        }

        public void Dispose()
        {
            if (_watcher != null)
            {
                _watcher.Dispose();
            }
        }
    }
}
