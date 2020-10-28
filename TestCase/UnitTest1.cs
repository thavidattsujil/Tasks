using API.Controllers;
using NUnit.Framework;

namespace TestCase
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            MunicipalityInfo controller;

            //Create an instance of controller by passing repository
            controller = new MunicipalityInfo();

            //Number of records
            var getdetails = controller.Get();           
            
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}