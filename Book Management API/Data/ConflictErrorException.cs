﻿using System.Runtime.Serialization;

namespace Book_Management_API.Data
{
    [Serializable]
    public class ConflictErrorException : Exception
    {
        public ConflictErrorException()
        {
        }

        public ConflictErrorException(string? message) : base(message)
        {
        }

        public ConflictErrorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ConflictErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}