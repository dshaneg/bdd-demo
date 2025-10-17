Feature: Bank Account Management
  As a bank customer
  I want to manage my bank account
  So that I can deposit, withraw, and check my balance

  Scenario: Deposit money into account
    Given I have a bank account with a balance of $100
    When I deposit $50
    Then my account balance should be $150

  Scenario: Withraw money from account with sufficient funds
    Given I have a bank account with a balance of $100
    When I withdraw $30
    Then my account balance should be $70

  Scenario: Cannot withdraw more than account balance
    Given I have a bank account with a balance of $50
    When I attempt to withdraw $100
    Then I should see an error "Insufficient funds"
    And my account balance should remain $50

  @slow
  Scenario: Process multiple transactions
    Given I have a bank account with a balance of $1000
    When I perform the following transactions:
      | Type     | Amount |
      | Deposit  | 200    |
      | Withdraw | 150    |
      | Deposit  | 75     |
    Then my account balance should be $1125