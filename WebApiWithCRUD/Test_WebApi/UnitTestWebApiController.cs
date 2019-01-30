using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerInfoDataAccess;
using WebApiWithCRUD.Controllers;
using WebApiWithCRUD.Persistance;

namespace Test_WebApi
{
    [TestClass]
    public class UnitTestWebApiController
    {
        [TestMethod]
        public void TestGetById()
        {
            //setting up pre-requisites <ARRANGE>
            var controller = new ServerDataController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.Request.Headers.Add("refid", "123456");
            controller.Request.Headers.Add("Authorization", "Basic ZGhlZXJhajoxMjM0NTY=");

            //Act on Test <ACT>
            var response = controller.GET(1);

            //Assert the result <ASSERT>
            ServerInfo serverInfo;
            Assert.IsTrue(response.TryGetContentValue<ServerInfo>(out serverInfo));
            Assert.AreEqual(1, serverInfo.server_id);
        }

        [TestMethod]
        public void TestDelete()
        {
            //setting up pre-requisites <ARRANGE>
            var controller = new ServerDataController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.Request.Headers.Add("refid", "123456");
            controller.Request.Headers.Add("Authorization", "Basic ZGhlZXJhajoxMjM0NTY=");

            //Act on Test <ACT>
            var response = controller.DELETE(1);
            
            //Assert the result <ASSERT>
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void TestPost()
        {
            //setting up pre-requisites <ARRANGE>
            var controller = new ServerDataController();
            var serverData =new ServerInfo
            {
                server_id = 999,
                date = "12-12-19",
                loction = "pune",
                name = "dheeraj"
            };
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.Request.Headers.Add("refid", "123456");
            controller.Request.Headers.Add("Authorization", "Basic ZGhlZXJhajoxMjM0NTY=");

            //Act on Test <ACT>
            var response = controller.POST(serverData);

            //Assert the result <ASSERT>
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }



    }
}
