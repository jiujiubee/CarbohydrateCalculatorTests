using CarbohydrateCalculatorTests.Drivers;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace CarbohydrateCalculatorTests.Support
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var browser = Environment.GetEnvironmentVariable("BROWSER") ?? "chrome";
            var driver = WebDriverFactory.CreateDriver(browser);
            _scenarioContext["WebDriver"] = driver;
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_scenarioContext.TryGetValue("WebDriver", out IWebDriver driver))
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
