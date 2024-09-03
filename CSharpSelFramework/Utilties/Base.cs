using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System.Configuration;

namespace CSharpSelFramework.Utilties
{
    public class Base
    {
        protected IWebDriver? _driver;

        [SetUp]
        public void StartBrowser()
        {
            // Configure Browser
            string? browserName = GetBrowserNameFromConfig();
            InitBrowser(browserName);

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            _driver.Manage().Window.Maximize();
            _driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
        }
        private string? GetBrowserNameFromConfig()
        {
            string? browserName = ConfigurationManager.AppSettings.Get("browserName");
            if (string.IsNullOrEmpty(browserName))
            {
                throw new ConfigurationErrorsException("Browser name is not specified in the configuration file.");
            }

            return browserName;
        }

        protected void InitBrowser(string? browserName)
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

        [TearDown]
        public void CleanUp()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
                _driver = null;
            }
        }
    }
}