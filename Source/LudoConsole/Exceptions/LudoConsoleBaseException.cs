using System;
using System.Diagnostics.CodeAnalysis;

namespace LudoConsole.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    internal abstract class LudoConsoleBaseException : Exception
    {
        protected LudoConsoleBaseException() { }
        protected LudoConsoleBaseException(string message) : base(message) { }
        protected LudoConsoleBaseException(string message, Exception inner) : base(message, inner) { }
        protected LudoConsoleBaseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}