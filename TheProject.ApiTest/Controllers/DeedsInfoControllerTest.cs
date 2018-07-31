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
    public class DeedsInfoControllerTest
    {
        [TestMethod()]
        public void CreateTest()
        {
            var controller = new DeedsInfoController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            DeedsInfo deedsInfo = new DeedsInfo() {
                Id = 0,
                ErFNumber = "ErF123456",
                TitleDeedNumber= "123456",
                Extent= "Ext 2",
                OwnerInfomation = "Mr P",
                //CreatedDate = DateTime.Now,
                //CreatedUserId = 1,
                //FacilityId = 2
            };
            var result = controller.CreateEdit(deedsInfo);
            Assert.IsTrue(result.IsSuccessStatusCode);
        }
        [TestMethod()]
        public void GetDeedsInfoByIdTest()
        {
            var controller = new DeedsInfoController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();           
            var result = controller.GetDeedsInfoById(1);
            Assert.IsNotNull(result.Id);
        }              

        [TestMethod()]
        public void EditTest()
        {
            var controller = new DeedsInfoController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            DeedsInfo deedsInfo = new DeedsInfo()
            {
                Id = 1,
                ErFNumber = "ErF123456 rerwrewt",
                TitleDeedNumber = "123456",
                Extent = "Test Edit",
                OwnerInfomation = "Mr P Changed",
                //CreatedDate = DateTime.Now,
                //CreatedUserId = 1,
                //ModifiedDate = DateTime.Now,
                //ModifiedUserId = 1,
                //FacilityId = 2
            };
            var result = controller.CreateEdit(deedsInfo);
            Assert.IsTrue(result.IsSuccessStatusCode);
        }
    }
}