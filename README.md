# MWOProject  
Do zrealizowania projektu wykorzystałem aplikację MVC napisaną w języku C#.  
Aplikacja służy do wykonywania podstawowych operacji CRUD na obiektach klasy Vehicle.  
Każdy obiekt klasy Vehicle składa się z dwóch pól tj. Model oraz Fuel.  
Widok aplikacji:  
![image](https://github.com/bborkowsp/MWOProject/assets/95755487/b2942065-a15c-41d8-9dcd-51172377449b)  
![image](https://github.com/bborkowsp/MWOProject/assets/95755487/6dde2527-2770-4999-a2d8-3bbba1de8aae)  

Testy UI wykonałem w Selenium:
```csharp
using AngleSharp.Dom;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;


namespace VehicleDealershipApp.Test
{
    [TestClass]
    public class SeleniumTest
    {
        private IWebDriver ChromeDriver;
        private static string BASIC_URL = "https://localhost:7255/VehicleAPI";

        [TestInitialize]
        public void Initialize()
        {
            ChromeOptions options = new ChromeOptions();

            options.AddArguments("start-maximized");
            options.AddArguments("--disabled-gpu");
           // options.AddArgument("--headless");

            ChromeDriver = new ChromeDriver(options);
        }

        [TestCleanup]
        public void Cleanup()
        {
            ChromeDriver.Quit();
        }

        [TestMethod]
        public void Test1_CreateVehicleWithModelAndFuelValues()
        {
            ChromeDriver.Navigate().GoToUrl(BASIC_URL + "/Create");
            Thread.Sleep(1000);

            ChromeDriver.FindElement(By.CssSelector("#Model")).SendKeys("myModelTestValue");
            ChromeDriver.FindElement(By.CssSelector("#Fuel")).SendKeys("myFuelTestValue");
            ChromeDriver.FindElement(By.CssSelector("input[value='Create']")).Click();
            Thread.Sleep(1000);

            IWebElement expectedVehicle = ChromeDriver.FindElement(By.XPath("//*[contains(text(),'" + "myModelTestValue" + "')]"));
            Assert.IsTrue(expectedVehicle.Displayed);
        }

        
        [TestMethod]
        public void Test2_GetVehicleDetails()
        {
            ChromeDriver.Navigate().GoToUrl(BASIC_URL);
            Thread.Sleep(1000);

            IWebElement vehicleToSeeDetails = ChromeDriver.FindElement(By.XPath("//*[contains(text(),'" + "myModelTestValue" + "')]"));
            IWebElement vehicleToSeeDetailsParent = vehicleToSeeDetails.FindElement(By.XPath("./.."));

            IJavaScriptExecutor js = (IJavaScriptExecutor)ChromeDriver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", vehicleToSeeDetailsParent);
            Thread.Sleep(500);

            vehicleToSeeDetailsParent.FindElement(By.CssSelector("a[aria-label='Details']")).Click();
            Thread.Sleep(1000);

            IWebElement modelDetails = ChromeDriver.FindElement(By.XPath("//*[contains(text(),'" + "myModelTestValue" + "')]"));
            Assert.IsTrue(modelDetails.Displayed);
        }


        [TestMethod]
        public void Test3_EditVehicle()
        {
            ChromeDriver.Navigate().GoToUrl(BASIC_URL);
            Thread.Sleep(1000);
            IWebElement vehicleToEdit = ChromeDriver.FindElement(By.XPath("//*[contains(text(),'" + "myModelTestValue" + "')]"));
            IWebElement vehicleToEditParent = vehicleToEdit.FindElement(By.XPath("./.."));

            IJavaScriptExecutor js = (IJavaScriptExecutor)ChromeDriver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", vehicleToEditParent);
            Thread.Sleep(500);

            vehicleToEditParent.FindElement(By.CssSelector("a[aria-label='Edit']")).Click();
            Thread.Sleep(1000);
            IWebElement modelToEdit = ChromeDriver.FindElement(By.CssSelector("#Model"));
            modelToEdit.Clear();
            modelToEdit.SendKeys("UpdatedModel");
            IWebElement fuelToEdit = ChromeDriver.FindElement(By.CssSelector("#Fuel"));
            fuelToEdit.Clear();
            fuelToEdit.SendKeys("UpdatedFuel");
            Thread.Sleep(1000);

            ChromeDriver.FindElement(By.CssSelector("input[value='Save']")).Click();
            Thread.Sleep(1000);


            IWebElement expectedVehicleModel = ChromeDriver.FindElement(By.XPath("//*[contains(text(),'" + "UpdatedModel" + "')]"));
            Assert.IsTrue(expectedVehicleModel.Displayed);
            IWebElement expectedVehicleFuel = ChromeDriver.FindElement(By.XPath("//*[contains(text(),'" + "UpdatedFuel" + "')]"));
            Assert.IsTrue(expectedVehicleFuel.Displayed);
        }

        [TestMethod]
        public void Test4_DeleteVehicle()
        {
            ChromeDriver.Navigate().GoToUrl(BASIC_URL);
            Thread.Sleep(1000);

            IWebElement vehicleToDelete = ChromeDriver.FindElement(By.XPath("//*[contains(text(),'" + "UpdatedModel" + "')]"));
            IWebElement vehicleToDeleteParent = vehicleToDelete.FindElement(By.XPath("./.."));
            Thread.Sleep(1000);

            IJavaScriptExecutor js = (IJavaScriptExecutor)ChromeDriver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", vehicleToDeleteParent);
            Thread.Sleep(500);

            vehicleToDeleteParent.FindElement(By.CssSelector("a[aria-label='Delete']")).Click();
            Thread.Sleep(1000);

            ChromeDriver.FindElement(By.CssSelector("input[value='Delete']")).Click();
            Thread.Sleep(1000);

            Assert.ThrowsException<OpenQA.Selenium.NoSuchElementException>(() =>
                    ChromeDriver.FindElement(By.XPath("//*[contains(text(),'" + "UpdatedModel" + "')]")));
        }

    }
}
```
Ze względu na problemu z konfiguracją GitHub actions wykonałem tylko testy.
Link do prezentacji: https://youtu.be/S5fwt290YMA
