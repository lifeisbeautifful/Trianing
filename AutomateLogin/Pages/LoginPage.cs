using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutomateLogin
{
    public class LoginPage
    {
        IWebDriver Driver;
        public LoginPage(IWebDriver driver)
        {
            Driver = driver;
        }

        IWebElement username => Driver.FindElement(By.ClassName("form-control"));
        IWebElement password => Driver.FindElement(By.Name("Password"));
        public IWebElement loginLink => Driver.FindElement(By.Id("loginLink"));
        public IWebElement loginBtn => Driver.FindElement(By.XPath("//input[@type='submit']"));
        public IWebElement logOff => Driver.FindElement(By.LinkText("Log off"));
      
        public bool setCreds(string username,string password)
        {
            this.username.SendKeys(username);
            this.password.SendKeys(password);
            loginBtn.Click();
            return logOff.Displayed;
        }

        public bool LogOff()
        {
            logOff.Click();
            return loginLink.Displayed;
        }
    }
}
