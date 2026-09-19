Feature: Age Input Validation
  As a user
  I want to see proper validation messages for invalid age inputs
  So that I know what values are acceptable

  @TC04 @Functional @High
  Scenario: TC04 - Zero input for age shows error
    Given I am on the carbohydrate calculator page
    When I enter "0" in the Age field
    And I click Calculate
    Then I should see an error message containing "between 18 and 80"

  @TC05 @Functional @Medium
  Scenario: TC05 - Negative input for age shows error
    Given I am on the carbohydrate calculator page
    When I enter "-5" in the Age field
    And I click Calculate
    Then I should see an error message containing "between 18 and 80"

  @TC06 @Functional @Medium
  Scenario: TC06 - Non-numeric input for age shows error
    Given I am on the carbohydrate calculator page
    When I enter ";" in the Age field
    And I click Calculate
    Then I should see an error message containing "between 18 and 80"

  @TC07 @Functional @Medium
  Scenario: TC07 - Integer MAX_VALUE for age shows boundary warning
    Given I am on the carbohydrate calculator page
    When I enter "2147483648" in the Age field
    And I click Calculate
    Then I should see an error message containing "between 18 and 80"

  @TC08 @Functional @High
  Scenario: TC08 - Age below minimum boundary shows warning
    Given I am on the carbohydrate calculator page
    When I enter "17" in the Age field
    And I click Calculate
    Then I should see an error message containing "between 18 and 80"

  @TC08 @Functional @High
  Scenario: TC08b - Age at minimum boundary is accepted
    Given I am on the carbohydrate calculator page
    And I fill in default calculation data
    When I enter "18" in the Age field
    And I click Calculate
    Then the result section should be displayed
