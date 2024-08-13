using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace SeleniumLearning;

public class WindowHandlers
{
    private IWebDriver _driver;
    private readonly bool _closeBrowser = false;
    [SetUp]
    public void StartBrowser()
    {
        _driver = new ChromeDriver();
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        _driver.Manage().Window.Maximize();
        _driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
    }

    [Test]
    public void WindowHandler()
    {
        _driver.FindElement(By.ClassName("blinkingText")).Click();
        
        Assert.That(2, Is.EqualTo( _driver.WindowHandles.Count));

        // Switch to child window
        _driver.SwitchTo().Window(_driver.WindowHandles[1]);

        TestContext.Progress.WriteLine(_driver.FindElement(By.ClassName("red")).Text);
        string text = _driver.FindElement(By.ClassName("red")).Text;

        string[] textArray = text.Split("at");
        textArray[1] = textArray[1].Trim().Split(" ")[0];
        TestContext.Progress.WriteLine(textArray[1]);
    }
    
    [TearDown]
    public void Dispose()
    {
        if(_closeBrowser)
            _driver.Dispose();
        else Console.WriteLine("Browser remains open for debugging purposes.");
    }
}