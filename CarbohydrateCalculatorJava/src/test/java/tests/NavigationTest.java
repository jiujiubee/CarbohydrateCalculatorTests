package tests;

import org.testng.Assert;
import org.testng.annotations.Test;

public class NavigationTest extends BaseTest {

    @Test(description = "TC01 - Calculator logo redirects to homepage")
    public void testLogoRedirectsToHomepage() {
        calculatorPage.clickLogo();
        String currentUrl = calculatorPage.getCurrentUrl();
        Assert.assertTrue(currentUrl.contains("calculator.net"),
                "Expected URL to contain 'calculator.net', got: " + currentUrl);
    }

    @Test(description = "TC02 - Sign in link redirects to login page")
    public void testSignInRedirectsToLoginPage() {
        calculatorPage.clickSignIn();
        String currentUrl = calculatorPage.getCurrentUrl();
        Assert.assertTrue(currentUrl.contains("sign-in"),
                "Expected URL to contain 'sign-in', got: " + currentUrl);
    }

    @Test(description = "TC14 - Print link navigates to print page")
    public void testPrintLinkNavigatesToPrintPage() {
        calculatorPage.fillDefaultData();
        calculatorPage.clickCalculate();
        calculatorPage.clickPrint();
        String currentUrl = calculatorPage.getCurrentUrl();
        Assert.assertTrue(currentUrl.contains("print"),
                "Expected URL to contain 'print', got: " + currentUrl);
    }
}
