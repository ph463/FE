using System;

namespace FEALibrary.Model
{
    public class AnalysisException : Exception
    {
        public AnalysisException(string message)
            : base(string.Format("Analysis: Fehler in der Berechnung " + message)) { }

        public AnalysisException() { }

        public AnalysisException(string message, Exception innerException)
            : base(string.Format(message), innerException) { }

        protected AnalysisException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }
}
