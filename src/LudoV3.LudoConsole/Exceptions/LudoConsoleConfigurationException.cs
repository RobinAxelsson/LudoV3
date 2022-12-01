using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace LudoConsole.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    internal class LudoConsoleConfigurationException : LudoConsoleBaseException
    {
        public LudoConsoleConfigurationException()
        {
        }

        public LudoConsoleConfigurationException(string message) : base(message)
        {
        }

        public LudoConsoleConfigurationException(string message, Exception inner) : base(message, inner)
        {
        }

        public LudoConsoleConfigurationException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}