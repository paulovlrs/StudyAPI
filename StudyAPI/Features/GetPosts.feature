Feature: GetPosts
	Teste GET posts opretaion with RestSharp.net

Scenario: Verificar o autor do posts 1
	Given que eu realizo a operação GET para o <url>
	And que eu realizo a operação post para o <ID>
	Then devo visualizar na <chave> o valor <valor>

	Examples:
		| url            | chave  | valor      | ID |
		| posts/{postid} | author | Karthik KK | 1  |

Scenario: Verificar o autor do posts 2
	Given que eu realizo a operação GET para o <url>
	And que eu realizo a operação post para o <ID>
	Then devo visualizar na <chave> o valor <valor>

	Examples:
		| url            | chave | valor       | ID |
		| posts/{postid} | title | json-server | 2  |

Scenario: Verificar o autor do posts 6
	Given que eu realizo a operação GET para o <url>
	And que eu realizo a operação post para o <ID>
	Then devo visualizar na <chave> o valor <valor>

	Examples:
		| url            | chave  | valor             | ID |
		| posts/{postid} | author | ExecuteAutomation | 6  |