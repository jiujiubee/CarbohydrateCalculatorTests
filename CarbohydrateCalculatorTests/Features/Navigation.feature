Feature: Calculator Navigation
  As a user
  I want to navigate using the calculator page header elements
  So that I can access related pages

  @TC01 @Functional @High
  Scenario: TC01 - Calculator logo redirects to homepage
    Given I am on the carbohydrate calculator page
    When I click the Calculator.net logo
    Then I should be redirected to "https://www.calculator.net/"

  @TC02 @Integration @High
  Scenario: TC02 - Sign in link redirects to login page
    Given I am on the carbohydrate calculator page
    When I click the sign in link
    Then I should be redirected to a URL containing "sign-in"

  @TC14 @Functional @High
  Scenario: TC14 - Print link navigates to print-friendly page
    Given I am on the carbohydrate calculator page
    And I fill in default calculation data
    And I click Calculate
    When I click the Print link
    Then the URL should contain "print"
