using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace AutomateLogin.Pages
{
    public class DeletePage
    {
        IWebDriver Driver { get; set; }
        public DeletePage(IWebDriver driver)
        {
            Driver= driver;
        }


        public void DeleteEmployee(string data)
        {
            IWebElement deleteLnk = Driver.FindElement(By.LinkText("Delete"));
            deleteLnk.Click();
           
            IWebElement deleteBtn = Driver.FindElement(By.XPath("//div[@class='form-actions no-color']/input[@type='submit']"));
            deleteBtn.Click();
        }
    }
}
