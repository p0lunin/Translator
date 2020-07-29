using Tranlator.Services;

namespace TranslatorTests.Infrastructure
{
    class OneValueRandomGeneratorService<T> : IRandomGeneratorService<T>
    {
        private T _value;

        public OneValueRandomGeneratorService(T value)
        {
            _value = value;
        }
        public T Generate()
        {
            return _value;
        }
    }
}