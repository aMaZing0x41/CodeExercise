using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EventMonitor.Events;

namespace EventMonitor
{
    /// <summary>
    /// Process the incoming files.
    /// </summary>
    public static class EventProcessor
    {
        /// <summary>
        /// Given a file path will wait until the file is free, open the file, grab the contents, 
        /// and create a SecurityEvent.
        /// </summary>
        /// <param name="path">Path to the file that contains the JSON for the SecurityEvent.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public static bool Process(string path)
        {
            FileStream stream = null;
            byte[] buffer = null;

            try
            {
                if (File.Exists(path))
                {
                    while (true)
                    {
                        try
                        {
                            stream = System.IO.File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                            buffer = new byte[stream.Length];
                            stream.Read(buffer, 0, buffer.Length);

                            break;
                        }
                        catch (IOException)
                        {
                            System.Diagnostics.Debug.WriteLine("File is locked. Waiting 5ms. File: " + path);
                            System.Threading.Thread.Sleep(5);
                        }
                        finally
                        {
                            if (stream != null)
                            {
                                stream.Close();
                            }
                        }
                    }

                    SecurityEvent myEvent = EventMonitor.Events.EventFactory.GetByJSON(Encoding.ASCII.GetString(buffer));
                    return true;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(string.Format("Could not read file {0}. Message: {1}", path, ex.Message));
            }

            return false;
        }
    }
}
