using CommonLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace RepositoryLayer.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration _config;

        public UserRepository(FundooContext fundooContext, IConfiguration _config)
        {
            this.fundooContext = fundooContext;
            this._config = _config;

        }

        public static string EncodePassword(string password)
        {
            var encodedData = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(encodedData);
        }

        public static string DecodePassword(string password)
        {
            try
            {
                
                return Encoding.UTF8.GetString(Convert.FromBase64String(password));
            }
            catch (FormatException ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine("Error decoding password: " + ex.Message);
                // Return a default value or throw a custom exception
                return null; // or return a default password
            }
        }

        //public static string DecodePassword(string password)
        //{
        //    var decodeData = Convert.FromBase64String(password);
        //    return Encoding.UTF8.GetString(decodeData);
        //}

        private string GenerateToken(long UserId, string userEmail)
        {
            // Create a symmetric security key using the JWT key specified in the configuration
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            // Create signing credentials using the security key and HMAC-SHA256 algorithm
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // Define claims to be included in the JWT
            var claims = new[]
            {
                new Claim("Email",userEmail),
                new Claim("UserId", UserId.ToString())
            };
            // Create a JWT with specified issuer, audience, claims, expiration time, and signing credentials
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public UserEntity UserRegistration(UserRegistrationModel registrationModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = registrationModel.FirstName;
                userEntity.LastName = registrationModel.LastName;
                userEntity.Email = registrationModel.Email;

                userEntity.Password = EncodePassword(registrationModel.Password);

                fundooContext.UsersTable1.Add(userEntity);
                fundooContext.SaveChanges();
                return userEntity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string UserLogin(UserLoginModel user)
        {
            var userLogin = fundooContext.UsersTable1.FirstOrDefault(x => x.Email == user.Email);

            // Check if userLogin is null
            if (userLogin != null)
            {
                string userpass = DecodePassword(userLogin.Password);

                // Check if user email and password match
                if (userLogin.Email.Equals(user.Email) && userpass.Equals(user.Password))
                {
                    // Generate token and return it
                    var token = GenerateToken(userLogin.UserId, user.Email);
                    return token;
                }
            }

            // If userLogin is null or email/password don't match, return appropriate message
            return "Not Found...";
        }

        public object GetAllUsers()
        {
            var data=fundooContext.UsersTable1.ToList();
            if (data != null)
            {
                return data;

            }
            else
            {
                return null;
            }
        }

        //get user information by userid

        public object GetUserDetails(long userid)
        {
            var userData=fundooContext.UsersTable1.FirstOrDefault(x=>x.UserId == userid);
            if(userData != null)
            {
                return userData;
            }
            return null;
        }


        // Delete user Details by his first Name

        public bool Deleteuser(string fname)
        {
            var delete=fundooContext.UsersTable1.FirstOrDefault(x=>x.FirstName==fname);
            if (delete != null)
            {
                fundooContext.UsersTable1.Remove(delete);
                fundooContext.SaveChanges();
                return true;
            }
            return false;
        }

        //Update User Details

        public bool UpdateUserDetails(long userid, UserUpdateModel user)
        {
            var updateuser=fundooContext.UsersTable1.FirstOrDefault(x=>x.UserId==userid);
            if(updateuser != null)
            {
                updateuser.FirstName = user.FirstName;
                updateuser.LastName = user.LastName;
                updateuser.Email = user.Email;
                fundooContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }


        //Update by UserDetails with firstName

        public bool UpdateUserDetialsName(string name,UserUpdateModel user)
        {
            var updateuser=fundooContext.UsersTable1.FirstOrDefault(x=>x.FirstName==name);
            if(updateuser != null)
            {
                updateuser.FirstName = user.FirstName;
                updateuser.LastName = user.LastName;
                updateuser.Email = user.Email;
                fundooContext.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }
        }
        

    // Forget Password

        public ForgotPasswordModel ForgotPassword(string Email)
        {
            UserEntity User = fundooContext.UsersTable1.ToList().Find(user => user.Email == Email);
            ForgotPasswordModel forgotPassword = new ForgotPasswordModel();
            forgotPassword.Email = User.Email;
            forgotPassword.UserId = User.UserId;
            forgotPassword.Token = GenerateToken(User.UserId, User.Email);
            return forgotPassword;
        }


        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel)
        {
            UserEntity User = fundooContext.UsersTable1.ToList().Find(x => x.Email == Email);
            if (User != null)
            {
                User.Password = EncodePassword(resetPasswordModel.ConfirmPassword);
                //User.ChangedAt = DateTime.Now;
                fundooContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
    
