using Getri_TokenAuthentication.Models;

namespace Getri_TokenAuthentication.Repository
{
    public interface IAuthRepository
    {
        TblUser Register(TblUser user);

        TblUser Login(string email);
    }
}
