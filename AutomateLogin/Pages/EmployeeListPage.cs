using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AutomateLogin
{
    public class EmployeeListPage
    {
        IWebDriver Driver;
        public EmployeeListPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public IWebElement EmployeeList => Driver.FindElement(By.LinkText("Employee List"));
        public IWebElement SearchInput => Driver.FindElement(By.XPath("//input[@type='submit']"));
        public IWebElement CreateNewBtn => Driver.FindElement(By.XPath("//a[text()='Create New']"));

      
        public EmployeeListPage EmployeePageNavigate()
        {
            EmployeeList.Click();
            return new EmployeeListPage(Driver);
        }
     
        public List<IWebElement> SearchEmployee(string data,string quantity)
        {
            IWebElement searchField = Driver.FindElement(By.Name("searchTerm"));
            IWebElement searchBtn = Driver.FindElement(By.XPath("//input[@type='submit']"));
            searchField.SendKeys(data);
            searchBtn.Click();

            List<IWebElement> employeesData = Driver.FindElements(By.XPath("//table[@class='table']/tbody/tr/td")).ToList();
            List<IWebElement> employeesNames = Driver.FindElements(By.XPath("//table[@class='table']/tbody/tr/td[1]")).ToList();

            if (quantity == "mult") { return employeesNames; }
            return employeesData;
        }

        public bool checkFoundEmpData(List<IWebElement> data,params string[]empData)
        {
            if (empData[0]=="mult")
            {
                for(int i = 0; i < data.Count; i++)
                {
                    if (data[i].Text.Contains(empData[1]))
                    {
                        continue;
                    }
                    TakeScrenshot();
                    return false;
                }
                return true;
            }
            else
            {
                for (int i = 0; i < empData.Length; i++)
                {
                    if (empData[i] == data[i].Text) { continue; }
                    Console.WriteLine($"Created user info does not match with entered data at index {i}");
                    Console.WriteLine($"data.Text = {data[i].Text}");
                    Console.WriteLine($"empData = {empData[i]}");
                    TakeScrenshot();
                    return false;
                }
                return true;
            }
        }

        public void TestEditLink()
        {
            IWebElement editlnk = Driver.FindElement(By.LinkText("Edit"));
            editlnk.Click();
        }

        public bool checkIfEmpDeleted(string name)
        {
            List<IWebElement> employees = Driver.FindElements(By.XPath("//table[@class='table']/tbody/tr/td[1]")).ToList();
            if (employees.Count > 0) { TakeScrenshot(); return false; }
            return true;
        }

        public void TakeScrenshot()
        {
            try
            {
                Screenshot ss = ((ITakesScreenshot)Driver).GetScreenshot();
                ss.SaveAsFile(@"C:\Users\ognyp\Desktop\SelScreens\FoundEmpScr.jpeg");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
