using System;
using System.Runtime.Serialization;

namespace Dolphin.IO
{
    [Serializable]
    public class InvalidDirectoryFilterException : Exception
    {
        public InvalidDirectoryFilterException()
        {
        }

        public InvalidDirectoryFilterException(string message) : base(message)
        {
        }

        public InvalidDirectoryFilterException(string message, Exception innerException) : base(
            message, innerException)
        {
        }

        protected InvalidDirectoryFilterException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}