using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Tranlator.Models;

namespace Tranlator.Services
{
    public interface IFileConverterService
    {
        public Task<File> FromFileToModel(IFormFile file);
    }
}