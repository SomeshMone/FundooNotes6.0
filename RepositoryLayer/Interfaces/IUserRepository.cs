using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRepository
    {
        public UserEntity UserRegistration(UserRegistrationModel registartionModel);

        public string UserLogin(UserLoginModel user);

        public object GetAllUsers();
        public object GetUserDetails(long userid);

        public bool Deleteuser(string fname);

        public bool UpdateUserDetails(long userid, UserUpdateModel user);

        public bool UpdateUserDetialsName(string name, UserUpdateModel user);

        public ForgotPasswordModel ForgotPassword(string Email);

        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel);

        public TokenModel LoginMethod(UserLoginModel model);



    }
}
