using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

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
    public void Test_Alert()
    {
        string name = "Ehab";
        
        _driver.FindElement(By.Id("name")).SendKeys(name);
        
        _driver.FindElement(By.Id("confirmbtn")).Click();

         string alterText = _driver.SwitchTo().Alert().Text; // for assertion 
         
        _driver.SwitchTo().Alert().Accept();
        // _driver.SwitchTo().Alert().Dismiss();
        // _driver.SwitchTo().Alert().SendKeys("Hello"); // in edit box case

        Assert.That(alterText, Does.Contain(name));
    }

    [Test]
    public void Test_AutoSuggestiveDropDowns()
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

    [Test]
    public void Test_Actions()
    {
        _driver.Url = "https://rahulshettyacademy.com/#/INDEX";
        Actions a = new Actions(_driver);
        a.MoveToElement(_driver.FindElement(By.ClassName("dropdown-toggle"))).Perform();

        _driver.FindElement(By.PartialLinkText("About")).Click();
    } 
    
    [Test]
    public void Test_Actions_DragDrop()
    {
        _driver.Url = "https://demo.automationtesting.in/Static.html";
        Actions a = new Actions(_driver);
        a.DragAndDrop(_driver.FindElement(By.Id("angular")), _driver.FindElement(By.Id("droparea"))).Perform();
    }

    [Test]
    public void Frames()
    {
        // Scroll
        IWebElement frameScroll = _driver.FindElement(By.Id("courses-iframe"));
        IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
        js.ExecuteScript("arguments[0].scrollIntoView(true);", frameScroll);
        
        // Wait for the iframe to be available and switch to it  
        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));   
        wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.Id("courses-iframe")));  
        
        // ID, Name, Index
        //_driver.SwitchTo().Frame("courses-iframe");
        _driver.FindElement(By.LinkText("All Access Plan")).Click();
    }
    
    [TearDown]
    public void Dispose()
    {
        if(_closeBrowser)
            _driver.Dispose();
        else Console.WriteLine("Browser remains open for debugging purposes.");
    }
}