using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactNative.Tracing
{
    /// <summary>
    /// Represents a sequence of event fields and provides methods for adding fields to the sequence
    /// </summary>
    public class LoggingFields
    {
        private Dictionary<string, object> Fields = new Dictionary<string, object>();

        /// <summary>
        /// IsEmpty method
        /// </summary>
        public bool IsEmpty
        {
            get { return this.Fields.Count == 0; }
        }

        /// <summary>
        /// Add bool value to dictionary
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddBoolean(string name, Boolean value)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Fields.Add(name, value);
        }

        /// <summary>
        /// Add double value to dictionary
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddDouble(string name, Double value)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Fields.Add(name, value);
        }

        /// <summary>
        /// Add Guid value to dictionary
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddGuid(string name, Guid value)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Fields.Add(name, value);
        }

        /// <summary>
        /// Add Int16 value to dictionary
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddInt16(string name, Int16 value)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Fields.Add(name, value);
        }

        /// <summary>
        /// Add Int32 value to dictionary
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddInt32(string name, Int32 value)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Fields.Add(name, value);
        }

        /// <summary>
        /// Add Int64 value to dictionary
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddInt64(string name, Int64 value)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Fields.Add(name, value);
        }

        /// <summary>
        /// Add Single value to dictionary
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddSingle(string name, Single value)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Fields.Add(name, value);
        }

        /// <summary>
        /// Add String value to dictionary
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddString(string name, String value)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Fields.Add(name, value);
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            if (this.IsEmpty)
            {
                return String.Empty;
            }

            return String.Join(Environment.NewLine,
                (from element in this.Fields
                select $"{element.Key}: {element.Value}").ToArray());
        }
    }
}