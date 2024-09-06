using System.Collections;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumLearning;

namespace CSharpSelFramework.PageObjects;

public class ProductsPage
{
    private IWebDriver _driver;
    By cardTitle = By.CssSelector(".card-title a");
    By addToCart = By.CssSelector(".card-footer button:last-of-type");
    public ProductsPage(IWebDriver driver)
    {
        this._driver = driver;
        PageFactory.InitElements(driver, this);
    }


    [FindsBy(How = How.TagName, Using = "app-card")]
    private IList<IWebElement> _cards;

    [FindsBy(How = How.PartialLinkText, Using = "Checkout")]
    private IWebElement _checkoutButton;

    public void WaitToCheckout()
    {
        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(8));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));
    }

    public IList<IWebElement> GetCards()
    {
        return _cards;
    }
    public By GetCardTitle()
    {
        return cardTitle;
    }
    public By AddToCartButton()
    {
        return addToCart;
    }
    public CheckoutPage GetCheckout()
    {
        _checkoutButton.Click();
        return new CheckoutPage(_driver);

    }
}