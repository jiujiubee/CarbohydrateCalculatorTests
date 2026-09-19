using CarbohydrateCalculatorTests.Pages;
using FluentAssertions;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace CarbohydrateCalculatorTests.StepDefinitions
{
    [Binding]
    public class CalculatorSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;
        private CarbohydrateCalculatorPage _page;

        public CalculatorSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        private void InitDriver()
        {
            _driver = (IWebDriver)_scenarioContext["WebDriver"];
            _page = new CarbohydrateCalculatorPage(_driver);
        }

        [Given(@"I am on the carbohydrate calculator page")]
        public void GivenIAmOnTheCarbohydrateCalculatorPage()
        {
            InitDriver();
            _page.NavigateTo();
        }

        [Given(@"I fill in default calculation data")]
        public void GivenIFillInDefaultCalculationData()
        {
            _page.FillDefaultData();
        }

        [When(@"I click the Calculator\.net logo")]
        public void WhenIClickTheCalculatorNetLogo()
        {
            _page.ClickLogo();
        }

        [When(@"I click the sign in link")]
        public void WhenIClickTheSignInLink()
        {
            _page.ClickSignIn();
        }

        [When(@"I enter ""(.*)"" in the Age field")]
        public void WhenIEnterInTheAgeField(string value)
        {
            _page.EnterAge(value);
        }

        [Given(@"I click Calculate")]
        [When(@"I click Calculate")]
        public void WhenIClickCalculate()
        {
            _page.ClickCalculate();
        }

        [When(@"I click the Clear button")]
        public void WhenIClickTheClearButton()
        {
            _page.ClickClear();
        }

        [When(@"I click the Settings link")]
        public void WhenIClickTheSettingsLink()
        {
            _page.ClickSettings();
        }

        [When(@"I click the Print link")]
        public void WhenIClickThePrintLink()
        {
            _page.ClickPrint();
        }

        [When(@"I click the Other Units header")]
        public void WhenIClickTheOtherUnitsHeader()
        {
            _page.ClickOtherUnits();
        }

        [When(@"I enter XSS payload ""(.*)"" in the Weight field")]
        public void WhenIEnterXssPayloadInTheWeightField(string payload)
        {
            _page.EnterXssPayload(payload);
        }

        [Then(@"I should be redirected to ""(.*)""")]
        public void ThenIShouldBeRedirectedTo(string expectedUrl)
        {
            _driver.Url.Should().StartWith(expectedUrl);
        }

        [Then(@"I should be redirected to a URL containing ""(.*)""")]
        public void ThenIShouldBeRedirectedToAUrlContaining(string urlPart)
        {
            _driver.Url.Should().Contain(urlPart);
        }

        [Then(@"the URL should contain ""(.*)""")]
        public void ThenTheUrlShouldContain(string urlPart)
        {
            _driver.Url.Should().Contain(urlPart);
        }

        [Then(@"I should see an error message containing ""(.*)""")]
        public void ThenIShouldSeeAnErrorMessageContaining(string expectedText)
        {
            var errorMsg = _page.GetErrorMessage();
            errorMsg.ToLower().Should().Contain(expectedText.ToLower());
        }

        [Then(@"the result section should be displayed")]
        public void ThenTheResultSectionShouldBeDisplayed()
        {
            _page.IsResultDisplayed().Should().BeTrue();
        }

        [Then(@"the result table should be displayed")]
        public void ThenTheResultTableShouldBeDisplayed()
        {
            _page.IsResultTableDisplayed().Should().BeTrue();
        }

        [Then(@"the Activity dropdown default should be ""(.*)""")]
        public void ThenTheActivityDropdownDefaultShouldBe(string expectedDefault)
        {
            _page.GetSelectedActivity().Should().Contain(expectedDefault);
        }

        [Then(@"the Activity dropdown should have (.*) options")]
        public void ThenTheActivityDropdownShouldHaveOptions(int count)
        {
            _page.GetActivityOptions().Should().HaveCount(count);
        }

        [Then(@"the Body Fat input section should be visible")]
        public void ThenTheBodyFatInputSectionShouldBeVisible()
        {
            _page.IsBodyFatSectionVisible().Should().BeTrue();
        }

        [Then(@"the Age field should be empty")]
        public void ThenTheAgeFieldShouldBeEmpty()
        {
            _page.GetAgeValue().Should().BeEmpty();
        }

        [Then(@"the Weight field should be empty")]
        public void ThenTheWeightFieldShouldBeEmpty()
        {
            _page.GetWeightValue().Should().BeEmpty();
        }

        [Then(@"the convert section should be visible")]
        public void ThenTheConvertSectionShouldBeVisible()
        {
            // After clicking Other Units, the converter iframe should be present
            _page.IsConverterIframePresent().Should().BeTrue();
        }

        [Then(@"the script should not be executed in the page source")]
        public void ThenTheScriptShouldNotBeExecutedInThePageSource()
        {
            // Verify the raw script tag is not rendered as executable HTML
            var source = _page.GetPageSource();
            source.Should().NotContain("<script>alert('xss')</script>");
        }
    }
}
