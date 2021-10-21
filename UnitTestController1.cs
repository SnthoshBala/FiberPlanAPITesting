using NUnit.Framework;
using FiberPlanAPI.Controllers;
using FiberPlanAPI.Service;
using FiberPlanAPI.FiberConnection;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq;

namespace FiberPlanAPITest
{
    [TestFixture]
    public class Tests
    {
        public fiber_connectionContext fcc;
        public Mock<IFiberPlanServ<FiberPlan>> frp;
        public FiberController con;
        public FiberPlan fp;
        [SetUp]
        public void Setup()
        {
            fcc = new fiber_connectionContext();
            frp = new Mock<IFiberPlanServ<FiberPlan>>();
            fp = new FiberPlan {PlanName="TestPlan",PlanPrice="100",PlanSpeed="5mbps",Validity="28days"};
        }

        [Test]
        public void GetAllPlanTest()
        {
            frp.Setup(G => G.GetFiberPlans().Result).Returns(fcc.FiberPlans.ToList());
            con = new FiberController(frp.Object);
            var data = con.GetAllFiberPlans().Result as OkObjectResult;
            Assert.AreEqual(200,data.StatusCode);
        }
        [Test]
        public void InvalidGetAllPlanTest()
        {
            frp.Setup(G => G.GetFiberPlans().Result).Returns(fcc.FiberPlans.ToList());
            con = new FiberController(frp.Object);
            var data = con.GetAllFiberPlans().Result as OkObjectResult;
            Assert.AreEqual(500, data.StatusCode);
        }
        [Test]
        public void GetValidId()
        {
            frp.Setup(G => G.GetFiberPlansByID(501).Result).Returns(fcc.FiberPlans.Find(501));
            con = new FiberController(frp.Object);
            var data = con.GetById(501).Result as OkObjectResult;
            Assert.AreEqual(200,data.StatusCode);
        }
        [Test]
        public void GetInValidId()
        {
            frp.Setup(G => G.GetFiberPlansByID(501).Result).Returns(fcc.FiberPlans.Find(501));
            con = new FiberController(frp.Object);
            var data = con.GetById(501).Result as OkObjectResult;
            Assert.AreEqual(500, data.StatusCode);
        }

    }
}