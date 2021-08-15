Feature: AuthenticationJWT
	Test GET and POST operation with Authentication JWT

#Deve ser iniciado o Fake API para executar este teste

Scenario: Gerar token e visualizar produto 1
	Given I get JWT authentication of user with following details
		| Email           | Password |
		| paulo@email.com | 123456   |
	And que eu realizo a operação GET para o <url>
	When solicito a resposta para o <ID>
	Then devo visualizar na <chave> o valor <valor>

	Examples:
		| url               | chave | valor      | ID |
		| products/{postid} | name  | Product001 | 1  |

Scenario: Gerar token e visualizar produto 2
	Given I get JWT authentication of user with following details
		| Email           | Password |
		| paulo@email.com | 123456   |
	And que eu realizo a operação GET para o <url>
	When solicito a resposta para o <ID>
	Then devo visualizar na <chave> o valor <valor>

	Examples:
		| url               | chave | valor      | ID |
		| products/{postid} | name  | Product002 | 2  |