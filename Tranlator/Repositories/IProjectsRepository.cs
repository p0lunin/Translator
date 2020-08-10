using System.Collections.Generic;
using System.Threading.Tasks;
using Tranlator.Models;

namespace Tranlator.Repositories
{
    public interface IProjectsRepository : IRepository
    {
        public Task<Project> CreateProject(User owner, string name, Lang mainLang);
        public Task<List<Project>> GetUserProjects(int userId);
        public Task<List<Lang>> GetProjectLangs(int projectId);
        public Task<List<File>> GetLangFiles(int langId);
        public Task<List<Paragraph>> GetFileParagraphs(int fileId);
        public Task UpdateParagraph(int paragraphId, string newContent);
        public Task<User> GetParagraphAuthor(int paragraphId);
        public Task AddFile(int langId, File file);
        public Task AddLang(int projectId, Lang lang);
        public Task AddParagraph(int fileId, Paragraph paragraph);
    }
}