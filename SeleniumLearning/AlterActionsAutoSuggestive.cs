using System.Text.RegularExpressions;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumLearning;

public class AlterActionsAutoSuggestive
{
    private IWebDriver _driver;
    private readonly bool _closeBrowser = false;

    [SetUp]
    public void StartBrowser()
    {
        _driver = new ChromeDriver();

        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        
        _driver.Manage().Window.Maximize();

        _driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
    }

    [Test]
    public void test_Alert()
    {
        string name = "Ehab";
        
        _driver.FindElement(By.Id("name")).SendKeys("Ehab");
        
        _driver.FindElement(By.Id("confirmbtn")).Click();

         string alterText = _driver.SwitchTo().Alert().Text; // for assertion 
         
        _driver.SwitchTo().Alert().Accept();
        // _driver.SwitchTo().Alert().Dismiss();
        // _driver.SwitchTo().Alert().SendKeys("Hello"); // in edit box case

        Assert.That(alterText, Does.Contain(name));
    }

    [Test]
    public void test_AutoSuggestiveDropDowns()
    {
        _driver.FindElement(By.Id("autocomplete")).SendKeys("eg");
        
        // Explicit wait
        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(3));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("autocomplete")));

        IList<IWebElement> options = _driver.FindElements(By.ClassName("ui-menu-item"));

        foreach (IWebElement option in options)
        {
            if (option.Text.Equals("Egypt"))
                option.Click();
        }

        // run time values are not extracted.
        TestContext.Progress.WriteLine(_driver.FindElement(By.Id("autocomplete")).Text);
        
        // Solution 
        TestContext.Progress.WriteLine(_driver.FindElement(By.Id("autocomplete")).GetAttribute("Value"));
    }
    
    [TearDown]
    public void Dispode()
    {
        if(_closeBrowser)
            _driver.Dispose();
        else Console.WriteLine("Browser remains open for debugging purposes.");
    }
}