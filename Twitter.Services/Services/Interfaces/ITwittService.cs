using Twitter.Service.Models.Twitter;
using System.Threading.Tasks;

namespace Twitter.Service.Services.Interfaces
{
    public interface ITwittService
    {        
        void AddTwittAsync(TwittInfo twitt);
    }
}