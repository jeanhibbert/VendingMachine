Feature: Determine Change For Vending Machine User
	When I purchase a product from the Machine
	As a vending machine user
	I want to recieve my change in the least amount of coins possible

@mytag
Scenario: Recieving change from the vending machine
	Given The vending machine has sufficient change
	And I have purchased an item for "65" pence
	When I provide a 1 pound coin
	Then I should recieve "35" pence change
	And The machine should thank the user for thier custom

Scenario: Recieving the right amount of coins from the vending machine
	Given The vending machine has sufficient change
	And I have purchased an item for "65" pence
	When I provide a 1 pound coin
	Then the couns returned should consist of two 20 pence coins and a 5 pence coin
	And The machine should thank the user for thier custom

