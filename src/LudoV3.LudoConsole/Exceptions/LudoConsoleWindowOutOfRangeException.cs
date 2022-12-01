using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace LudoConsole.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    internal class LudoConsoleWindowOutOfRangeException : LudoConsoleBaseException
    {
        public LudoConsoleWindowOutOfRangeException()
        {
        }

        public LudoConsoleWindowOutOfRangeException(string message) : base(message)
        {
        }

        public LudoConsoleWindowOutOfRangeException(string message, Exception inner) : base(message, inner)
        {
        }

        public LudoConsoleWindowOutOfRangeException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}