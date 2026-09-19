package tests;

import org.testng.Assert;
import org.testng.annotations.Test;

public class SecurityTest extends BaseTest {

    @Test(description = "TC20 - XSS script tag in weight field should not execute")
    public void testXssInjectionInWeightField() {
        calculatorPage.enterWeight("<script>alert('xss')</script>");
        calculatorPage.clickCalculate();
        String source = calculatorPage.getPageSource();
        Assert.assertFalse(source.contains("<script>alert('xss')</script>"),
                "XSS payload should not appear as executable script in page source");
    }
}
