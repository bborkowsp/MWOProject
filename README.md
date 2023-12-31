# MWOProject  
Do zrealizowania projektu wykorzystałem aplikację MVC napisaną w języku C#.  
Aplikacja służy do wykonywania podstawowych operacji CRUD na obiektach klasy Vehicle.  
Każdy obiekt klasy Vehicle składa się z dwóch pól tj. Model oraz Fuel.  
Widok aplikacji:  
![image](https://github.com/bborkowsp/MWOProject/assets/95755487/b2942065-a15c-41d8-9dcd-51172377449b)  
![image](https://github.com/bborkowsp/MWOProject/assets/95755487/6dde2527-2770-4999-a2d8-3bbba1de8aae)  

## Testy Selenium
Ważnym parametrem przy wykonywaniu testów jest parametr "headless", który pozwala na uruchomienie testów w środowisku Github Actions.
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
```yml
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
```

## Azure devops
Aby zintegrować pipeline z Azure Devops musiałem najpierw założyć nową organizację na Azure devops.  
![image](https://github.com/bborkowsp/MWOProject/assets/95755487/39301708-714a-47b1-ae3e-b2c0862301fe)  
Następnie wygenerowałe tzw. Personal Access Token.  
![image](https://github.com/bborkowsp/MWOProject/assets/95755487/52ef8ca0-e18d-4585-ac8c-4c88683652e5)  

![image](https://github.com/bborkowsp/MWOProject/assets/95755487/de422761-414c-4835-9247-f986a1620bb1)  
Token musiałem skopiować i dodać nowy sekret do mojego repozytorium  
![image](https://github.com/bborkowsp/MWOProject/assets/95755487/67d2d995-18d6-438a-ade0-06e1483acbc2)  

Musiałem również wygenerować token github'owy.  
![image](https://github.com/bborkowsp/MWOProject/assets/95755487/f74b10d6-082c-494b-969c-f2bc0cbb1a5d)  
![image](https://github.com/bborkowsp/MWOProject/assets/95755487/f163e87b-cbab-4cec-b80f-18df8285f8f8)  
W efekcie w moim repozotorium są dwa sekrety:  
![image](https://github.com/bborkowsp/MWOProject/assets/95755487/2686c98f-ad67-4bd2-a8e5-f468e3e4db46)  

W ten sposób jeśli w github actions zostanie wykryty błąd to automatycznie będzie tworzony bug w projekcie na Azure Devops.  
![image](https://github.com/bborkowsp/MWOProject/assets/95755487/a7ec8210-73d8-474f-9b38-47c47793c72d)

Link do prezentacji: https://youtu.be/vKzj3uoTUlI
