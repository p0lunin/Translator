using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tranlator.Exceptions;
using Tranlator.Models;
using Tranlator.Repositories;
using Tranlator.ViewModels;

namespace Tranlator.Services
{
    public class ProjectsService: IProjectsService
    {
        private readonly IProjectsRepository _projectsRepository;

        public ProjectsService(IProjectsRepository projectsRepository)
        {
            _projectsRepository = projectsRepository;
        }
        
        public async Task<ProjectInfoViewModel> CreateNewProject(User owner, string name, string mainLangName)
        {
            var proj = await _projectsRepository.CreateProject(owner, name, new Lang { Name = mainLangName });
            await _projectsRepository.SaveChanges();
            return ProjectToViewModel(proj);
        }

        public async Task<List<ProjectInfoViewModel>> UserProjects(int userId)
        {
            return (await _projectsRepository.GetUserProjects(userId)).Select(ProjectToViewModel).ToList();
        }

        public async Task<List<LangInfoViewModel>> ProjectLangs(int projectId)
        {
            return (await _projectsRepository.GetProjectLangs(projectId)).Select(LangToViewModel).ToList();
        }

        public async Task<List<FileInfoViewModel>> LangFiles(int langId)
        {
            return (await _projectsRepository.GetLangFiles(langId)).Select(FileToViewModel).ToList();
        }

        public async Task<List<ParagraphInfoViewModel>> FileParagraphs(int fileId)
        {
            return (await _projectsRepository.GetFileParagraphs(fileId)).Select(ParagraphToViewModel).ToList();
        }

        public async Task UpdateParagraph(int userId, int paragraphId, string newContent)
        {
            await AssertUserIsAuthorOfParagraph(userId, paragraphId);
            await _projectsRepository.UpdateParagraph(paragraphId, newContent);
            await _projectsRepository.SaveChanges();
        }

        private async Task AssertUserIsAuthorOfParagraph(int userId, int paragraphId)
        {
            var author = await _projectsRepository.GetParagraphAuthor(paragraphId);
            if (author.Id != userId)
            {
                throw new RecordNotFoundException("paragraph");
            }
        }

        private static ProjectInfoViewModel ProjectToViewModel(Project project)
        {
            return new ProjectInfoViewModel(project.Id, project.Name, project.IsPublic);
        }
        private static LangInfoViewModel LangToViewModel(Lang lang)
        {
            return new LangInfoViewModel(lang.Id, lang.Name);
        }
        private static FileInfoViewModel FileToViewModel(File file)
        {
            return new FileInfoViewModel(file.Id, file.Name);
        }
        private static ParagraphInfoViewModel ParagraphToViewModel(Paragraph paragraph)
        {
            return new ParagraphInfoViewModel(paragraph.Id, paragraph.Order, paragraph.Content);
        }
    }
}