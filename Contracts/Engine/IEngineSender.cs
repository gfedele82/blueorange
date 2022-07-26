using Models;
using System.Threading.Tasks;

namespace Contracts.Engine
{
    public interface IEngineSender
    {
        Task<EmailResponse> ProcessSender(Email email);
    }
}
