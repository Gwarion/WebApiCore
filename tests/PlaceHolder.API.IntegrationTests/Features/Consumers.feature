Feature: Consumers

Scenario: CreateConsumer should create one consumer
	Given the following Address
		| Country | City       | Street               | PostalCode |
		| France  | Strasbourg | 1 avenue de l'europe | 67000      |
	And the following Consumer
		| FirstName | LastName | Email        | PhoneNumber  |
		| John      | Doe      | test@test.fr | +33600000001 |
	When I send the following request
		| Method | RequestUri |
		| POST   | Consumer   |
	Then No exception occurs
	And I Get the status code '201'
	And I Get the following Consumer
		| FirstName | lastName |
		| John      | Doe      |


