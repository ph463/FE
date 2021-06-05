using System;

namespace FEALibrary.Model
{
    [Serializable]
    public class ModelException : Exception
    {
        public ModelException(string message)
            : base(string.Format("Fehler in Modelldaten " + message)) { }

        public ModelException() { }

        public ModelException(string message, Exception innerException)
            : base(string.Format(message), innerException) { }

        protected ModelException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }
}
