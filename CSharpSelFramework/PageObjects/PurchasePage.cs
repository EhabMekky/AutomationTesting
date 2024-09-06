using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace CSharpSelFramework.PageObjects;

public class PurchasePage
{
    private  IWebDriver _driver;
    private By checkBox = By.XPath("/html/body/app-root/app-shop/div/app-checkout/div/div[2]/label");
    public PurchasePage(IWebDriver driver)
    {
        this._driver = driver;
        PageFactory.InitElements(_driver, this);
    }

    [FindsBy(How = How.Id, Using = "country")]
    private IWebElement country;
    public void GetCountry(string name)
    {
        country.SendKeys(name);
    }

    public void getCheckBox()
    {
        _driver.FindElement(checkBox).Click();
    }
}