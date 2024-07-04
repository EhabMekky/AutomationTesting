using NUnit.Framework;

namespace SeleniumLearning
{
    public class Tests
    {
        // prerequisites 
        [SetUp]
        public void Setup()
        {
            //log
            TestContext.Progress.WriteLine("Setup Method Execution");
        }

        // Functionally 
        [Test]
        public void Test1()
        {
            TestContext.Progress.WriteLine("This is test 1");
        }

        [Test]
        public void Test2()
        {
            TestContext.Progress.WriteLine("This is test 2");
        }

        [TearDown]
        public void CloseBrowser() 
        {
            TestContext.Progress.WriteLine("Tear down method");
        }
    }
}