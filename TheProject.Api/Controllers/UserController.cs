using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data.Entity;
using System.Web.Http;
using TheProject.Data;
using TheProject.Model;

namespace TheProject.Api.Controllers
{
    public class UserController : ApiController
    {
        [HttpPut]
        public User Login(User user)
        {
            using (ApplicationUnit unit = new ApplicationUnit())
            {
                User loginUser = unit.Users.GetAll()
                    .FirstOrDefault(usr => usr.Username.ToLower() == user.Username.ToLower() &&
                     user.Password == usr.Password);

                if (loginUser != null)
                {
                    return loginUser;
                }
                return null;
            }
        }

    }
}
