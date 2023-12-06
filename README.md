# MWOProject  
Do zrealizowania projektu wykorzystałem aplikację MVC napisaną w języku C#.  
Aplikacja służy do wykonywania podstawowych operacji CRUD na obiektach klasy Vehicle.  
Każdy obiekt klasy Vehicle składa się z dwóch pól tj. Model oraz Fuel.  
Widok aplikacji:  
![image](https://github.com/bborkowsp/MWOProject/assets/95755487/b2942065-a15c-41d8-9dcd-51172377449b)  
![image](https://github.com/bborkowsp/MWOProject/assets/95755487/6dde2527-2770-4999-a2d8-3bbba1de8aae)  

## Testy Selenium
```csharp
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;


namespace VehicleDealershipApp.Test
{
    [TestClass]
    public class SeleniumTests
    {
        private IWebDriver FirefoxDriver;
        private static string BASIC_URL = "https://localhost:7255/VehicleApi";

        [TestInitialize]
        public void Initialize()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AddArgument("--marionette-port=0");

                options.AddArgument("--headless");

            options.AcceptInsecureCertificates = true;
            Console.WriteLine("Setup Firefox Driver...");
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
            Thread.Sleep(2000);

            FirefoxDriver.FindElement(By.CssSelector("#Model")).SendKeys("myModelTestValue");
            Thread.Sleep(2000);

            FirefoxDriver.FindElement(By.CssSelector("#Fuel")).SendKeys("myFuelTestValue");
            Thread.Sleep(2000);
          
            FirefoxDriver.FindElement(By.CssSelector("input[value='Create']")).Click();
            Thread.Sleep(2000);

            IWebElement expectedVehicle = FirefoxDriver.FindElement(By.XPath("//*[contains(text(),'" + "myModelTestValue" + "')]"));
            Thread.Sleep(2000);

            Assert.IsTrue(expectedVehicle.Displayed);
        }

        
        [TestMethod]
        public void Test2_GetVehicleDetails()
        {
            FirefoxDriver.Navigate().GoToUrl(BASIC_URL);
            Thread.Sleep(2000);

            IWebElement vehicleToSeeDetails = FirefoxDriver.FindElement(By.XPath("//*[contains(text(),'" + "myModelTestValue" + "')]"));
            Thread.Sleep(2000);

            IWebElement vehicleToSeeDetailsParent = vehicleToSeeDetails.FindElement(By.XPath("./.."));

            IJavaScriptExecutor js = (IJavaScriptExecutor)FirefoxDriver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", vehicleToSeeDetailsParent);
            Thread.Sleep(2000);

            vehicleToSeeDetailsParent.FindElement(By.CssSelector("a[aria-label='Details']")).Click();
            Thread.Sleep(1000);

            IWebElement modelDetails = FirefoxDriver.FindElement(By.XPath("//*[contains(text(),'" + "myModelTestValue" + "')]"));
            Thread.Sleep(2000);

            Assert.IsTrue(modelDetails.Displayed);
        }


        [TestMethod]
        public void Test3_EditVehicle()
        {
            FirefoxDriver.Navigate().GoToUrl(BASIC_URL);
            Thread.Sleep(1000);
            IWebElement vehicleToEdit = FirefoxDriver.FindElement(By.XPath("//*[contains(text(),'" + "myModelTestValue" + "')]"));
            Thread.Sleep(2000);

            IWebElement vehicleToEditParent = vehicleToEdit.FindElement(By.XPath("./.."));

            IJavaScriptExecutor js = (IJavaScriptExecutor)FirefoxDriver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", vehicleToEditParent);
            Thread.Sleep(2000);

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


            IWebElement expectedVehicleModel = FirefoxDriver.FindElement(By.XPath("//*[contains(text(),'" + "Up1datedModel" + "')]"));
            Assert.IsTrue(expectedVehicleModel.Displayed);
            Thread.Sleep(2000);
            
            IWebElement expectedVehicleFuel = FirefoxDriver.FindElement(By.XPath("//*[contains(text(),'" + "UpdatedFuel" + "')]"));
            Thread.Sleep(2000);

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
            Thread.Sleep(2000);

            vehicleToDeleteParent.FindElement(By.CssSelector("a[aria-label='Delete']")).Click();
            Thread.Sleep(1000);

            FirefoxDriver.FindElement(By.CssSelector("input[value='Delete']")).Click();
            Thread.Sleep(1000);

            Assert.ThrowsException<OpenQA.Selenium.NoSuchElementException>(() =>
                    FirefoxDriver.FindElement(By.XPath("//*[contains(text(),'" + "UpdatedModel" + "')]")));
        }

    }
}
```

## Github Actions
Konfiguracja github actions:
'''yml
name: .NET

on:
  push:
    branches: [ "main" ]
  pull_request:
    branches: [ "main" ]

jobs:
  run-program-and-tests:

    runs-on: ubuntu-latest

    steps:
    - uses: actions/checkout@v3
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: 7.0.x
    - name: Restore dependencies
      run: dotnet restore
    - name: Build
      run: dotnet build --configuration Release --no-restore
    - name: Set executable permissions
      run: |
        chmod 777 -R /home/runner/work/MWOProject/MWOProject

    # Start API
    - name: Start API and WEB App
      run: nohup dotnet /home/runner/work/MWOProject/MWOProject/VehicleDealershipApp/bin/Release/net7.0/VehicleDealershipApp.Client.dll &

    # Wait for services to start (you may need to adjust the sleep duration)
    - name: Wait for services
      run: sleep 20

    # Run Selenium tests
    - name: Run Selenium tests
      run: dotnet test --verbosity normal

    - name: Create Bug Workitem on workflow failure
      uses: stefanstranger/azuredevops-bug-action@1.1
      if: failure()
      with:
        OrganizationName: "mwoProject"
        PAT: "PAT"
        ProjectName: "mwoProject"
        AreaPath: "mwoProject"
        IterationPath: "mwoProject"
        GithubToken: "GithubToken"
        WorkflowFileName: "integration.yml"
      env:
        PAT: ${{ secrets.PAT }}
        GithubToken: ${{ secrets.githubtoken }}
'''
Link do prezentacji: 
