using System;

namespace FEALibrary.Utils
{
    [Serializable]
    public class AlgebraicException : Exception
    {
        public AlgebraicException(string message)
            : base(string.Format("Fehler in MatrixAlgebra " + message)) { }

        public AlgebraicException() { }

        public AlgebraicException(string message, Exception innerException)
            : base(string.Format(message), innerException) { }

        protected AlgebraicException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }
}
