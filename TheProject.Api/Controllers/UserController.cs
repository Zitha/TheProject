using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data.Entity;
using System.Web.Http;
using TheProject.Data;
using TheProject.Model;
using TheProject.Api.Models;
using System.Threading.Tasks;

namespace TheProject.Api.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        public User Login(string username, string password)
        {
            try
            {
                using (ApplicationUnit unit = new ApplicationUnit())
                {
                    User loginUser = unit.Users.GetAll()
                        .FirstOrDefault(usr => usr.Username.ToLower() == username.ToLower());

                    if (loginUser != null)
                    {
                        string decriptedPassword = EncryptString.Decrypt(loginUser.Password, "THEPROJECT");
                        if (decriptedPassword == password)
                        {
                            Task addTask = new Task(() => LogAuditTrail("Login", string.Format("Successful Login {0}", loginUser.Username), loginUser.Id, loginUser.Id));
                            addTask.Start();

                            return loginUser;
                        }
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.LogError(ex.StackTrace, "AddBuilding");
                throw ex;
            }
          
        }

        private void LogAuditTrail(string section, string type, int userId, int itemId)
        {
            try
            {
                using (ApplicationUnit unit = new ApplicationUnit())
                {
                    Audit audit = new Audit
                    {
                        ChangeDate = DateTime.Now,
                        ItemId = itemId,
                        Section = section,
                        UserId = userId,
                        Type = type
                    };
                    unit.Audits.Add(audit);
                    unit.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public bool ResertPassword(string username)
        {
            using (ApplicationUnit unit = new ApplicationUnit())
            {
                User foundUser = unit.Users.GetAll().FirstOrDefault(user => user.Username.ToLower() == username.ToLower());

                if (foundUser != null)
                {
                    string decriptedPassword = EncryptString.Decrypt(foundUser.Password, "THEPROJECT");
                    EmailService emailService = new EmailService();

                    //Send Email with Invoice
                    Task sendEmailTask = new Task(() => emailService.SendResertPasswordEmail(foundUser.Email, decriptedPassword));
                    // Start the task.
                    sendEmailTask.Start();

                    return true;
                }
                return false;
            }
        }

        [HttpPost]
        public User AddUser(User user)
        {
            using (ApplicationUnit unit = new ApplicationUnit())
            {
                User loginUser = unit.Users.GetAll()
                    .FirstOrDefault(usr => usr.Username.ToLower() == user.Username.ToLower());

                if (loginUser == null)
                {
                    string password = EncryptString.Encrypt(user.Password, "THEPROJECT");
                    user.Password = password;
                    user.CreatedDate = DateTime.Now;
                    user.ModifiedDate = DateTime.Now;
                    unit.Users.Add(user);
                    unit.SaveChanges();
                    return user;
                }
                return null;
            }
        }

        [HttpGet]
        public List<User> GetUsers()
        {
            using (ApplicationUnit unit = new ApplicationUnit())
            {
                List<User> users = unit.Users.GetAll().ToList();
                return users;
            }
        }

    }
}
