using Ordinacija.Features.Login.Models;

namespace Ordinacija.Features.Login.Repository
{
    public interface ILoginRepository
    {
        Task<bool> AuthenticateUser(LoginCredentials loginCredentials);
    }
}
