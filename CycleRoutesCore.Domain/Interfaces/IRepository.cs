using System.Threading.Tasks;

namespace CycleRoutesCore.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task Create(T item);
    }
}