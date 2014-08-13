using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using EventMonitor;
using EventMonitor.Events;

namespace EventMonitorTests
{
    [TestClass]
    public class EventTests
    {
        [TestMethod]
        public void AlarmCreated()
        {
            Alarm alarm = new Alarm(DateTime.Now, "Test", 13, "Scary");
            Assert.IsNotNull(alarm);
        }

        [TestMethod]
        public void AlarmCount()
        {
            Assert.IsTrue(Alarm.Count > 0);
        }

        [TestMethod]
        public void DoorCreated()
        {
            Door door = new Door(DateTime.Now, false);
            Assert.IsNotNull(door);
        }

        [TestMethod]
        public void DoorCount()
        {
            Assert.IsTrue(Door.Count > 0);
        }

        [TestMethod]
        public void ImageCreated()
        {
            byte[] bytes = new byte[5];
            Image image = new Image(DateTime.Now, bytes, bytes.Length);
            Assert.IsNotNull(image);
        }

        [TestMethod]
        public void ImageCount()
        {
            Assert.IsTrue(Image.Count > 0);
        }

        [TestMethod]
        public void SecurityEventCount()
        {
            Assert.IsTrue(SecurityEvent.Count > 0);
        }

        [TestMethod]
        public void EventFactory_GetAlarmEventJson()
        {
            SecurityEvent alarm = EventMonitor.Events.EventFactory.GetByJSON("{\"Type\":\"alarm\", \"Date\":\"2014-02-01 10:01:05\", \"name\":\"fire\", \"floor\":\"1\", \"room\":\"101\"}");
            Assert.IsTrue(alarm is SecurityEvent);
            Assert.IsTrue(alarm is Alarm);
        }

        [TestMethod]
        public void EventFactory_GetDoorEventJson()
        {
            SecurityEvent door = EventMonitor.Events.EventFactory.GetByJSON("{\"Type\":\"Door\", \"Date\":\"2014-02-01 10:01:02\", \"open\": true}");
            Assert.IsTrue(door is SecurityEvent);
            Assert.IsTrue(door is Door);
        }

        [TestMethod]
        public void EventFactory_GetImageEventJson()
        {
            SecurityEvent image = EventMonitor.Events.EventFactory.GetByJSON("{\"Type\":\"img\", \"Date\":\"2014-02-01 10:01:02\", \"bytes\": \"ab39szh6\", \"size\": 8}");
            Assert.IsTrue(image is SecurityEvent);
            Assert.IsTrue(image is Image);
        }

        [TestMethod]
        public void EventFactory_GetMalformedEventJson()
        {
            SecurityEvent malformed = null;
            try
            {
                malformed = EventMonitor.Events.EventFactory.GetByJSON("malformed");
            }
            catch
            {
                // Can't find a good way to test for thrown exception.
                Assert.IsNull(malformed);
            }
        }

        [TestMethod]
        public void EventProcessor_Process()
        {
            string path = "C:\\temp\\door.json";
            System.IO.File.WriteAllText(path, "{\"Type\":\"Door\", \"Date\":\"2014-02-01 10:01:02\", \"open\": true}");
            Assert.IsTrue(EventProcessor.Process(path));
            System.IO.File.Delete(path);
        }
    }
}
