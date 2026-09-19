package tests;

import org.testng.Assert;
import org.testng.annotations.Test;

public class ClearButtonTest extends BaseTest {

    @Test(description = "TC12 - Clear button resets age and weight fields")
    public void testClearButtonResetsFields() {
        calculatorPage.fillDefaultData();
        calculatorPage.clickClear();

        Assert.assertEquals(calculatorPage.getAgeValue(), "",
                "Age field should be empty after clicking Clear");
        Assert.assertEquals(calculatorPage.getWeightValue(), "",
                "Weight field should be empty after clicking Clear");
    }
}
