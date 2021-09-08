using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutomateLogin
{
    public class HomePage
    {
        IWebDriver Driver;
     
        public HomePage(IWebDriver driver)
        {
            Driver = driver;
        }

        public IWebElement loginLink => Driver.FindElement(By.Id("loginLink"));
        public IWebElement loginBtn => Driver.FindElement(By.XPath("//input[@type='submit']"));

        public void ClickLogin() => loginLink.Click();


        public bool moveToLogin()
        {
            return loginBtn.Displayed;
        }
    }
}
