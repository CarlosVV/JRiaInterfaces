using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Infrastructure.Core.Security
{
    public interface IAuthenticationService
    {
        User AuthenticateUser(string username, string password);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private IUserService _userService;

        public AuthenticationService(IUserService userService)
        {
            _userService = userService;
        }

        public User AuthenticateUser(string username, string clearTextPassword)
        {
            //var hash = CalculateHash(clearTextPassword, username);
            var userData = _userService.GetAllUsers().FirstOrDefault(u => u.Name.Equals(username)
                && u.Password.Equals(CalculateHash(clearTextPassword, u.Name)));

            if (userData == null)
                throw new UnauthorizedAccessException("Access denied. Please provide some valid credentials.");

            return new User(userData.Name, userData.Email, userData.UserRoles.Select(u=> u.Role.Name).ToArray());
        }

        private string CalculateHash(string clearTextPassword, string salt)
        {
            // Convert the salted password to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);
            // Use the hash algorithm to calculate the hash
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // Return the hash as a base64 encoded string to be compared to the stored password
            return Convert.ToBase64String(hash);
        }
    }   
}
