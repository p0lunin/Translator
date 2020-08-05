using System;
using Microsoft.Extensions.Configuration;

namespace Tranlator
{
    public class Settings
    {
        public string Host { get; }

        public Settings(string host)
        {
            Host = host;
        }

        public Settings()
        {
            Host = null;
        }
    }
}