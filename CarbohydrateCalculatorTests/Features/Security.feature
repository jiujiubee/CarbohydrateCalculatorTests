Feature: Security Testing
  As a QA engineer
  I want to verify the calculator is protected against XSS attacks
  So that user data remains safe

  @TC15 @Security @High
  Scenario: TC15 - XSS script injection in weight field
    Given I am on the carbohydrate calculator page
    When I enter XSS payload "<script>alert('xss')</script>" in the Weight field
    And I click Calculate
    Then the script should not be executed in the page source
