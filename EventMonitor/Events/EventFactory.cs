using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMonitor.Events
{
    /// <summary>
    /// Factory used to create SecurityEvents.
    /// </summary>
    public static class EventFactory
    {
        /// <summary>
        /// Given a json string will create a SecurityEvent.
        /// </summary>
        /// <param name="json">JSON string that represents the object.</param>
        /// <returns>A SecurityEvent of the specific type specified in the json.</returns>
        public static SecurityEvent GetByJSON(string json)
        {
            try
            {
                Newtonsoft.Json.Linq.JObject temp = Newtonsoft.Json.Linq.JObject.Parse(json.Trim());
                string typeName = (string)temp["Type"];

                switch (typeName.ToUpperInvariant())
                {
                    case "ALARM":
                        Alarm alarm = new Alarm(
                            (DateTime)temp["Date"], 
                            (string)temp["name"], 
                            (int)temp["floor"], 
                            (string)temp["room"]);

                        return alarm;

                    case "DOOR":
                        Door door = new Door((DateTime)temp["Date"], (bool)temp["open"]);
                        return door;

                    case "IMG":
                        Image image = new Image(
                            (DateTime)temp["Date"], 
                            (byte[])temp["bytes"],
                            (int)temp["size"]);

                        return image;
                    default:
                        throw new ArgumentException(string.Format("Event type {0} is not implemented.", typeName));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
