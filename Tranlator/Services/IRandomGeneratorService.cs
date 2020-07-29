namespace Tranlator.Services
{
    public interface IRandomGeneratorService<T>
    {
        public T Generate();
    }
}