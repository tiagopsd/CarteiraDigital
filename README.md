# Carteira Digital

API ASP.NET Core.
Persiste os dados em SQL Server.
Modelagem de software Domain Driven Design (DDD).

Para executar a aplicação basta ter o docker instalado em sua máquina, entrar na pasta principal do projeto e executar o comando docker-compose up.
Isso pode levar um tempo. Todas as aplicações necessárias serão iniciadas.

Assim que iniciado você podera testar a API através do Swagger, basta seguir as instruções abaixo:

Uma Api dividida em 4 controllers.

1 - Token

* Chave de segurança para acessar os endpoints com duração de 1 hora no horário universal (UTC).
Para obter o token de teste, basta enviar username: carteiradigital e password: carteiradigital.
Autorize os endpoints através do swagger no canto superior direito na opção Authorize.

2 - Usuario (User) 

* Cadastro de Usuario (Register)  
Para se cadastrar basta enviar seus dados, nome completo, CPF, data de nascimento, senha e o país de origem.
Nome deverá ser completo.
Deve ser enviado um CPF válido.
Obrigatório ser maior de idade, caso o país de origem for EUA deverá possuir no mínimo 21 anos, caso contrário 18.
Senha deve possuir entre 8 a 20 caracteres e sem espaços vazios.
País é um enumerador sendo Brasil = 0 e EUA = 1.

3 - Funcionalidades da conta (Account)

* Deposito (Deposit): Basta enviar seu CPF, senha cadastrados e o valor que deseja depositar.
* Saque (Withdraw): Basta enviar seu CPF, senha e o valor que deseja sacar.
* Transferência (Transfer): Basta enviar seu CPF, senha, valor da operação e o CPF do beneficiário.
* Saldo (Balance): Basta enviar seu CPF e senha para obter seu saldo atual.

4 - Movimentação da conta (Movement)

* Extrato de conta (History): Basta enviar seu CPF, senha, data inicial e final do período desejado. 

Caso você queira rodar a aplicação através do Visual Studio ou alguma outra IDE, basta ter o SQL Server instalado, 
configurar a connectionString e o banco de dados e as tabelas serão criados automatico atrávez da Migration configurada.  

