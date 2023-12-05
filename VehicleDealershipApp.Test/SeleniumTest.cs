using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;


namespace VehicleDealershipApp.Test
{
    [TestClass]
    public class SeleniumTest
    {
        private IWebDriver Driver;
        private static string APP_URL = "http://localhost:5238/VehicleAPI";

        [TestInitialize]
        public void Initialize()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AddArgument("--marionette-port=0");
            if (IsRunningInGithubActions())
            {
                options.AddArgument("--headless");
            }
            options.AcceptInsecureCertificates = true;
            Console.WriteLine("Setup Firefox Driver...");
            Driver = new FirefoxDriver(options);            
        }

        private bool IsRunningInGithubActions()
        {
            string githubActions = Environment.GetEnvironmentVariable("GITHUB_ACTIONS");
            if (!string.IsNullOrEmpty(githubActions) && githubActions.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        [TestCleanup]
        public void Cleanup()
        {
            Driver.Quit();
        }

        [TestMethod]
        public void CreateTestWithModelAndFuelValues()
        {
            Driver.Navigate().GoToUrl(APP_URL + "/Create");
            Thread.Sleep(2000);
            Driver.FindElement(By.CssSelector("#Model")).SendKeys("myModelTestValue");
            Driver.FindElement(By.CssSelector("#Fuel")).SendKeys("myFuelTestValue");
            Driver.FindElement(By.CssSelector("input[value='Create']")).Click();
            Thread.Sleep(1000);
            IWebElement tableRecord = Driver.FindElement(By.XPath("//*[contains(text(),'" + "myModelTestValue" + "')]"));
            Assert.IsTrue(tableRecord.Displayed);
        }

        
       

    }
}