using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace LudoConsole.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    internal abstract class LudoConsoleBaseException : Exception
    {
        protected LudoConsoleBaseException()
        {
        }

        protected LudoConsoleBaseException(string message) : base(message)
        {
        }

        protected LudoConsoleBaseException(string message, Exception inner) : base(message, inner)
        {
        }

        protected LudoConsoleBaseException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}