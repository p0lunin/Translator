using System.Collections.Generic;
using System.Threading.Tasks;
using Tranlator.Models;

namespace Tranlator.Repositories
{
    public interface IProjectsRepository : IRepository
    {
        public Task<Project> CreateProject(User owner, string name);
        public Task<List<Project>> GetUserProjects(string username);
    }
}