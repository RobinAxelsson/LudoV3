using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace LudoEngine.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    internal abstract class LudoEngineBaseException : Exception
    {
        protected LudoEngineBaseException()
        {
        }

        protected LudoEngineBaseException(string message) : base(message)
        {
        }

        protected LudoEngineBaseException(string message, Exception inner) : base(message, inner)
        {
        }

        protected LudoEngineBaseException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}