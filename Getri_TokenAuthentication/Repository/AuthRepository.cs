using Getri_TokenAuthentication.EntityFramework;
using Getri_TokenAuthentication.Models;

namespace Getri_TokenAuthentication.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly GetriTokenAuthenticationContext dbContext;

        public AuthRepository(GetriTokenAuthenticationContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public TblUser Login(string email)
        {
            var user = dbContext.TblUser.FirstOrDefault(x => x.Email == email);
            if (user == null)
                return null;


            return user; // auth successful
        }

        public TblUser Register(TblUser user)
        {
            dbContext.TblUser.Add(user);
            dbContext.SaveChanges();

            return user;
        }
    }
}
