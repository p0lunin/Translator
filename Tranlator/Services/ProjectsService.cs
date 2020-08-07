using System.Collections.Generic;
using System.Threading.Tasks;
using Tranlator.Models;
using Tranlator.Repositories;

namespace Tranlator.Services
{
    public class ProjectsService: IProjectsService
    {
        private readonly IProjectsRepository _projectsRepository;

        public ProjectsService(IProjectsRepository projectsRepository)
        {
            _projectsRepository = projectsRepository;
        }
        
        public async Task<Project> CreateNewProject(User owner, string name)
        {
            var proj = await _projectsRepository.CreateProject(owner, name);
            await _projectsRepository.SaveChanges();
            return proj;
        }

        public async Task<List<Project>> UserProjects(string username)
        {
            return await _projectsRepository.GetUserProjects(username);
        }

        public async Task UpdateParagraph(int paragraphId, string newContent)
        {
            await _projectsRepository.UpdateParagraph(paragraphId, newContent);
            await _projectsRepository.SaveChanges();
        }
    }
}