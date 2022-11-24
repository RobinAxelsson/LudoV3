using System;
using System.Diagnostics.CodeAnalysis;

namespace LudoConsole.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    internal class LudoConsoleWindowOutOfRangeException : LudoConsoleBaseException
    {
        public LudoConsoleWindowOutOfRangeException() { }
        public LudoConsoleWindowOutOfRangeException(string message) : base(message) { }
        public LudoConsoleWindowOutOfRangeException(string message, Exception inner) : base(message, inner) { }
        public LudoConsoleWindowOutOfRangeException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}