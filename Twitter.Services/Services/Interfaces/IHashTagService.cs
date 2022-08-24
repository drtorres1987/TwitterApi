using Twitter.Service.Models.Report;
using System.Threading.Tasks;

namespace Twitter.Service.Services.Interfaces
{
    public interface IHashTagService
    {        
        public Task<HashTagReport> GetHashTags(int number);
    }
}