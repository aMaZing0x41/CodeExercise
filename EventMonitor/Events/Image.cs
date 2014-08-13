using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMonitor.Events
{
    /// <summary>
    /// An image event.
    /// </summary>
    public class Image : SecurityEvent
    {
        /// <summary>
        /// Tracks how many of this object are created.
        /// </summary>
        private static int _count = 0;

        public Image(DateTime date, byte[] bytes, int size)
            : base(date)
        {
            Bytes = bytes;
            Size = size;

            System.Threading.Interlocked.Increment(ref _count);
        }

        /// <summary>
        /// Gets the image data.
        /// </summary>
        /// <remarks>
        /// ASSUME: More work to do to get the data into bytes. Perhaps we 
        /// could just store the string, if needed.
        /// </remarks>
        public byte[] Bytes
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the size of the image.
        /// </summary>
        public int Size
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
