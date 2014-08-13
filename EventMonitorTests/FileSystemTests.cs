using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using EventMonitor;
using EventMonitor.Events;

namespace EventMonitorTests
{
    [TestClass]
    public class FileSystemTests
    {
        DirectoryMonitor _monitor;

        [TestInitialize]
        public void SetUp()
        {
            _monitor = new DirectoryMonitor(@"C:\temp");

            System.IO.FileStream stream = null;
            try
            {
                stream = System.IO.File.Create(@"C:\temp\testing.json");
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

            System.Threading.Thread.Sleep(250);
        }

        [TestCleanup]
        public void CleanUp()
        {
            try
            {
                System.IO.File.Delete(@"C:\temp\testing.json");
            }
            catch
            {
            }
        }

        [TestMethod]
        public void DirectoryMonitorCreated()
        {
            Assert.IsNotNull(_monitor);
        }
    }
}
