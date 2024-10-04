using System.Reflection;
using System.Runtime.InteropServices.JavaScript;
using CSharpSelFramework.PageObjects;
using CSharpSelFramework.Utilties;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;

namespace SeleniumLearning
{
    public class E2ETest : Base
    {

        [Test, TestCaseSource(nameof(AddTestDataConfig))]
        [Parallelizable(ParallelScope.All)]
        public void EndtoEndFlow(String username, String password, String[] expectedProducts)
        {
            //String[] expectedProducts = { "iphone X", "Blackberry" };
            string[] actualProducts = new string[2];

            LoginPage loginPage = new LoginPage(getDriver());
            ProductsPage productsPage = loginPage.validLogin(username, password);

            // Explicit Wait Implementation
            productsPage.WaitToCheckout();

            IList<IWebElement> products = productsPage.GetCards();

            foreach (IWebElement product in products)
            {
                if (expectedProducts.Contains(product
                        .FindElement(productsPage.GetCardTitle()).Text))
                {
                    product.FindElement(productsPage.AddToCartButton()).Click();
                    // click on cart
                }
            }

            CheckoutPage checkoutPage = productsPage.GetCheckout();


            IList<IWebElement> checkoutProducts = checkoutPage.GetCheckoutCards();

            for (int i = 0; i < checkoutProducts.Count; i++)
            {
                actualProducts[i] = checkoutProducts[i].Text;
            }
            Assert.That(actualProducts, Is.EqualTo(expectedProducts).AsCollection);

            checkoutPage.Checkout();

           //_driver.FindElement(By.Id("country")).SendKeys("Egypt");
           PurchasePage purchasePage = checkoutPage.Checkout();
           purchasePage.GetCountry("Egypt");

           purchasePage.getCheckBox();

            Assembly assem = typeof(Base).Assembly;
            Console.WriteLine("Assembly name: {0}", assem.FullName);
        }

        [Test, Category("Smoke")]
        public void Locator()
        {
            _driver.FindElement(By.Id("username")).SendKeys("ehab.khallaf");
            _driver.FindElement(By.Id("username")).Clear();
            _driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");

            _driver.FindElement(By.Name("password")).SendKeys("learning");

            // check box
            _driver.FindElement(By.Id("terms")).Click();

            // CSS selector %Xpath
            //_driver.FindElement(By.CssSelector("input[value='Sign in']")).Click();
            _driver.FindElement(By.XPath("//input[@value='Sign In']")).Click();

            //Thread.Sleep(3000);

            // Explicit Wait Implementation
            // WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(8));
            // wait.Until(ExpectedConditions.TextToBePresentInElementValue(
            //     _driver.FindElement(By.XPath("//input[@value='Sign In']")), "Sign In"));

            string errorMessage = _driver.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.Write(errorMessage);

            //LinkText
            IWebElement link = _driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
            link.Click();
            string hrefAtrr = link.GetAttribute("href");
            string expectedHrefAtrr = "https://rahulshettyacademy.com/documents-request";

            //Assert.AreEqual(expectedHrefAtrr, hrefAtrr);
            Assert.That(expectedHrefAtrr, Is.EqualTo(hrefAtrr));
        }
        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData(getDataParser().ExtractData("username"), getDataParser().ExtractData("password"), getDataParser().ExtractDataArray("products"));
            //yield return new TestCaseData(getDataParser().ExtractData("username_wrong"), getDataParser().ExtractData("password_wrong")); // this test will fail
        }
    }
}