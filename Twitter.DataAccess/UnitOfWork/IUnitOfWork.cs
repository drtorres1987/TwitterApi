using System.Threading.Tasks;

namespace Twitter.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CommitChangesAsync();
    }
}