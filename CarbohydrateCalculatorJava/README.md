# Carbohydrate Calculator – Selenium Java Automation

## What This Is

A test automation project for the [Calculator.net Carbohydrate Calculator](https://www.calculator.net/carbohydrate-calculator.html), built as a portfolio piece for QA Automation Engineer interviews. It covers functional testing, input validation, boundary testing, and basic security testing.

## Tech Stack

| Tool | Version | Purpose |
|------|---------|---------|
| Java | 17 | Language |
| Selenium WebDriver | 4.21.0 | Browser automation |
| TestNG | 7.10.2 | Test runner + assertions |
| Maven | 3.9+ | Build & dependency management |
| WebDriverManager | 5.8.0 | Automatic browser driver setup |
| Page Object Model | — | Design pattern for maintainability |

## Project Structure

```
CarbohydrateCalculatorJava/
├── pom.xml
├── testng.xml
├── src/
│   ├── main/java/
│   │   ├── drivers/
│   │   │   └── DriverFactory.java          # ThreadLocal WebDriver, multi-browser
│   │   ├── pages/
│   │   │   └── CarbohydrateCalculatorPage.java  # All locators + page actions
│   │   └── utils/
│   │       └── ConfigReader.java           # Reads config.properties
│   ├── main/resources/
│   │   └── config.properties               # Base URL, browser, wait times
│   └── test/java/tests/
│       ├── BaseTest.java                   # Setup/teardown (@Before/@AfterMethod)
│       ├── NavigationTest.java             # TC01, TC02, TC14
│       ├── AgeValidationTest.java          # TC04–TC08 (boundary/edge cases)
│       ├── CalculatorFunctionalityTest.java # TC09–TC11, TC13
│       ├── ClearButtonTest.java            # TC12
│       └── SecurityTest.java              # TC20 (XSS)
```

## Test Coverage

| Test Class | Test Cases | What It Covers |
|------------|-----------|----------------|
| NavigationTest | TC01, TC02, TC14 | Logo redirect, sign-in link, print page |
| AgeValidationTest | TC04–TC08 | Zero, negative, non-numeric, overflow, boundary values |
| CalculatorFunctionalityTest | TC09–TC11, TC13 | Dropdown defaults, settings toggle, calculation results, unit converter |
| ClearButtonTest | TC12 | Field reset behavior |
| SecurityTest | TC20 | XSS injection in input fields |

## How to Run

### Prerequisites
- Java 17+
- Maven 3.9+
- Chrome, Firefox, or Edge installed

### Run all tests (default browser from config.properties)
```bash
mvn clean test
```

### Run a specific test class
```bash
mvn test -Dtest=AgeValidationTest
```

### Switch browser
Edit `src/main/resources/config.properties`:
```
browser=firefox
```

## Interview Talking Points

- **Why Page Object Model?** Keeps locators in one place. When the dev team changed an element ID, I updated one line in the page class instead of touching every test.
- **Why ThreadLocal in DriverFactory?** Supports parallel execution later without tests sharing browser state.
- **Why explicit waits?** The Settings toggle animates open — implicit waits alone couldn't reliably handle the timing. I used `ExpectedConditions.visibilityOf()` for elements that appear after user interaction.
- **Boundary testing approach:** I tested age = 0, -5, ";", 2147483648, 17 (just below min), and 18 (at min). The overflow case actually exposed a server-side bug — it returned a 500 instead of a validation message.
- **What I'd add next:** Data-driven tests with `@DataProvider` for the age validation matrix, screenshot capture on failure via a TestNG listener, and CI integration with GitHub Actions.

## Related Documents

See `../CarbohydrateCalculatorTests/QA_Project_Experience.md` for the full interview experience writeup with STAR-format behavioral answers, detailed responsibilities, and sample Q&A.
