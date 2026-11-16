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
            var headless = string.Equals(
                Environment.GetEnvironmentVariable("HEADLESS"),
                "true",
                StringComparison.OrdinalIgnoreCase);

            IWebDriver driver;

            switch (browser.ToLowerInvariant())
            {
                case "firefox":
                    var firefoxOptions = new FirefoxOptions();
                    if (headless)
                        firefoxOptions.AddArgument("-headless");
                    driver = new FirefoxDriver(firefoxOptions);
                    break;

                case "edge":
                    var edgeOptions = new EdgeOptions();
                    if (headless)
                    {
                        edgeOptions.AddArgument("--headless=new");
                        edgeOptions.AddArgument("--no-sandbox");
                        edgeOptions.AddArgument("--disable-dev-shm-usage");
                        edgeOptions.AddArgument("--disable-gpu");
                    }
                    driver = new EdgeDriver(edgeOptions);
                    break;

                case "chrome":
                default:
                    var chromeOptions = new ChromeOptions();
                    if (headless)
                    {
                        chromeOptions.AddArgument("--headless=new");
                        chromeOptions.AddArgument("--no-sandbox");
                        chromeOptions.AddArgument("--disable-dev-shm-usage");
                        chromeOptions.AddArgument("--disable-gpu");
                    }
                    else
                    {
                        chromeOptions.AddArgument("--start-maximized");
                    }
                    driver = new ChromeDriver(chromeOptions);
                    break;
            }

            // Keep navigation bounded: remote calculator pages can occasionally stall
            // while loading third-party resources. DOM readiness is sufficient for these tests.
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(30);
            return driver;
        }
    }
}
