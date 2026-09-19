using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace CarbohydrateCalculatorTests.Drivers
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateDriver(string browser = "chrome")
        {
            IWebDriver driver;

            switch (browser.ToLower())
            {
                case "firefox":
                    driver = new FirefoxDriver();
                    break;
                case "edge":
                    driver = new EdgeDriver();
                    break;
                case "chrome":
                default:
                    var options = new ChromeOptions();
                    options.AddArgument("--start-maximized");
                    driver = new ChromeDriver(options);
                    break;
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            return driver;
        }
    }
}
