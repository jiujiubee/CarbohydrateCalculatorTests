package tests;

import org.testng.Assert;
import org.testng.annotations.Test;

import java.util.List;

public class CalculatorFunctionalityTest extends BaseTest {

    @Test(description = "TC09 - Activity dropdown defaults to Moderate and has 7 options")
    public void testActivityDropdownDefaults() {
        String selected = calculatorPage.getSelectedActivity();
        Assert.assertTrue(selected.contains("Moderate"),
                "Expected default activity to contain 'Moderate', got: " + selected);

        List<String> options = calculatorPage.getActivityOptions();
        Assert.assertEquals(options.size(), 7,
                "Expected 7 activity options, got: " + options.size());
    }

    @Test(description = "TC10 - Settings link reveals BMR / body fat section")
    public void testSettingsRevealsBodyFatSection() {
        calculatorPage.clickSettings();
        Assert.assertTrue(calculatorPage.isBodyFatSectionVisible(),
                "Expected body fat input to be visible after clicking Settings");
    }

    @Test(description = "TC11 - Valid inputs produce a result table")
    public void testValidCalculationShowsResultTable() {
        calculatorPage.fillDefaultData();
        calculatorPage.clickCalculate();
        Assert.assertTrue(calculatorPage.isResultTableDisplayed(),
                "Expected result table to be displayed after calculation");
    }

    @Test(description = "TC13 - Other Units section expands")
    public void testOtherUnitsSectionExpands() {
        calculatorPage.clickOtherUnits();
        String source = calculatorPage.getPageSource();
        Assert.assertTrue(source.contains("From"),
                "Expected converter section to be visible after clicking Other Units");
    }
}
