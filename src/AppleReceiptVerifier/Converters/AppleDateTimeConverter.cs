using System;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AppleReceiptVerifier.Converters
{
    /// <summary>
    /// Apple date time converter
    /// </summary>
    internal class AppleDateTimeConverter : DateTimeConverterBase
    {
        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The DateTime
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var workingDateString = reader.Value.ToString();
            string[] dateparts = workingDateString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            DateTime parsedDate = DateTime.MinValue;

            if (dateparts.Count() >= 2)
            {
                DateTime.TryParse(string.Format("{0} {1}", dateparts[0], dateparts[1]), CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
            }

            return parsedDate;
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
