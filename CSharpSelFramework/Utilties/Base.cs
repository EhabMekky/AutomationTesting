using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System.Configuration;
using System.Reflection;
using CSharpSelFramework.Tests;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace CSharpSelFramework.Utilties
{
    public class Base
    {
        private ExtentReports extent;
        public static ExtentTest test;
        string browserName;

        [OneTimeSetUp]
        public void SetUp()
        {
            var workingDirectory = Environment.CurrentDirectory;
            var projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            var reportPath = projectDirectory + "\\ExtentReport.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);

            extent = new ExtentReports();
            extent .AttachReporter(htmlReporter);
            extent.AddSystemInfo("Host Name", "Local host");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("User Name", "Rahul Shetty");
        }
        protected IWebDriver? _driver;

        [SetUp]
        public void StartBrowser()
        {
            var test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            //Configuration
            browserName = TestContext.Parameters["browserName"];
            if (browserName == null)
            {
                browserName = GetBrowserNameFromConfig();
            }

            InitBrowser(browserName);
            if (_driver != null)
            {
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

                _driver.Manage().Window.Maximize();
                _driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";


            }
        }

        private static string? GetBrowserNameFromConfig()
        {
            string? name = ConfigurationManager.AppSettings["browser"];
            return name;
        }

        public IWebDriver getDriver()
        {
            return _driver;
        }
        private void InitBrowser(string browserName)
        {
            switch (browserName)
            {
                case "Firefox":
                    _driver = new FirefoxDriver();
                    break;

                case "Chrome":
                    _driver = new ChromeDriver();
                    break;

                case "Edge":
                    _driver = new EdgeDriver();
                    break;

                default:
                    throw new ArgumentException($"Unsupported browser: {browserName}");
            }
        }

        public static JsonReader getDataParser()
        {
            return new JsonReader();
        }

        [TearDown]
        public void CleanUp()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;

            var time = DateTime.Now;
            String fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";
            if (status == TestStatus.Failed)
            {
                test.Fail("Test Failed",CaptureScreenShot(_driver, fileName));
                test.Log(Status.Fail, "test failed with logtrace: " + stackTrace);
            }
            else if (status == TestStatus.Passed)
            {

            }
            extent.Flush();
            _driver?.Quit();
            _driver?.Dispose();
        }

        public MediaEntityModelProvider CaptureScreenShot(IWebDriver _driver, String screenshotName)
        {
            ITakesScreenshot screenshotDriver = (ITakesScreenshot)_driver;
            var screenshot = screenshotDriver.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenshotName).Build();
        }
    }
}