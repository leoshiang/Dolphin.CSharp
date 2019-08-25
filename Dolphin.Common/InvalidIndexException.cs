using System;
using System.Runtime.Serialization;

namespace Dolphin.Common
{
    [Serializable]
    public class InvalidIndexException : Exception
    {
        public InvalidIndexException()
        {
        }

        public InvalidIndexException(string message) : base(message)
        {
        }

        public InvalidIndexException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidIndexException(SerializationInfo info, StreamingContext context) : base(
            info, context)
        {
        }
    }
}