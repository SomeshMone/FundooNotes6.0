using BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using CommonLayer.Model;

namespace BusinessLayer.Services
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository iuserr;
        public UserBusiness(IUserRepository iuserr)
        {
            this.iuserr = iuserr;
        }

        public UserEntity UserRegistration(UserRegistrationModel registrationModel)
        {
            try
            {
                return iuserr.UserRegistration(registrationModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string UserLogin(UserLoginModel user)
        {
            try
            {
                return iuserr.UserLogin(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object GetAllUsers()
        {
            return iuserr.GetAllUsers();
        }

        public object GetUserDetails(long userid)
        {
            return iuserr.GetUserDetails(userid);
        }

        public bool Deleteuser(string fname)
        {
            return iuserr.Deleteuser(fname);
        }

        public bool UpdateUserDetails(long userid, UserUpdateModel user)
        {
            return iuserr.UpdateUserDetails(userid, user);
        }


        public bool UpdateUserDetialsName(string name, UserUpdateModel user)
        {
            return iuserr.UpdateUserDetialsName(name, user);
        }

        public ForgotPasswordModel ForgotPassword(string Email)
        {
            return iuserr.ForgotPassword(Email);
        }

        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel)
        {
            return iuserr.ResetPassword(Email, resetPasswordModel);
        }

        
    }

    
}
