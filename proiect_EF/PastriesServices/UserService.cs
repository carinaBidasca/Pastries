using PastriesCommon;
using PastriesCommon.Entities;
using PastriesInterfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using PastriesCommon.Util;
using PastriesData;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PastriesServices
{
    /// <summary>
    /// Manage the users for the API.
    /// </summary>
    public class UserService : IUserService
    {
       // private List<User> _users = new List<User>();
        private readonly AppSettings _appSettings;
        private readonly byte[] _salt;//pt hash
        private readonly PastriesDbContext _context;
        public UserService(IOptions<AppSettings> appSettings,PastriesDbContext pastriesDbContext)
        {
            _context = pastriesDbContext;
            _appSettings = appSettings.Value;
            
            _salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(_salt);
            }
            
            //_users.Add(new User { Id = 1, FirstName = "ana", LastName = "Pop", Username = "ana_pop", PasswordHash = HashPassword("parola1") });
            //_users.Add(new User { Id = 2, FirstName = "maria", LastName = "Popescu", Username = "maria_popescu", PasswordHash = HashPassword("parola2")});
        }
      

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var result = await _context.Users
                .ToListAsync();

            var entities = _context.ChangeTracker.Entries();

            return result;
        }

        public async Task<User> GetById(int id)
        {
            var result = await _context.Users
                           .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }
        private string HashPassword(string password)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: _salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }
        /// <summary>
        /// Generate a token that is valid for a pre-defined number of minutes.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GenerateJwtToken(User user)
        {
            
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("sub", user.Username.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        //sa o fac async
        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
           //  Console.WriteLine("HASH!!!!");
           // Console.WriteLine(HashPassword("anapop"));
            //var user = _users.SingleOrDefault(x => x.Username == model.Username && x.PasswordHash == HashPassword(model.Password));
            //var user = _context.Users.FirstOrDefault(x => x.Username == model.Username && x.PasswordHash == HashPassword(model.Password)); nu face ok verif la parola
            var user = _context.Users.FirstOrDefault(x => x.Username.Equals(model.Username));
            //!!!!!!!!!!!!!sa pun hash ul corect in bd,dupa revin aici



            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public User GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault(x => x.Username.Equals(username));
        }
        

        
    }
}
