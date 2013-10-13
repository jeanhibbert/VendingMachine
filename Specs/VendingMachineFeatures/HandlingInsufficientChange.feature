Feature: Handling insufficient change
	To prevent the user from becoming unhappy
	When the machine is empty or has insufficient change
	The machine needs to return the original change and display a message to the user

@mytag
Scenario: Returning users change because the machine has insufficient change
	Given The vending machine has insufficient change
	When I have attempted to purchased an item for "65" pence using 1 pound coin
	Then I the user should recieve a one pound coin as his change
	And The machine should provide the insufficient change message
