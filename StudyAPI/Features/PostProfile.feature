Feature: PostProfile
	Simple calculator for adding two numbers

@mytag
Scenario: Verify Post Operation for Profile
	Given I Perform POST operation for "/post/{profile}/profile" with body
		| name | profile |
		| Sams | 2       |
	Then devo visualizar na <chave> o valor <valor>

	Examples:
		| chave | valor |
		| name  | Sams  |