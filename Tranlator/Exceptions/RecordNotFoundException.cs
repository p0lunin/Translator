using System;

namespace Tranlator.Exceptions
{
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException(string message) : base(message) {}
    }
}