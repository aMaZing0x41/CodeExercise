using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMonitor.Events
{
    /// <summary>
    /// Basic event interface.
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// Gets the date and time of the IEvent.
        /// </summary>
        DateTime Date { get; }
    }
}
