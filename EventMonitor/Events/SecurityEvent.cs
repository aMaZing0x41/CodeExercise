using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMonitor.Events
{
    public abstract class SecurityEvent : IEvent
    {
        /// <summary>
        /// Tracks how many of this object are created.
        /// </summary>
        private static int _baseCount = 0;

        public SecurityEvent(DateTime date)
        {
            Date = date;
            System.Threading.Interlocked.Increment(ref _baseCount);
        }

        /// <summary>
        /// Gets the number of this class that have been instantiated.
        /// </summary>
        public static int Count
        {
            get { return _baseCount; }
        }

        /// <summary>
        /// Gets or sets the Date of the event.
        /// </summary>
        public DateTime Date
        {
            get;
            protected set;
        }
    }
}
