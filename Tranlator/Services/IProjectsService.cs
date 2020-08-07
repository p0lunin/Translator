using System.Collections.Generic;
using System.Threading.Tasks;
using Tranlator.Models;

namespace Tranlator.Services
{
    public interface IProjectsService
    {
        public Task<Project> CreateNewProject(User owner, string name);
        public Task<List<Project>> UserProjects(string username);
        public Task UpdateParagraph(int paragraphId, string newContent);
    }
}