using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System.Configuration;
using System.Reflection;

namespace CSharpSelFramework.Utilties
{
    public class Base
    {
        protected IWebDriver? _driver;

        [SetUp]
        public void StartBrowser()
        {
            string browserName = GetBrowserNameFromConfig();
            InitBrowser(browserName);
            if (_driver != null)
            {
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                _driver.Manage().Window.Maximize();
                _driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";


            }
        }

        private static string? GetBrowserNameFromConfig()
        {
            string? name = ConfigurationManager.AppSettings["browser"];

            // if (string.IsNullOrEmpty(name))
            // {
            //     name = "Chrome";
            // }

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