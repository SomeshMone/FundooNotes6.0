using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Interfaces;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata.Ecma335;
using BusinessLayer.Services;

namespace FundooNoteApplication6._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _business;
        private readonly IBus bus;
        private readonly ILogger<UserController> logger;
        public UserController(IUserBusiness business,IBus bus,ILogger<UserController> logger)
        {
            this._business = business;
            this.bus = bus;
            this.logger = logger;
        }
       
        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterUser(UserRegistrationModel registrationModel)
        {
            try
            {
                var result = _business.UserRegistration(registrationModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Registration Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registration Failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }


        }

        [HttpPost]
        [Route("Login")]
        public IActionResult LoginUser(UserLoginModel user)
        {
            
            var login = _business.UserLogin(user);  
            //int userid = (int)login;
            if (login != null)
            {
                
                return Ok(new {success=true,message="User logged successfully"});
            }
            return BadRequest("Invalid Credentials");

        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var getAllUsers = _business.GetAllUsers();
                //throw new Exception("Error occured");

                if (getAllUsers != null)
                {
                    return Ok(getAllUsers);
                }
                else
                {
                    
                    return BadRequest("Not found");
                }
                
            }
            catch(Exception ex)
            {
                //logger.LogError(ex.ToString());
                return BadRequest("Notfound");

            }
            

        }

        //Get User Details by UserId

        [HttpGet]
        [Route("GetUserDetailsById")]
        public IActionResult GetUserDetails(long userid)
        {
            var details= _business.GetUserDetails(userid);
            if (details != null)
            {
                return Ok(new { success = true, message = "User Details Obtained", data = details });
            }
            else
            {
                return BadRequest(new { success = false, message = "User id not found", data = details });
            }

        }

        [HttpDelete]
        [Route("Delteuser")]

        public IActionResult DeleteUser(string fanme)
        {
            bool delteUser=_business.Deleteuser(fanme);
            if (delteUser != false)
            {
                return Ok(new { success = true, message = "user is deleted", data = delteUser });
            }
            else
            {
                return BadRequest("With the Name User not Found");
            }
        }

        [HttpPut]
        [Route("UpdateByUserId")]

        public IActionResult UpdateUser(long id, UserUpdateModel user)
        {
            var updateUser=_business.UpdateUserDetails(id, user);
            if(updateUser)
            {
                return Ok(new { success = true, message = "user is updated", data = updateUser });
            }
            else
            {
                return BadRequest("User Id not found and Not Updated User Details");
            }
        }

        [HttpPut]
        [Route("UpdateByUserName")]

        public IActionResult UpdateUserByName(string name,UserUpdateModel user)
        {
            var updateUser=_business.UpdateUserDetialsName(name, user);
            if (updateUser)
            {
                return Ok(new { success = true, message = "user is updated", data = updateUser });
            }
            else
            {
                return BadRequest("User Id not found and Not Updated User Details");
            }
        }


        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string Email)
        {

            var password = _business.ForgotPassword(Email);

            if (password != null)
            {
                Send send = new Send();
                ForgotPasswordModel forgotPasswordModel = _business.ForgotPassword(Email);
                send.SendMail(forgotPasswordModel.Email, forgotPasswordModel.Token);
                Uri uri = new Uri("rabbitmq:://localhost/FunDooNotesEmailQueue");
                var endPoint = await bus.GetSendEndpoint(uri);
                await endPoint.Send(forgotPasswordModel);
                return Ok(new ResponseModel<string> { IsSuccess = true, Message = "Mail sent Successfully", Data = password.Token });
            }
            else
            {
                // Handle the case where password is null
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Email Does not Exist" });
            }


        }

        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordModel reset)
        {
            string Email = User.Claims.FirstOrDefault(x => x.Type == "Email").Value;
            var res = _business.ResetPassword(Email, reset);
            if (res)
            {
                return Ok(new { success = true, message = "Password Reset is done" });

            }
            else
            {
                return BadRequest("Password is not Updated");
            }
        }

        [HttpPost("LoginMethod")]
        public IActionResult LoginMethod(UserLoginModel model)
        {
            var login = _business.LoginMethod(model);
            if(login!=null)
            {
                return Ok(new { success = true, message = "User Login Successful", Data = login });
            }
            else
            {
                return BadRequest(new { success = false, message = "User Login Unsuccessful" });
            }

        }

    }
 }
