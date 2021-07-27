using PastriesCommon.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PastriesInterfaces
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetById(int id);
        User GetByUsername(string username);
        public string GenerateJwtToken(User user);
    }
}
