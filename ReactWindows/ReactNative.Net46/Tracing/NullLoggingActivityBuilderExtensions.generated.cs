using System;
using System.Windows;

namespace ReactNative.Tracing
{
    static partial class LoggingFieldsExtensions
    {
        public static LoggingActivityBuilder With(this LoggingActivityBuilder builder, string name, Boolean value)
        {
            if (builder != null)
            {
                builder.Fields.AddBoolean(name, value);
            }

            return builder;
        }

        public static LoggingActivityBuilder With(this LoggingActivityBuilder builder, string name, Boolean[] value)
        {
            if (builder != null)
            {
                foreach (var val in value)
                {
                    builder.Fields.AddBoolean(name, val);
                }
            }

            return builder;
        }

        public static LoggingActivityBuilder With(this LoggingActivityBuilder builder, string name, Double value)
        {
            if (builder != null)
            {
                builder.Fields.AddDouble(name, value);
            }

            return builder;
        }

        public static LoggingActivityBuilder With(this LoggingActivityBuilder builder, string name, Double[] value)
        {
            if (builder != null)
            {
                foreach (var val in value)
                {
                    builder.Fields.AddDouble(name, val);
                }
            }

            return builder;
        }

        public static LoggingActivityBuilder With(this LoggingActivityBuilder builder, string name, Guid value)
        {
            if (builder != null)
            {
                builder.Fields.AddGuid(name, value);
            }

            return builder;
        }

        public static LoggingActivityBuilder With(this LoggingActivityBuilder builder, string name, Guid[] value)
        {
            if (builder != null)
            {
                foreach (var val in value)
                {
                    builder.Fields.AddGuid(name, val);
                }
            }
            return builder;
        }

        public static LoggingActivityBuilder With(this LoggingActivityBuilder builder, string name, Int16 value)
        {
            if (builder != null)
            {
                builder.Fields.AddInt16(name, value);
            }

            return builder;
        }

        public static LoggingActivityBuilder With(this LoggingActivityBuilder builder, string name, Int16[] value)
        {
            if (builder != null)
            {
                foreach (var val in value)
                {
                   builder.Fields.AddInt16(name, val);
                }
            }

            return builder;
        }

        public static LoggingActivityBuilder With(this LoggingActivityBuilder builder, string name, Int32 value)
        {
            if (builder != null)
            {
                builder.Fields.AddInt32(name, value);
            }

            return builder;
        }

        public static LoggingActivityBuilder With(this LoggingActivityBuilder builder, string name, Int32[] value)
        {
            if (builder != null)
            {
                foreach (var val in value)
                {
                    builder.Fields.AddInt32(name, val);
                }
            }

            return builder;
        }

        public static LoggingActivityBuilder With(this LoggingActivityBuilder builder, string name, Int64 value)
        {
            if (builder != null)
            {
                builder.Fields.AddInt64(name, value);
            }

            return builder;
        }

        public static LoggingActivityBuilder With(this LoggingActivityBuilder builder, string name, Int64[] value)
        {
            if (builder != null)
            {
                foreach (var val in value)
                {
                    builder.Fields.AddInt64(name, val);
                }
            }

            return builder;
        }

        public static LoggingActivityBuilder With(this LoggingActivityBuilder builder, string name, Single value)
        {
            if (builder != null)
            {
                builder.Fields.AddSingle(name, value);
            }

            return builder;
        }

        public static LoggingActivityBuilder With(this LoggingActivityBuilder builder, string name, Single[] value)
        {
            if (builder != null)
            {
                foreach (var val in value)
                {
                    builder.Fields.AddSingle(name, val);
                }
            }

            return builder;
        }

        public static LoggingActivityBuilder With(this LoggingActivityBuilder builder, string name, String value)
        {
            if (builder != null)
            {
                builder.Fields.AddString(name, value);
            }

            return builder;
        }

        public static LoggingActivityBuilder With(this LoggingActivityBuilder builder, string name, String[] value)
        {
            if (builder != null)
            {
                foreach (var val in value)
                {
                    builder.Fields.AddString(name, val);
                }
            }

            return builder;
        }

        public static NullLoggingActivityBuilder With(this NullLoggingActivityBuilder builder, string name, TimeSpan value)
        {
            return builder;
        }

        public static NullLoggingActivityBuilder With(this NullLoggingActivityBuilder builder, string name, TimeSpan[] value)
        {
            return builder;
        }

        public static NullLoggingActivityBuilder With(this NullLoggingActivityBuilder builder, string name, UInt16 value)
        {
            return builder;
        }

        public static NullLoggingActivityBuilder With(this NullLoggingActivityBuilder builder, string name, UInt16[] value)
        {
            return builder;
        }

        public static NullLoggingActivityBuilder With(this NullLoggingActivityBuilder builder, string name, UInt32 value)
        {
            return builder;
        }

        public static NullLoggingActivityBuilder With(this NullLoggingActivityBuilder builder, string name, UInt32[] value)
        {
            return builder;
        }

        public static NullLoggingActivityBuilder With(this NullLoggingActivityBuilder builder, string name, UInt64 value)
        {
            return builder;
        }

        public static NullLoggingActivityBuilder With(this NullLoggingActivityBuilder builder, string name, UInt64[] value)
        {
            return builder;
        }

        public static NullLoggingActivityBuilder With(this NullLoggingActivityBuilder builder, string name, Byte value)
        {
            return builder;
        }

        public static NullLoggingActivityBuilder With(this NullLoggingActivityBuilder builder, string name, Byte[] value)
        {
            return builder;
        }

        public static NullLoggingActivityBuilder With(this NullLoggingActivityBuilder builder, string name, Char value)
        {
            return builder;
        }

        public static NullLoggingActivityBuilder With(this NullLoggingActivityBuilder builder, string name, Char[] value)
        {
            return builder;
        }

        public static NullLoggingActivityBuilder With(this NullLoggingActivityBuilder builder, string name, DateTimeOffset value)
        {
            return builder;
        }

        public static NullLoggingActivityBuilder With(this NullLoggingActivityBuilder builder, string name, DateTimeOffset[] value)
        {
            return builder;
        }

        public static NullLoggingActivityBuilder With(this NullLoggingActivityBuilder builder, string name, Rect value)
        {
            return builder;
        }

        public static NullLoggingActivityBuilder With(this NullLoggingActivityBuilder builder, string name, Rect[] value)
        {
            return builder;
        }

        public static NullLoggingActivityBuilder With(this NullLoggingActivityBuilder builder, string name, Point value)
        {
            return builder;
        }

        public static NullLoggingActivityBuilder With(this NullLoggingActivityBuilder builder, string name, Point[] value)
        {
            return builder;
        }

        public static NullLoggingActivityBuilder With(this NullLoggingActivityBuilder builder, string name, Size value)
        {
            return builder;
        }

        public static NullLoggingActivityBuilder With(this NullLoggingActivityBuilder builder, string name, Size[] value)
        {
            return builder;
        }

        public static IDisposable Start(this NullLoggingActivityBuilder builder)
        {
            return builder?.Create();
        }
    }
}