# Carteira Digital

API ASP.NET Core.
Persiste os dados em SQL Server.
Modelagem de software Domain Driven Design (DDD).

Uma Api dividida em 3 controllers.

1 - Usuario (User) 

* Cadastro de Usuario (Register)  
Para se cadastrar basta enviar seus dados, nome completo, CPF, data de nascimento, senha e o país de origem.
Nome deverá ser completo.
Deve ser enviado um CPF válido.
Obrigatório ser maior de idade, caso o país de origem for EUA deverá possuir no mínimo 21 anos, caso contrário 18.
Senha deve possuir entre 8 a 20 caracteres e sem espaços vazios.
País é um enumerador sendo Brasil = 0 e EUA = 1.

2 - Funcionalidades da conta (Account)

* Deposito (Deposit): Basta enviar seu CPF, senha cadastrados e o valor que deseja depositar.
* Saque (Withdraw): Basta enviar seu CPF, senha e o valor que deseja sacar.
* Transferência (Transfer): Basta enviar seu CPF, senha, valor da operação e o CPF do beneficiário.
* Saldo (Balance): Basta enviar seu CPF e senha para obter seu saldo atual.

3 - Movimentação da conta (Movement)

* Extrato de conta (History): Basta enviar seu CPF, senha, data inicial e final do período desejado. 

