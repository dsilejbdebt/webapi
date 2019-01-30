using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Web.Http;
using WebApiWithCRUD.Controllers;
using ServerInfoDataAccess;
using WebApiWithCRUD.Persistance;

namespace UnitTestWebApiWithCRUD
{
    [TestClass]
    public class UnitTestWebApiController
    {
        [TestMethod]
        public void ServerInfoGETMethod()
        {
            //setting up pre-requisites <ARRANGE>
            var controller = new ServerDataController();
            IServerDataRepository serverData=new ServerDataRepository();
                controller.Request = new HttpRequestMessage();
                controller.Configuration = new HttpConfiguration();
                //controller.Request.Headers.Add("refid", "123456");
                //controller.Request.Headers.Add("Authorization", "Basic ZGhlZXJhajoxMjM0NTY=");

            //Act on Test <ACT>
            var response = controller.GET(1);
            var result = serverData.GetAll();

            //Assert the result <ASSERT>
            ServerInfo serverInfo;
            Assert.IsTrue(response.TryGetContentValue<ServerInfo>(out serverInfo));
            Assert.AreEqual(1, serverInfo.server_id);
            
           
        }
    }
}
