using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace LudoEngine.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    internal class NoPlayersException : Exception
    {
        public NoPlayersException()
        {
        }

        public NoPlayersException(string message) : base(message)
        {
        }

        public NoPlayersException(string message, Exception inner) : base(message, inner)
        {
        }

        public NoPlayersException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}