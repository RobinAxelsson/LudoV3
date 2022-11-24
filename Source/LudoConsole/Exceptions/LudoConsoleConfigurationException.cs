using System;
using System.Diagnostics.CodeAnalysis;

namespace LudoConsole.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    internal class LudoConsoleConfigurationException : LudoConsoleBaseException
    {
        public LudoConsoleConfigurationException() { }
        public LudoConsoleConfigurationException(string message) : base(message) { }
        public LudoConsoleConfigurationException(string message, Exception inner) : base(message, inner) { }
        public LudoConsoleConfigurationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}