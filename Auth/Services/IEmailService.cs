using Auth.DataAccess.Entities;
using Auth.Models;
using System.Threading.Tasks;

namespace Auth.Services
{
    public interface IEmailService
    {

        Task SendEmailForEmailConfirmation(UserEmailOptionsModel userEmailOptions);

        Task SendEmailForForgotPassword(AppUser user, string link);
    }
}