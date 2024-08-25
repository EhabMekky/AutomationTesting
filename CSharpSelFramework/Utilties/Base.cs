using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CSharpSelFramework.Utilties
{
    public class Base
    {
        public IWebDriver _driver;

        [SetUp]
        public void StartBrowser()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            _driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
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