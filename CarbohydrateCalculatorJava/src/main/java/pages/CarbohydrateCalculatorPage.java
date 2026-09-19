package pages;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.Select;
import org.openqa.selenium.support.ui.WebDriverWait;
import utils.ConfigReader;

import java.time.Duration;
import java.util.List;
import java.util.stream.Collectors;

public class CarbohydrateCalculatorPage {

    private final WebDriver driver;
    private final WebDriverWait wait;

    // --- Locators ---

    @FindBy(id = "homelink")
    private WebElement logoLink;

    @FindBy(css = "a[href*='sign-in']")
    private WebElement signInLink;

    @FindBy(id = "cage")
    private WebElement ageInput;

    @FindBy(id = "csex1")
    private WebElement genderMale;

    @FindBy(id = "csex2")
    private WebElement genderFemale;

    @FindBy(id = "cheightfeet")
    private WebElement heightFeetInput;

    @FindBy(id = "cheightinch")
    private WebElement heightInchInput;

    @FindBy(id = "cpound")
    private WebElement weightInput;

    @FindBy(id = "cactivity")
    private WebElement activityDropdown;

    @FindBy(xpath = "//input[@value='Calculate']")
    private WebElement calculateButton;

    @FindBy(xpath = "//input[@value='Clear']")
    private WebElement clearButton;

    @FindBy(css = "div.bigtext")
    private WebElement resultSection;

    @FindBy(css = "font[color='red']")
    private WebElement errorBanner;

    @FindBy(xpath = "//a[contains(text(),'Settings')]")
    private WebElement settingsLink;

    @FindBy(id = "cbodyfat")
    private WebElement bodyFatInput;

    @FindBy(xpath = "//a[contains(text(),'Print')]")
    private WebElement printLink;

    @FindBy(id = "calcSearchTerm")
    private WebElement searchInput;

    @FindBy(xpath = "//a[contains(text(),'Other Units')]")
    private WebElement otherUnitsHeader;

    @FindBy(css = "table.cinfoT")
    private WebElement resultTable;

    public CarbohydrateCalculatorPage(WebDriver driver) {
        this.driver = driver;
        this.wait = new WebDriverWait(driver,
                Duration.ofSeconds(ConfigReader.getExplicitWait()));
        PageFactory.initElements(driver, this);
    }

    // --- Actions ---

    public void navigateTo() {
        driver.get(ConfigReader.getBaseUrl());
    }

    public void clickLogo() {
        wait.until(ExpectedConditions.elementToBeClickable(logoLink)).click();
    }

    public void clickSignIn() {
        wait.until(ExpectedConditions.elementToBeClickable(signInLink)).click();
    }

    public void enterAge(String age) {
        ageInput.clear();
        ageInput.sendKeys(age);
    }

    public void selectGender(String gender) {
        if (gender.equalsIgnoreCase("Male")) {
            genderMale.click();
        } else {
            genderFemale.click();
        }
    }

    public void enterHeight(String feet, String inches) {
        heightFeetInput.clear();
        heightFeetInput.sendKeys(feet);
        heightInchInput.clear();
        heightInchInput.sendKeys(inches);
    }

    public void enterWeight(String weight) {
        weightInput.clear();
        weightInput.sendKeys(weight);
    }

    public void selectActivity(String activityText) {
        Select select = new Select(activityDropdown);
        select.selectByVisibleText(activityText);
    }

    public String getSelectedActivity() {
        Select select = new Select(activityDropdown);
        return select.getFirstSelectedOption().getText();
    }

    public List<String> getActivityOptions() {
        Select select = new Select(activityDropdown);
        return select.getOptions().stream()
                .map(WebElement::getText)
                .collect(Collectors.toList());
    }

    public void clickCalculate() {
        calculateButton.click();
    }

    public void clickClear() {
        clearButton.click();
    }

    public void clickSettings() {
        settingsLink.click();
    }

    public void clickPrint() {
        printLink.click();
    }

    public void clickOtherUnits() {
        otherUnitsHeader.click();
    }

    public void enterSearchTerm(String term) {
        searchInput.clear();
        searchInput.sendKeys(term);
    }

    public boolean isResultDisplayed() {
        try {
            return resultSection.isDisplayed();
        } catch (Exception e) {
            return false;
        }
    }

    public boolean isResultTableDisplayed() {
        try {
            return resultTable.isDisplayed();
        } catch (Exception e) {
            return false;
        }
    }

    public String getErrorMessage() {
        try {
            return wait.until(ExpectedConditions.visibilityOf(errorBanner)).getText();
        } catch (Exception e) {
            return "";
        }
    }

    public String getAgeValue() {
        return ageInput.getAttribute("value");
    }

    public String getWeightValue() {
        return weightInput.getAttribute("value");
    }

    public boolean isBodyFatSectionVisible() {
        try {
            return wait.until(ExpectedConditions.visibilityOf(bodyFatInput)).isDisplayed();
        } catch (Exception e) {
            return false;
        }
    }

    public String getCurrentUrl() {
        return driver.getCurrentUrl();
    }

    public String getPageSource() {
        return driver.getPageSource();
    }

    public void fillDefaultData() {
        selectGender("Male");
        enterAge("25");
        enterHeight("5", "10");
        enterWeight("160");
        selectActivity("Moderate: exercise 4-5 times/week");
    }
}
