using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;

namespace SeleniumLearning;

public class E2ETest
{
#pragma warning disable NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method

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
    public void EndtoEndFlow()
    {

        String[] expectedProducts = { "iphone X", "Blackberry" };
        string[] actualProducts = new string[2];

        driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
        driver.FindElement(By.Name("password")).SendKeys("learning");
        driver.FindElement(By.Id("terms")).Click();
        driver.FindElement(By.Id("signInBtn")).Click();

        // Explicit Wait Implementation
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));

        IList<IWebElement> products = driver.FindElements(By.TagName("app-card"));

        foreach (IWebElement product in products)
        {
            if (expectedProducts.Contains(product
                    .FindElement(
                        By.CssSelector(".card-title a")).Text))
            {
                product.FindElement(By.CssSelector(".card-footer button")).Click();
                // click on cart
            }

            TestContext.Progress.WriteLine(product
                .FindElement(By.CssSelector(".card-title a")).Text);
        }

        driver.FindElement(By.PartialLinkText("Checkout")).Click();

        IList<IWebElement> checkoutProducts = driver.FindElements(By.CssSelector("h4 a"));

        for (int i = 0; i < checkoutProducts.Count; i++)
        {
            actualProducts[i] = checkoutProducts[i].Text;
        }

        TestContext.Progress.WriteLine(actualProducts);
        Assert.That(actualProducts, Is.EqualTo(expectedProducts).AsCollection);
        
        driver.FindElement(By.ClassName("btn-success")).Click();
        
        // Explicit Wait Implementation
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("country")));
        driver.FindElement(By.Id("country")).SendKeys("Egypt");
        
        driver.FindElement(By.CssSelector("body > app-root > app-shop > div > app-checkout > div > div.checkbox.checkbox-primary")).Click();
        driver.FindElement(By.ClassName("btn-lg")).Click();
        
        string confirmText = driver.FindElement(By.CssSelector(".alert-success")).Text;

        Assert.That(confirmText.Contains("Success!"));
        
    }
}