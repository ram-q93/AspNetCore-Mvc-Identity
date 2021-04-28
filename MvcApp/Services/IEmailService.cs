using System.Threading.Tasks;
using MvcApp.DataAccess.Entities;
using MvcApp.Models;

namespace MvcApp.Services
{
    public interface IEmailService
    {

        Task SendEmailForEmailConfirmation(UserEmailOptionsModel userEmailOptions);

        Task SendEmailForForgotPassword(AppUser user, string link);
    }
}