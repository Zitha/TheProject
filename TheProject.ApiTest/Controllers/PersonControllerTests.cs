using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheProject.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;
using TheProject.Model;

namespace TheProject.Api.Controllers.Tests
{
    [TestClass()]
    public class PersonControllerTests
    {        

        [TestMethod()]
        public void CreateTest()
        {
            var controller = new PersonController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            Person person = new Person()
            {
                Id = 0,
                FullName = "Person Name",
                Designation = "",
                PhoneNumber = "01245637895",
                EmailAddress = "g@g.com",                
                //CreatedDate = DateTime.Now,
                //CreatedUserId = 1,
                //FacilityId = 2
            };
            var result = controller.CreateEdit(person);
            Assert.IsTrue(result.IsSuccessStatusCode);
        }

        [TestMethod()]
        public void EditTest()
        {
            var controller = new PersonController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            Person person = new Person()
            {
                Id = 1,
                FullName = "Mr L Naina22 3",
                Designation = "Principal",
                PhoneNumber = "01245637895",
                EmailAddress = "imaila@dalparkprivate.co.za",
                //CreatedDate = DateTime.Now,
                //CreatedUserId = 1,
                //ModifiedDate = DateTime.Now,
                //ModifiedUserId = 1,
                //FacilityId = 2
            };
            var result = controller.CreateEdit(person);
            Assert.IsTrue(result.IsSuccessStatusCode);
        }

        [TestMethod()]
        public void GetPersonByIdTest()
        {
            var controller = new PersonController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var result = controller.GetPersonById(1);
            Assert.IsNotNull(result.Id);
        }
    }
}