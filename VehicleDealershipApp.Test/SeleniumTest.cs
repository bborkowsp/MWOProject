using AngleSharp.Dom;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;


namespace VehicleDealershipApp.Test
{
    [TestClass]
    public class SeleniumTest
    {
        private IWebDriver FirefoxDriver;
        private static string BASIC_URL = "https://localhost:7255/VehicleAPI";

        [TestInitialize]
        public void Initialize()
        {
            Console.WriteLine("Setting up FirefoxDriver...");
            
            new DriverManager().SetUpDriver(new FirefoxConfig());
            
            FirefoxOptions options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;

            options.AddArgument("--headless");
            
            FirefoxDriver = new FirefoxDriver(options);
        }

        [TestCleanup]
        public void Cleanup()
        {
            FirefoxDriver.Quit();
        }

        [TestMethod]
        public void Test1_CreateVehicleWithModelAndFuelValues()
        {
            FirefoxDriver.Navigate().GoToUrl(BASIC_URL + "/Create");
            Thread.Sleep(1000);

            FirefoxDriver.FindElement(By.CssSelector("#Model")).SendKeys("myModelTestValue");
            FirefoxDriver.FindElement(By.CssSelector("#Fuel")).SendKeys("myFuelTestValue");
            FirefoxDriver.FindElement(By.CssSelector("input[value='Create']")).Click();
            Thread.Sleep(1000);

            IWebElement expectedVehicle = FirefoxDriver.FindElement(By.XPath("//*[contains(text(),'" + "myModelTestValue" + "')]"));
            Assert.IsTrue(expectedVehicle.Displayed);
        }

        
        [TestMethod]
        public void Test2_GetVehicleDetails()
        {
            FirefoxDriver.Navigate().GoToUrl(BASIC_URL);
            Thread.Sleep(1000);

            IWebElement vehicleToSeeDetails = FirefoxDriver.FindElement(By.XPath("//*[contains(text(),'" + "myModelTestValue" + "')]"));
            IWebElement vehicleToSeeDetailsParent = vehicleToSeeDetails.FindElement(By.XPath("./.."));

            IJavaScriptExecutor js = (IJavaScriptExecutor)FirefoxDriver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", vehicleToSeeDetailsParent);
            Thread.Sleep(500);

            vehicleToSeeDetailsParent.FindElement(By.CssSelector("a[aria-label='Details']")).Click();
            Thread.Sleep(1000);

            IWebElement modelDetails = FirefoxDriver.FindElement(By.XPath("//*[contains(text(),'" + "myModelTestValue" + "')]"));
            Assert.IsTrue(modelDetails.Displayed);
        }


        [TestMethod]
        public void Test3_EditVehicle()
        {
            FirefoxDriver.Navigate().GoToUrl(BASIC_URL);
            Thread.Sleep(1000);
            IWebElement vehicleToEdit = FirefoxDriver.FindElement(By.XPath("//*[contains(text(),'" + "myModelTestValue" + "')]"));
            IWebElement vehicleToEditParent = vehicleToEdit.FindElement(By.XPath("./.."));

            IJavaScriptExecutor js = (IJavaScriptExecutor)FirefoxDriver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", vehicleToEditParent);
            Thread.Sleep(500);

            vehicleToEditParent.FindElement(By.CssSelector("a[aria-label='Edit']")).Click();
            Thread.Sleep(1000);
            IWebElement modelToEdit = FirefoxDriver.FindElement(By.CssSelector("#Model"));
            modelToEdit.Clear();
            modelToEdit.SendKeys("UpdatedModel");
            IWebElement fuelToEdit = FirefoxDriver.FindElement(By.CssSelector("#Fuel"));
            fuelToEdit.Clear();
            fuelToEdit.SendKeys("UpdatedFuel");
            Thread.Sleep(1000);

            FirefoxDriver.FindElement(By.CssSelector("input[value='Save']")).Click();
            Thread.Sleep(1000);


            IWebElement expectedVehicleModel = FirefoxDriver.FindElement(By.XPath("//*[contains(text(),'" + "UpdatedModel" + "')]"));
            Assert.IsTrue(expectedVehicleModel.Displayed);
            IWebElement expectedVehicleFuel = FirefoxDriver.FindElement(By.XPath("//*[contains(text(),'" + "UpdatedFuel" + "')]"));
            Assert.IsTrue(expectedVehicleFuel.Displayed);
        }

        [TestMethod]
        public void Test4_DeleteVehicle()
        {
            FirefoxDriver.Navigate().GoToUrl(BASIC_URL);
            Thread.Sleep(1000);

            IWebElement vehicleToDelete = FirefoxDriver.FindElement(By.XPath("//*[contains(text(),'" + "UpdatedModel" + "')]"));
            IWebElement vehicleToDeleteParent = vehicleToDelete.FindElement(By.XPath("./.."));
            Thread.Sleep(1000);

            IJavaScriptExecutor js = (IJavaScriptExecutor)FirefoxDriver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", vehicleToDeleteParent);
            Thread.Sleep(500);

            vehicleToDeleteParent.FindElement(By.CssSelector("a[aria-label='Delete']")).Click();
            Thread.Sleep(1000);

            FirefoxDriver.FindElement(By.CssSelector("input[value='Delete']")).Click();
            Thread.Sleep(1000);

            Assert.ThrowsException<OpenQA.Selenium.NoSuchElementException>(() =>
                    FirefoxDriver.FindElement(By.XPath("//*[contains(text(),'" + "UpdatedModel" + "')]")));
        }

    }
}