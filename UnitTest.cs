using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FiberPlanAPI.Service;
using FiberPlanAPI.FiberConnection;
using FiberPlanAPI.Repository;
using Moq;

namespace FiberPlanAPITest
{
    [TestFixture]
    public class UnitTest
    {
        public IFiberPlanServ<FiberPlan> fp_serv;
        public FiberPlan fp1;
        public FiberPlan fp;
        public FiberPlan fad;
        public FiberPlan fput;
        public FiberPlan fputin;
        public Mock<IFiberPlanServ<FiberPlan>> frp;
        [SetUp]
        public void Setup()
        {
            fp = new FiberPlan();
            frp = new Mock<IFiberPlanServ<FiberPlan>>();
            fp1 = new FiberPlan
            {
                PlanId = 501,
                PlanName = "Basic",
                PlanPrice = "499",
                PlanSpeed = "50mbps",
                OfferId = 201
            };
            fad = new FiberPlan
            {
                PlanId = 530,
                PlanName = "Test",
                PlanPrice = "5000",
                PlanSpeed = "60mbps",
                Validity = "30days",
                OfferId = 201
            };
            fput = new FiberPlan
            {
                PlanId = 530,
                PlanName = "Test",
                PlanPrice = "5000",
                PlanSpeed = "70mbps",
                Validity = "30days",
                OfferId = 201
            };
            fputin = new FiberPlan
            {
                PlanId = 1000,
                PlanName = "Test",
                PlanPrice = "5000",
                PlanSpeed = "70mbps",
                Validity = "30days",
                OfferId = 201
            };
        }
        [Test]
        public void ValidGetPlan()
        {
            frp.Setup(G => G.GetFiberPlansByID(500).Result).Returns(fp1);
            var res = fp.GetFiberPlansByID(500).Result;
            Assert.AreEqual(fp1.PlanName,res.PlanName);
        }

        [Test]
        public void InValidGetPlan()
        {
            frp.Setup(G => G.GetFiberPlansByID(500).Result).Returns(fp1);
            var res = fp.GetFiberPlansByID(501).Result;
            Assert.AreEqual(fp.PlanName, res.PlanName);
        }
        [Test]
        public void ValidAddPlan()
        {
            frp.Setup(G => G.GetFiberPlans().Result).Returns(fp.GetFiberPlans().Result);
            var res = fp.AddPlans(fad).Result;
            Assert.AreEqual(1, res);
        }
        [Test]
        public void InValidAddPlan()
        {
            frp.Setup(G => G.GetFiberPlans().Result).Returns(fp.GetFiberPlans().Result);
            var res = fp.AddPlans(fp1).Result;
            Assert.AreEqual(1, res);
        }
        [Test]
        public void ValidPutPlan()
        {
            frp.Setup(G => G.GetFiberPlans().Result).Returns(fp.GetFiberPlans().Result);
            var res = fp.EditPlan(530, fput).Result;
            Assert.AreEqual(1,res);
        }
        [Test]
        public void InValidPutPlan()
        {
            frp.Setup(G => G.GetFiberPlans().Result).Returns(fp.GetFiberPlans().Result);
            var res = fp.EditPlan(1000, fputin).Result;
            Assert.AreEqual(1, res);
        }
        [Test]
        public void ValidDeltePlan()
        {
            frp.Setup(G => G.GetFiberPlans().Result).Returns(fp.GetFiberPlans().Result);
            var res = fp.DeletePlan(fad.PlanId).Result;
            Assert.AreEqual(1, res);
        }
        [Test]
        public void InValidDeltePlan()
        {
            frp.Setup(G => G.GetFiberPlans().Result).Returns(fp.GetFiberPlans().Result);
            var res = fp.DeletePlan(1000).Result;
            Assert.AreEqual(1, res);
        }
    }
}
