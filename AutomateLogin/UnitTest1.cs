using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using AutomateLogin.Pages;

namespace AutomateLogin
{
    [TestFixture]
    public class Tests : Drivers
    {
        public string urlHome = "http://eaapp.somee.com/";
        public string urlLogin = "http://eaapp.somee.com/Account/Login";

        string adminUserName = "admin";
        string adminPassword = "password";

        string [] employeeCreatedData = { "Oksana", "4000", "2", "4", "a@mailforspam.com" };
        string[] employeeEditedData = { "Name", "3000", "1", "3", "a@mailforspam.com" };
        string searchData = "Test";

        

        [OneTimeSetUp]
        public void Setup()
        {
          ChooseDriver(Browsers.Chrome);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Driver.Close();
        }

        [TearDown]
        public void TearDown()
        {
            LoginPage login = new LoginPage(Driver);
          
            if (!login.logOff.Displayed) 
            { 
              Navigate(urlLogin);
              login.setCreds(adminUserName, adminPassword);
            }
        }

        [Test]
        public void Login()
        {
            HomePage homepage = new HomePage(Driver);
            LoginPage login = new LoginPage(Driver);

            if (login.logOff.Displayed) { login.LogOff(); }
            homepage.ClickLogin();
            Assert.IsTrue(homepage.moveToLogin(),"'Login' link is not displayed");

            bool loginResult = login.setCreds(adminUserName, adminPassword);
            Assert.That(loginResult, Is.True, "'Log off' link is not displyed");
        }

        [Test]
        public void CreateEmployee()
        {
            Navigate(urlHome);
            EmployeeListPage empListPage = new EmployeeListPage(Driver);
            CreatePage createPage = new CreatePage(Driver);
            LoginPage login = new LoginPage(Driver);

            if (login.loginLink.Displayed)
            {
                Navigate(urlLogin);
                login.setCreds(adminUserName, adminPassword); 
            }

            empListPage.EmployeePageNavigate();
            createPage.OpenCreatePage();
            createPage.CreateEditEmployee(employeeEditedData, employeeCreatedData);
            Assert.That(empListPage.CreateNewBtn.Displayed,"User is not navigated back to 'Employee List' page from 'Create' page");

            var foundCreatedEmployee = empListPage.SearchEmployee(employeeCreatedData[0],"single");
            Assert.IsTrue(empListPage.checkFoundEmpData(foundCreatedEmployee,employeeCreatedData), "Found user data does " +
               "not match with search criteria");
        }

       
        [Test]
        public void EditEmployee()
        {
            CreatePage createPage = new CreatePage(Driver);
            EmployeeListPage empListPage = new EmployeeListPage(Driver);

            empListPage.EmployeePageNavigate();
            empListPage.SearchEmployee(employeeCreatedData[0],"single");
            empListPage.TestEditLink();
            createPage.CreateEditEmployee(employeeEditedData, employeeCreatedData);
            Assert.That(empListPage.CreateNewBtn.Displayed, "User is not navigated back to 'Employee List' page from 'Edit' page");

            var foundEditedEmployee = empListPage.SearchEmployee(employeeEditedData[0],"single");
            Assert.IsTrue(empListPage.checkFoundEmpData(foundEditedEmployee,employeeEditedData));
        }

        [Test]
        public void TestSearch()
        {
            EmployeeListPage empListPage = new EmployeeListPage(Driver);
            empListPage.EmployeePageNavigate();
            var employees=empListPage.SearchEmployee(searchData, "mult");
            bool result = empListPage.checkFoundEmpData(employees, "mult", searchData);
            Assert.That(result, Is.True, "Not all found users follow search criteria");
        }

        [Test]
        public void ZDeleteEmployee()
        {
            EmployeeListPage empListPage = new EmployeeListPage(Driver);
            DeletePage deletePage = new DeletePage(Driver);

            empListPage.EmployeePageNavigate();
            empListPage.SearchEmployee(employeeEditedData[0],"single");
            deletePage.DeleteEmployee(employeeEditedData[0]);
            empListPage.SearchEmployee(employeeEditedData[0],"single");

            bool deleteResult = empListPage.checkIfEmpDeleted(employeeEditedData[0]);
            Assert.IsTrue(deleteResult, "User is not deleted");
        }
    }     
}