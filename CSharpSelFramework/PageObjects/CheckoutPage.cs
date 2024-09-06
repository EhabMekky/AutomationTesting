using System.Collections;
using CSharpSelFramework.PageObjects;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumLearning;

public class CheckoutPage
{
    private IWebDriver _driver;
    public CheckoutPage(IWebDriver driver)
    {
        this._driver = driver;
        PageFactory.InitElements(_driver, this);
    }

    [FindsBy(How = How.CssSelector, Using = "h4 a")]
    private IList<IWebElement> checkoutCards;

    [FindsBy(How = How.CssSelector, Using = ".btn-success")]
    private IList<IWebElement> checkoutButton;


    public IList<IWebElement> GetCheckoutCards()
    {
        return checkoutCards;
    }

    public PurchasePage Checkout()
    {
        checkoutButton[0].Click();
        return new PurchasePage(_driver);
    }
}