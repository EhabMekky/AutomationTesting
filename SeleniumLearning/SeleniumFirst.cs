using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;

namespace SeleniumLearning
{
    public class SeleniumFirst
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            // initialize new instence from chrome
            // ChromeDriver.exe on chrome browser
            // Firefox rely on gecko driver
            // WebDriverManager (Download version and let selenium about this version)

            // new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            //driver = new ChromeDriver();

            //new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            //driver = new FirefoxDriver();

            new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
            driver = new EdgeDriver();

            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test1()
        {
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise";
            TestContext.Progress.WriteLine(driver.Title);
            TestContext.Progress.WriteLine(driver.Url);
            //TestContext.Progress.WriteLine(driver.PageSource);

            // driver.Close(); // close the browser
            //driver.Quit(); // close all windows accessed by automation
        }

        [TearDown]
        public void Teardown()
        {
            // Dispose of the driver instance
            driver.Quit();
            driver.Dispose();
        }
    }
}
