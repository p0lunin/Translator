using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Tranlator.Models;
using File = Tranlator.Models.File;

namespace Tranlator.Services
{
    public class FileConverterService : IFileConverterService
    {
        public async Task<File> FromFileToModel(IFormFile file)
        {
            var content = await ReadAsStringAsync(file);
            var paragraphs = content
                .Split(new[] {Environment.NewLine}, StringSplitOptions.None)
                .Select((s, counter) =>
                {
                    var res = new Paragraph { Content = s, Order = counter };
                    return res;
                })
                .ToList();
            return new File { Name = file.Name, Paragraphs = paragraphs};
        }
        private static async Task<string> ReadAsStringAsync(IFormFile file)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(await reader.ReadLineAsync()); 
            }
            return result.ToString();
        }
    }
}