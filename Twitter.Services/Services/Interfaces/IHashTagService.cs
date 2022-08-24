using Twitter.Service.Models.Report;
using System.Threading.Tasks;

namespace Twitter.Service.Services.Interfaces
{
    public interface IHashTagService
    {
        /// <summary>
        /// Process HashTag data.
        /// </summary>
        /// <param name="topN">Top (N) hastags in the system.</param>
        /// <returns>The Hashtag Report</returns>
        public Task<HashTagReport> GetHashTags(int topN);
    }
}