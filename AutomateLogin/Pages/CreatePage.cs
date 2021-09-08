using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutomateLogin.Pages
{
    public class CreatePage
    {
        IWebDriver Driver { get; set; }
        public CreatePage(IWebDriver driver)
        {
            Driver = driver;
        }

        public IWebElement CreateNewBtn => Driver.FindElement(By.XPath("//a[text()='Create New']"));
        public IWebElement CreateLnk => Driver.FindElement(By.XPath("//input[@type='submit']"));


        public bool OpenCreatePage()
        {
            CreateNewBtn.Click();
            return CreateLnk.Displayed;
        }


        public void CreateEditEmployee(string[] userData, params string[] fieldInputs)
        {
            List<IWebElement> inputs = Driver.FindElements(By.TagName("input")).ToList();
            int i = 0;

            for (int j = 0; j < inputs.Count; j++)
            {
                if (inputs[j].GetAttribute("value") == "")
                {
                    inputs[j].SendKeys(fieldInputs[i]);
                    i++;
                }

                if (i < 5)
                {
                    if (inputs[j].GetAttribute("value") == fieldInputs[i])
                    {
                        inputs[j].Clear();
                        inputs[j].SendKeys(userData[i]);
                        i++;
                    }
                }

                if (j == inputs.Count - 1)
                {
                    inputs[j].Click();
                }
            }
        }
    }
}
