using System;

namespace FEALibrary.EquationSolver
{
    [Serializable]
    public class AlgebraicException : Exception
    {
        public AlgebraicException(string message)
            : base(string.Format("Error in EquationSolver " + message)) { }

        public AlgebraicException() { }

        public AlgebraicException(string message, Exception innerException)
            : base(string.Format(message), innerException) { }

        protected AlgebraicException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }
}
