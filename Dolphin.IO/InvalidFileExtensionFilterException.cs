using System;
using System.Runtime.Serialization;

namespace Dolphin.IO
{
    [Serializable]
    public class InvalidFileExtensionFilterException : Exception
    {
        public InvalidFileExtensionFilterException()
        {
        }

        public InvalidFileExtensionFilterException(string message) : base(message)
        {
        }

        public InvalidFileExtensionFilterException(string message, Exception innerException) : base(
            message, innerException)
        {
        }

        protected InvalidFileExtensionFilterException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}