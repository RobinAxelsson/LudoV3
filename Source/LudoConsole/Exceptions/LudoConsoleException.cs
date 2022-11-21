using System;
using System.Diagnostics.CodeAnalysis;

namespace LudoConsole.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    internal class LudoConsoleException : Exception
    {
        public LudoConsoleException() { }
        public LudoConsoleException(string message) : base(message) { }
        public LudoConsoleException(string message, Exception inner) : base(message, inner) { }
        public LudoConsoleException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}