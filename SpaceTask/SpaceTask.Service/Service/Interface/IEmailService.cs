using SpaceTask.Data.DataModels;
using System.Threading.Tasks;

namespace SpaceTask.Service.Service.Interface
{
    public interface IEmailService
    {
        public Task SendEmailAsync(EmailInfo emailInfo);
    }
}
