using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace CSharpSelFramework.PageObjects;

public class LoginPage
{
    private IWebDriver driver;
    // Constructor
    public LoginPage(IWebDriver driver)
    {
        this.driver = driver;
        PageFactory.InitElements(driver, this);
    }

    // Page object factory
    [FindsBy(How = How.Id, Using = "username")]
    private IWebElement username;

    [FindsBy(How = How.Name, Using = "password")]
    private IWebElement password;

    [FindsBy(How = How.Id, Using = "terms")]
    private IWebElement terms;

    [FindsBy(How = How.Id, Using = "signInBtn")]
    private IWebElement signInBtn;

    public void validLogin(string user, string pass)
    {
        username.SendKeys(user);
        password.SendKeys(pass);
        terms.Click();
        signInBtn.Click();
    }

}