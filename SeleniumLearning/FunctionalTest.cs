using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumLearning;

public class FunctionalTest
{
    // Select class
   private IWebDriver driver;

    [SetUp]
    public void StartBrowser()
    {
        driver = new EdgeDriver();

        // Implicit wait can be declared globally
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        driver.Manage().Window.Maximize();
        driver.Url = "https://rahulshettyacademy.com/loginpagePractise";
    }

    [Test]
    public void DropDown()
    {
      IWebElement dropdown =  driver.FindElement(By.CssSelector("#login-form > div:nth-child(5) > select"));
      SelectElement s = new SelectElement(dropdown);
      s.SelectByText("Teacher");
      s.SelectByValue("stud");
      s.SelectByIndex(1);
    }

    [Test]
    public void Customradio()
    {
        IList<IWebElement> rdos =
            driver.FindElements(By.Id("usertype"));

        foreach (IWebElement radioButtons in rdos)
        {
            if (radioButtons.GetAttribute("Value").Equals("user"))
            {
                radioButtons.Click();
            }
        }
        
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("okayBtn")));
            
        driver.FindElement(By.Id("okayBtn")).Click();
       
        Boolean result = driver.FindElement(By.Id("usertype")).Selected;
        Assert.That(result, Is.True);
    }
    
    [TearDown]
    public void Teardown()
    {
        Thread.Sleep(5000);
        driver.Quit();
        driver.Dispose();
        
    }
}