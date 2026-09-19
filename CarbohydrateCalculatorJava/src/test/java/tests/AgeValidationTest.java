package tests;

import org.testng.Assert;
import org.testng.annotations.Test;

public class AgeValidationTest extends BaseTest {

    @Test(description = "TC04 - Zero input for age shows error")
    public void testZeroAgeShowsError() {
        calculatorPage.enterAge("0");
        calculatorPage.clickCalculate();
        String error = calculatorPage.getErrorMessage();
        Assert.assertTrue(error.toLowerCase().contains("positive"),
                "Expected error about positive numbers, got: " + error);
    }

    @Test(description = "TC05 - Negative input for age shows error")
    public void testNegativeAgeShowsError() {
        calculatorPage.enterAge("-5");
        calculatorPage.clickCalculate();
        String error = calculatorPage.getErrorMessage();
        Assert.assertTrue(error.toLowerCase().contains("positive"),
                "Expected error about positive numbers, got: " + error);
    }

    @Test(description = "TC06 - Non-numeric input for age shows error")
    public void testNonNumericAgeShowsError() {
        calculatorPage.enterAge(";");
        calculatorPage.clickCalculate();
        String error = calculatorPage.getErrorMessage();
        Assert.assertTrue(error.toLowerCase().contains("positive"),
                "Expected error about positive numbers, got: " + error);
    }

    @Test(description = "TC07 - Integer MAX_VALUE overflow for age shows boundary warning")
    public void testIntegerOverflowAgeShowsWarning() {
        calculatorPage.enterAge("2147483648");
        calculatorPage.clickCalculate();
        String error = calculatorPage.getErrorMessage();
        Assert.assertTrue(error.toLowerCase().contains("between 18 and 80"),
                "Expected boundary warning, got: " + error);
    }

    @Test(description = "TC08 - Age below minimum boundary (17) shows warning")
    public void testAgeBelowMinBoundary() {
        calculatorPage.enterAge("17");
        calculatorPage.clickCalculate();
        String error = calculatorPage.getErrorMessage();
        Assert.assertTrue(error.toLowerCase().contains("between 18 and 80"),
                "Expected boundary warning, got: " + error);
    }

    @Test(description = "TC08b - Age at minimum boundary (18) is accepted")
    public void testAgeAtMinBoundaryAccepted() {
        calculatorPage.fillDefaultData();
        calculatorPage.enterAge("18");
        calculatorPage.clickCalculate();
        Assert.assertTrue(calculatorPage.isResultDisplayed(),
                "Expected result section to be displayed for valid age 18");
    }
}
