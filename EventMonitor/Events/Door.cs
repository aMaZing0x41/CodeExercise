using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMonitor.Events
{
    /// <summary>
    /// A door event.
    /// </summary>
    public class Door : SecurityEvent
    {
        /// <summary>
        /// Tracks how many of this object are created.
        /// </summary>
        private static int _count = 0;

        public Door(DateTime date, bool open)
            : base(date)
        {
            Open = open;

            System.Threading.Interlocked.Increment(ref _count);
        }

        /// <summary>
        /// Gets a value that determines if the door is open.
        /// </summary>
        public bool Open
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
