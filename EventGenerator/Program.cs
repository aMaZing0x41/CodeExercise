using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventGenerator
{
    /// <summary>
    /// Testing application for test EventMonitor.
    /// </summary>
    class Program
    {
        const string ALARM = "{\"Type\":\"alarm\", \"Date\":\"2014-02-01 10:01:05\", \"name\":\"fire\", \"floor\":\"1\", \"room\":\"101\"}";
        const string DOOR = "{\"Type\":\"Door\", \"Date\":\"2014-02-01 10:01:02\", \"open\": true}";
        const string IMG = "{\"Type\":\"img\", \"Date\":\"2014-02-01 10:01:02\", \"bytes\": \"ab39szh6\", \"size\": 8}";

        static void Main(string[] args)
        {
            Console.WriteLine("Press Ctrl-C to quit generating events.");
            Console.WriteLine("Press <enter> to start generating");
            Console.ReadLine();
            int count = 0;
            int sleep = 5000;

            if (args.Length > 0)
            {
                sleep = int.Parse(args[0]);
            }

            Random random = new Random((int)DateTime.Now.Ticks);
            string path = string.Empty;

            while (true)
            {

                FileStream stream = null;

                string myEvent = string.Empty;

                switch (random.Next(3))
                {
                    case 0:
                        myEvent = ALARM;
                        break;
                    case 1:
                        myEvent = IMG;
                        break;
                    default:
                        myEvent = DOOR;
                        break;
                }

                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(myEvent);
                path = string.Format(@"C:\temp\{0}.json", Guid.NewGuid().ToString());

                try
                {
                    stream = File.OpenWrite(path);
                    stream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                }

                Console.WriteLine("File generated.");
                count++;

                System.Threading.Thread.Sleep(sleep);
            }
        }
    }
}
