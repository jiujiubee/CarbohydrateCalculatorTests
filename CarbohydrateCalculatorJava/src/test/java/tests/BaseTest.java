package tests;

import drivers.DriverFactory;
import org.testng.annotations.AfterMethod;
import org.testng.annotations.BeforeMethod;
import pages.CarbohydrateCalculatorPage;

public class BaseTest {

    protected CarbohydrateCalculatorPage calculatorPage;

    @BeforeMethod
    public void setUp() {
        DriverFactory.getDriver();
        calculatorPage = new CarbohydrateCalculatorPage(DriverFactory.getDriver());
        calculatorPage.navigateTo();
    }

    @AfterMethod
    public void tearDown() {
        DriverFactory.quitDriver();
    }
}
