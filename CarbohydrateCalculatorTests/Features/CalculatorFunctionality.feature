Feature: Calculator Functionality
  As a user
  I want to use the carbohydrate calculator features
  So that I can get accurate nutritional information

  @TC09 @Functional @High
  Scenario: TC09 - Activity dropdown defaults and options
    Given I am on the carbohydrate calculator page
    Then the Activity dropdown default should be "Light"
    And the Activity dropdown should have 6 options

  @TC10 @Functional @High
  Scenario: TC10 - Settings link reveals BMR estimation section
    Given I am on the carbohydrate calculator page
    When I click the Settings link
    Then the Body Fat input section should be visible

  @TC11 @Functional @High
  Scenario: TC11 - Valid calculation produces result table
    Given I am on the carbohydrate calculator page
    And I fill in default calculation data
    When I click Calculate
    Then the result table should be displayed

  @TC12 @Functional @High
  Scenario: TC12 - Clear button resets input fields
    Given I am on the carbohydrate calculator page
    And I fill in default calculation data
    When I click the Clear button
    Then the Age field should be empty
    And the Weight field should be empty

  @TC13 @Functional @High
  Scenario: TC13 - Other Units converter section
    Given I am on the carbohydrate calculator page
    When I click the Other Units header
    Then the convert section should be visible
