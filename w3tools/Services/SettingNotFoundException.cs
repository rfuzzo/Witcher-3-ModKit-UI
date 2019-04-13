using System;
using System.Runtime.Serialization;

namespace w3tools.Services
{
    [Serializable]
    internal class SettingNotFoundException : Exception
    {
        public SettingNotFoundException()
        {
        }

        public SettingNotFoundException(string message) : base(message)
        {
        }

        public SettingNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SettingNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}