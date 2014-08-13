using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace EventMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press ctrl-c to quit monitor.");
            DirectoryMonitor monitor = new DirectoryMonitor(@"C:\temp");
        }
    }
}
