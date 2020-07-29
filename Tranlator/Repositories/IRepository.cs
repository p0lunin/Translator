using System.Threading.Tasks;

namespace Tranlator.Repositories
{
    public interface IRepository
    {
        public Task SaveChanges();
    }
}