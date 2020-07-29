namespace Tranlator.ViewModels.Errors
{
    public class ApiError
    {
        public string Err { get; }

        public ApiError(string err)
        {
            Err = err;
        }
    }
}