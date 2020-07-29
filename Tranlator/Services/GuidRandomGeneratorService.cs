using System;

namespace Tranlator.Services
{
    public class GuidRandomGeneratorService : IRandomGeneratorService<string>
    {
        public string Generate()
        {
            return Guid.NewGuid().ToString();
        }
    }
}