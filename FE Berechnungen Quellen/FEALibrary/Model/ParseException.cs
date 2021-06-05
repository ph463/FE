﻿using System;

namespace FEALibrary.Model
{
    [Serializable]
    public class ParseException : Exception
    {
        public ParseException(string message)
            : base(string.Format("Fehler in Eingabezeile " + message)) { }

        public ParseException() { }

        public ParseException(string message, Exception innerException)
            : base(string.Format(message), innerException) { }

        protected ParseException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }
}
