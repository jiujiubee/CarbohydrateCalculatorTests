using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CarbohydrateCalculatorTests.Pages
{
    public class CarbohydrateCalculatorPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private const string PageUrl = "https://www.calculator.net/carbohydrate-calculator.html";

        // Locators
        private readonly By _logoLink = By.CssSelector("#logo a");
        private readonly By _signInLink = By.CssSelector("a[href*='sign-in']");
        private readonly By _ageInput = By.Id("cage");
        private readonly By _genderMale = By.Id("csex1");
        private readonly By _genderFemale = By.Id("csex2");
        private readonly By _heightFeetInput = By.Id("cheightfeet");
        private readonly By _heightInchInput = By.Id("cheightinch");
        private readonly By _weightInput = By.Id("cpound");
        private readonly By _heightMetricInput = By.Id("cheightmeter");
        private readonly By _weightMetricInput = By.Id("ckg");
        private readonly By _activityDropdown = By.Id("cactivity");
        private readonly By _calculateButton = By.XPath("//input[@value='Calculate']");
        private readonly By _clearButton = By.XPath("//input[@value='Clear']");
        private readonly By _resultSection = By.CssSelector("table.cinfoT");
        private readonly By _errorBanner = By.CssSelector("font[color='red']");
        private readonly By _settingsLink = By.XPath("//a[contains(text(),'Settings')]");
        private readonly By _bodyFatInput = By.Name("cfatpct");
        private readonly By _printLink = By.XPath("//a[contains(text(),'Print')]");
        private readonly By _saveLink = By.XPath("//a[contains(text(),'Save')]");
        private readonly By _searchInput = By.Id("calcSearchTerm");
        private readonly By _otherUnitsHeader = By.XPath("//a[contains(text(),'Other Units')]");
        private readonly By _convertFromInput = By.Id("fromVal");
        private readonly By _resultTable = By.CssSelector("table.cinfoT");

        public CarbohydrateCalculatorPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void NavigateTo()
        {
            _driver.Navigate().GoToUrl(PageUrl);
        }

        public void ClickLogo()
        {
            ClickElement(_logoLink);
        }

        public void ClickSignIn()
        {
            ClickElement(_signInLink);
        }

        public void EnterAge(string age)
        {
            SetInputValue(_ageInput, age);
        }

        public void SelectGender(string gender)
        {
            if (gender.Equals("Male", StringComparison.OrdinalIgnoreCase))
                ClickElement(_genderMale);
            else
                ClickElement(_genderFemale);
        }

        public void EnterHeight(string feet, string inches)
        {
            SetInputValue(_heightFeetInput, feet);
            SetInputValue(_heightInchInput, inches);
        }

        public void EnterWeight(string weight)
        {
            SetInputValue(_weightInput, weight);
        }

        private void SetInputValue(By locator, string value)
        {
            var js = (IJavaScriptExecutor)_driver;
            var element = _driver.FindElement(locator);
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            js.ExecuteScript("arguments[0].value = '';", element);
            js.ExecuteScript("arguments[0].value = arguments[1];", element, value);
        }

        private void ClickElement(By locator)
        {
            var js = (IJavaScriptExecutor)_driver;
            var element = _driver.FindElement(locator);
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            js.ExecuteScript("arguments[0].click();", element);
        }

        public void SelectActivity(string activity)
        {
            var dropdown = new SelectElement(_driver.FindElement(_activityDropdown));
            dropdown.SelectByText(activity);
        }

        public string GetSelectedActivity()
        {
            var dropdown = new SelectElement(_driver.FindElement(_activityDropdown));
            return dropdown.SelectedOption.Text;
        }

        public List<string> GetActivityOptions()
        {
            var dropdown = new SelectElement(_driver.FindElement(_activityDropdown));
            return dropdown.Options.Select(o => o.Text).ToList();
        }

        public void ClickCalculate()
        {
            ClickElement(_calculateButton);
        }

        public void ClickClear()
        {
            ClickElement(_clearButton);
        }

        public void ClickSettings()
        {
            ClickElement(_settingsLink);
        }

        public void ClickPrint()
        {
            ClickElement(_printLink);
        }

        public void ClickOtherUnits()
        {
            ClickElement(_otherUnitsHeader);
        }

        public void EnterConvertFromValue(string value)
        {
            var element = _driver.FindElement(_convertFromInput);
            element.Clear();
            element.SendKeys(value);
        }

        public void EnterSearchTerm(string term)
        {
            var element = _driver.FindElement(_searchInput);
            element.Clear();
            element.SendKeys(term);
        }

        public bool IsResultDisplayed()
        {
            try
            {
                _wait.Until(d =>
                {
                    try { return d.FindElement(_resultSection).Displayed; }
                    catch { return false; }
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsResultTableDisplayed()
        {
            try
            {
                return _driver.FindElement(_resultTable).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public string GetErrorMessage()
        {
            try
            {
                return _driver.FindElement(_errorBanner).Text;
            }
            catch (NoSuchElementException)
            {
                return string.Empty;
            }
        }

        public string GetAgeValue()
        {
            return _driver.FindElement(_ageInput).GetAttribute("value");
        }

        public string GetWeightValue()
        {
            return _driver.FindElement(_weightMetricInput).GetAttribute("value");
        }

        public bool IsBodyFatSectionVisible()
        {
            try
            {
                _wait.Until(d => d.FindElement(_bodyFatInput).Displayed);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetCurrentUrl()
        {
            return _driver.Url;
        }

        public void EnterXssPayload(string payload)
        {
            SetInputValue(_weightMetricInput, payload);
        }

        public string GetPageSource()
        {
            return _driver.PageSource;
        }

        public bool IsConverterIframePresent()
        {
            try
            {
                var iframe = _driver.FindElement(By.CssSelector("iframe[src*='converter']"));
                return iframe.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void FillDefaultData(string age = "25", string heightCm = "180",
            string weightKg = "60", string gender = "Male", string activity = "Moderate: exercise 4-5 times/week")
        {
            SelectGender(gender);
            EnterAge(age);
            SetInputValue(_heightMetricInput, heightCm);
            SetInputValue(_weightMetricInput, weightKg);
            SelectActivity(activity);
        }
    }
}
