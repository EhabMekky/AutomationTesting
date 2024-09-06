using System.Reflection;
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
        [Test]
        public void EndtoEndFlow()
        {
            String[] expectedProducts = { "iphone X", "Blackberry" };
            string[] actualProducts = new string[2];

            LoginPage loginPage = new LoginPage(getDriver());
            ProductsPage productsPage = loginPage.validLogin("rahulshettyacademy", "learning");

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
    }
}