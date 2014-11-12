﻿using insomnia.Api.Controllers;
using insomnia.Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        private RequestPostModel TESTURI = new RequestPostModel { Url = "http://www.anttieskola.com" };

        private static MakeRequestController createAndSetup()
        {
            // setup
            var controller = new MakeRequestController();
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            return controller;
        }

        [TestMethod]
        public void Get()
        {
            MakeRequestController controller = createAndSetup();

            // 0 requests
            var response = controller.Get();
            RequestListModel model;
            Assert.IsTrue(response.TryGetContentValue(out model));
            Assert.AreEqual(0, model.Count);
        }

        [TestMethod]
        public void Post()
        {
            MakeRequestController controller = createAndSetup();

            // simply check return status
            var response = controller.Post(TESTURI);
            Assert.AreEqual(HttpStatusCode.Accepted, response.StatusCode);

            // null input
            response = controller.Post(null);
            Assert.AreEqual((HttpStatusCode)422, response.StatusCode);

            // null url
            response = controller.Post(new RequestPostModel());
            Assert.AreEqual((HttpStatusCode)422, response.StatusCode);
        }

        [TestMethod]
        public void Usage()
        {
            MakeRequestController controller = createAndSetup();

            // create request and check it is in the list
            DateTime currentTime = DateTime.Now;
            var response = controller.Post(TESTURI);
            Assert.AreEqual(HttpStatusCode.Accepted, response.StatusCode);

            response = controller.Get();
            RequestListModel listModel;
            Assert.IsTrue(response.TryGetContentValue(out listModel));
            Assert.AreEqual(1, listModel.Count);
            RequestModel model = listModel.Requests.First();
            Assert.AreEqual(TESTURI.Url, model.Url);
            long ticksToCreate = model.Created.Ticks - currentTime.Ticks;
            // request was made in the last 5 seconds
            Assert.IsTrue(ticksToCreate < 50000000 && ticksToCreate > -1);
        }
    }
}
