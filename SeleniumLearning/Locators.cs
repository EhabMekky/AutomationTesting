using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using NUnit.Framework;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;


namespace SeleniumLearning
{

    public class Locators
    {
        // Xpath, CSS, ID, ClassName, Name, TagName, linkText
        #pragma warning disable NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
            driver = new EdgeDriver();

            // Implicit wait can be declared globally
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise";
        }

        [Test]
        public void Locator()
        {
            driver.FindElement(By.Id("username")).SendKeys("ehab.khallaf");
            driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");

            driver.FindElement(By.Name("password")).SendKeys("learning");

            // check box
            driver.FindElement(By.Id("terms")).Click();

            // CSS selector %Xpath
            //driver.FindElement(By.CssSelector("input[value='Sign in']")).Click();
            driver.FindElement(By.XPath("//input[@value='Sign In']")).Click();

            //Thread.Sleep(3000);

            // Explicit Wait Implementation
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(
                driver.FindElement(By.XPath("//input[@value='Sign In']")), "Sign In"));
            
            string errorMessage = driver.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.Write(errorMessage);

            //LinkText
            IWebElement link = driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
            link.Click();
            string hrefAtrr = link.GetAttribute("href");
            string expectedHrefAtrr = "https://rahulshettyacademy.com/documents-request";

            //Assert.AreEqual(expectedHrefAtrr, hrefAtrr);
            Assert.That(expectedHrefAtrr, Is.EqualTo(hrefAtrr));
        }
    }
}
