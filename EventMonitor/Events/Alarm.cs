using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMonitor.Events
{
    /// <summary>
    /// An alarm event.
    /// </summary>
    public class Alarm : SecurityEvent
    {
        /// <summary>
        /// Tracks how many of this object are created.
        /// </summary>
        private static int _count = 0;

        public Alarm(DateTime date, string name, int floor, string room)
            : base(date)
        {
            Name = name;
            Floor = floor;
            Room = room;

            System.Threading.Interlocked.Increment(ref _count);
        }

        /// <summary>
        /// Gets the name of the type of alarm.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the floor of the building the alarm occurred.
        /// </summary>
        public int Floor
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the room that the alarm occurred in.
        /// </summary>
        /// <remarks>
        /// Note: I assumed this could be something besides a number so I made it a string.
        /// </remarks>
        public string Room
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the number of this class that have been instantiated.
        /// </summary>
        new public static int Count
        {
            get { return _count; }
        }
    }
}
