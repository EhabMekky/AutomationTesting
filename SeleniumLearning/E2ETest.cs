using CSharpSelFramework.Utilties;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;

namespace SeleniumLearning
{
    public class E2ETest : Base 
    {
        [Test]
        public void EndtoEndFlow()
        {

            String[] expectedProducts = { "iphone X", "Blackberry" };
            string[] actualProducts = new string[2];

            _driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            _driver.FindElement(By.Name("password")).SendKeys("learning");
            _driver.FindElement(By.Id("terms")).Click();
            _driver.FindElement(By.Id("signInBtn")).Click();

            // Explicit Wait Implementation
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));

            IList<IWebElement> products = _driver.FindElements(By.TagName("app-card"));

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

            _driver.FindElement(By.PartialLinkText("Checkout")).Click();

            IList<IWebElement> checkoutProducts = _driver.FindElements(By.CssSelector("h4 a"));

            for (int i = 0; i < checkoutProducts.Count; i++)
            {
                actualProducts[i] = checkoutProducts[i].Text;
            }

            TestContext.Progress.WriteLine(actualProducts);
            Assert.That(actualProducts, Is.EqualTo(expectedProducts).AsCollection);

            _driver.FindElement(By.ClassName("btn-success")).Click();

            // Explicit Wait Implementation
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("country")));
            _driver.FindElement(By.Id("country")).SendKeys("Egypt");

            _driver.FindElement(
                    By.CssSelector(
                        "body > app-root > app-shop > div > app-checkout > div > div.checkbox.checkbox-primary"))
                .Click();
            _driver.FindElement(By.ClassName("btn-lg")).Click();

            string confirmText = _driver.FindElement(By.CssSelector(".alert-success")).Text;

            Assert.That(confirmText.Contains("Success!"));

        }
    }
}