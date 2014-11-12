using insomnia.Api.Controllers;
using insomnia.Api.Models;
using insomnia.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace insomnia.Test
{
    [TestClass]
    public class Test_Api_MakeRequest
    {
        private static String TESTURL = "http://www.anttieskola.com";
        private static int TESTSTATUSCODE = 200;
        private static DateTime TESTDATE = DateTime.MinValue;
        private static RequestPostModel TESTPOST = new RequestPostModel { Url = TESTURL };
        private static RequestModel TESTMODEL = new RequestModel { Url = TESTURL, StatusCode = TESTSTATUSCODE, Created = TESTDATE, Completed = TESTDATE};

        private static Mock<IRequestEngine> mockEmpty
        {
            get
            {
                Mock<IRequestEngine> m = new Mock<IRequestEngine>();
                m.Setup(r => r.Create(It.IsAny<String>())).Returns(Task.FromResult(200));
                m.Setup(r => r.List()).Returns(new List<RequestModel> {});
                return m;
            }
        }

        private static Mock<IRequestEngine> mockTestModel
        {
            get
            {
                Mock<IRequestEngine> m = new Mock<IRequestEngine>();
                m.Setup(r => r.Create(It.IsAny<String>())).Returns(Task.FromResult(200));
                m.Setup(r => r.List()).Returns(new List<RequestModel> { TESTMODEL });
                return m;
            }
        }

        private static MakeRequestController createAndSetup(Mock<IRequestEngine> e)
        {
            // setup
            var controller = new MakeRequestController(e.Object);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            return controller;
        }

        [TestMethod]
        public void Get()
        {
            MakeRequestController controller = createAndSetup(mockEmpty);

            // 0 requests
            var response = controller.Get();
            RequestListViewModel model;
            Assert.IsTrue(response.TryGetContentValue(out model));
            Assert.AreEqual(0, model.Count);
        }

        [TestMethod]
        public void Post()
        {
            MakeRequestController controller = createAndSetup(mockEmpty);
            RequestPostResponseModel model;

            // simply check return status
            var response = controller.Post(TESTPOST).Result;
            Assert.IsTrue(response.TryGetContentValue(out model));
            Assert.AreEqual(TESTSTATUSCODE, model.StatusCode);
            Assert.IsTrue(model.Success);

            // null input
            response = controller.Post(null).Result;
            Assert.IsTrue(response.TryGetContentValue(out model));
            Assert.IsFalse(model.Success);
            Assert.IsFalse(model.Error.Length == 0);

            // null url
            response = controller.Post(new RequestPostModel()).Result;
            Assert.IsTrue(response.TryGetContentValue(out model));
            Assert.IsFalse(model.Success);
            Assert.IsFalse(model.Error.Length == 0);
        }

        [TestMethod]
        public void NormalUseCase()
        {
            MakeRequestController controller = createAndSetup(mockTestModel);
            RequestPostResponseModel model;

            // create request and check it is in the list
            var response = controller.Post(TESTPOST).Result;
            Assert.IsTrue(response.TryGetContentValue(out model));
            Assert.AreEqual(TESTSTATUSCODE, model.StatusCode);
            Assert.IsTrue(model.Success);

            response = controller.Get();
            RequestListViewModel listModel;
            Assert.IsTrue(response.TryGetContentValue(out listModel));
            Assert.AreEqual(1, listModel.Count);
            RequestModel rm = listModel.Requests.First();
            Assert.AreEqual(TESTURL, rm.Url);
            Assert.AreEqual(TESTSTATUSCODE, rm.StatusCode);
            Assert.AreEqual(TESTDATE, rm.Created);
            Assert.AreEqual(TESTDATE, rm.Completed);
        }
    }
}
