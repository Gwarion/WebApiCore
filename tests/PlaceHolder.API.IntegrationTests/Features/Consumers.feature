Feature: Consumers

Scenario: CreateConsumer should create one consumer
	Given A Consumer with an address
	When I send the following request
		| Method | RequestUri |
		| POST   | Consumer   |
	Then No exception occurs
	And I get the status code '201'
	And I get the consumer


