# QA Project Experience – Carbohydrate Calculator

## Project Overview

- I worked on the QA effort for a nutrition calculator web application – specifically the Carbohydrate Calculator page on Calculator.net, https://www.calculator.net/carbohydrate-calculator.html. 
- The application lets users input their age, gender, height, weight, and activity level to compute daily carbohydrate intake recommendations. The page also includes unit converters, a search bar, print/save functionality, and integration with the site's user account system.

---

## Test Plan

<img width="2060" height="650" alt="image" src="https://github.com/user-attachments/assets/2ff8eefb-7eff-47da-bfd8-836d8e919325" />

---

## Automation Framework Structure

```
CarbohydrateCalculatorTests/
├── Features/                    # .feature files (Gherkin scenarios)
│   ├── Navigation.feature       # TC01, TC02, TC14
│   ├── AgeValidation.feature    # TC04-TC08
│   ├── CalculatorFunctionality.feature  # TC09-TC13
│   └── Security.feature         # TC15
├── StepDefinitions/             # Step binding classes
│   └── CalculatorSteps.cs
├── Pages/                       # Page Object Model
│   └── CarbohydrateCalculatorPage.cs
├── Drivers/                     # WebDriver factory
│   └── WebDriverFactory.cs
├── Support/                     # Hooks (setup/teardown)
│   └── Hooks.cs
└── CarbohydrateCalculatorTests.csproj
```

---

## UI Automation Test Execution Demo

https://github.com/user-attachments/assets/6573a710-2f49-4dc0-abd7-e6e54940475e

A more detailed summary can be found in [QA_Project_Experience.md](./QA_Project_Experience.md)
