using System.Collections;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumLearning;

public class SortWebTables
{
    private IWebDriver _driver;
    private readonly bool _closeBrowser = false;

    [SetUp]
    public void StartBrowser()
    {
        _driver = new ChromeDriver();

        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        
        _driver.Manage().Window.Maximize();

        _driver.Url = "https://rahulshettyacademy.com/seleniumPractise/#/offers";
    }

    [Test]
    public void SortTable()
    {
        ArrayList a = new ArrayList();
        SelectElement dropDown = new SelectElement(_driver.FindElement(By.Id("page-menu")));
        
        dropDown.SelectByValue("20");

        // Step 1 - Get all items name into arrayList A
       IList<IWebElement> veggies = _driver.FindElements(By.XPath("//tr//td[1]"));

       foreach (IWebElement veggie in veggies )
       {
           a.Add(veggie.Text);
       }
        // Step 2 - Sort this arrayList A 
       
        foreach (string element in a) //Before sorting
        {
            TestContext.Progress.WriteLine(element);
        }
        a.Sort();
        foreach (string element in a) // After sorting
        {
            TestContext.Progress.WriteLine(element);
        }
        // Step 3 = Go and click on column
        _driver.FindElement(By.CssSelector(
            "#root > div > div.products-wrapper > div > div > div > div > table > thead > tr > th:nth-child(1) > span.sort-icon.sort-descending"))
            .Click();

       // Step 4 = Get all items names into arrayList B
       ArrayList b = new ArrayList();
       IList<IWebElement> sortedVeggies =  _driver.FindElements(By.XPath("//tr//td[1]"));
       foreach (IWebElement veggie in sortedVeggies )
       {
           b.Add(veggie.Text);
       }
        // Compare between A & B should be Equal
        Assert.That(a, Is.EqualTo(b));
        
        
        // _driver.FindElement(By.ClassName("sort-icon")).Click();
    }

    [TearDown]
    public void Dispode()
    {
      if(_closeBrowser)
          _driver.Dispose();
      else Console.WriteLine("Browser remains open for debugging purposes.");
    }
    
    
}