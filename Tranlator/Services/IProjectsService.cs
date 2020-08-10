using System.Collections.Generic;
using System.Threading.Tasks;
using Tranlator.Models;
using Tranlator.ViewModels;

namespace Tranlator.Services
{
    public interface IProjectsService
    {
        public Task<ProjectInfoViewModel> CreateNewProject(User owner, string name);
        public Task<List<ProjectInfoViewModel>> UserProjects(int userId);
        public Task<List<LangInfoViewModel>> ProjectLangs(int projectId);
        public Task<List<FileInfoViewModel>> LangFiles(int langId);
        public Task<List<ParagraphInfoViewModel>> FileParagraphs(int fileId);
        public Task UpdateParagraph(int userId, int paragraphId, string newContent);
    }
}