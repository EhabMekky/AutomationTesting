using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System.Configuration;
using System.Reflection;
using CSharpSelFramework.Tests;

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

        protected static JsonReader getDataParser()
        {
            return new JsonReader();
        }

        [TearDown]
        public void CleanUp()
        {
            _driver?.Quit();
            _driver?.Dispose();
        }
    }
}