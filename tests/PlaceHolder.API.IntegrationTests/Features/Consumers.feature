Feature: Consumers

Scenario: CreateConsumer should create one consumer
	Given the following AddressDto
		| Country | City       | Street               | PostalCode |
		| France  | Strasbourg | 1 avenue de l'europe | 67000      |
	Given the following ConsumerDto
		| FirstName | lastName |
		| John      | Doe      |
	When I send the following request
		| Method | RequestUri |
		| POST   | Consumer   |
	Then No exception occurs
	And I Get the status code '201'
	And I Get the following ConsumerDto
		| FirstName | lastName |
		| John      | Doe      |


