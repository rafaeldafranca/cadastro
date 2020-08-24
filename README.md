# Desafio Cadastro de Usuários

No meu desafio é um cadastro de usuários via Api onde um usuário possa criar uma conta e gerar um token de autenticação para utilizar o serviço de perfil.
	
*O que é preciso para rodar o projeto ?*

	- Visual Studio 2019
	- Docker para testes de containers (opcional)
	- NetCore 3.1
	- Postman, Insomnia ou outro sofware de requisições de API. (opcional se for utilizar o swagger)
	
**Colocando para funcionar**

1) Via Docker
 
	- Verifique se o docker se encontra ativo.
	- Entre no prompt de comando, acesse a **pasta raiz do projeto onde fica a solução** e crie uma imagem local do projeto.
		> docker build -t cadastro-img -f Dockerfile .
	- Inicie a imagem na porta 8080
		> docker run -d -p 8080:80 --name cadastro cadastro-img
	- Abra o navegador e entre no link http://localhost:8080/swagger ou utilize um programa para realizar os testes.

2) Via projeto pelo Visual Studio
	
	Abra o projeto no Visual Studio e rode a api apertando F5.
	Aguarde a compilação e o download de pacotes Nuget.
	Ele entrará automaticamente na página do Swagger podendo utilizar normalmente em programas de terceiros.

**Endpoints** ( *Utilizando a porta 8080 como exemplo.* )
	
	- Gerar token de acesso
		POST http://localhost:8080/token  
		{
		  "email": "string",
		  "password": "string"
		}
		
	- Criar um usuário novo
	POST http://localhost:8080/NewUser  
		{
		  "name": "string",
		  "email": "string",
		  "password": "string",
		  "phones": [
		    {
		      "ddd": "string",
		      "number": "string"
		    }
		  ]
		}
		
	- Buscar todos os usuários cadastrados ( Bearer authentication )
	GET http://localhost:8080/GetAll 
	
	- Retornar os dados do usuário logado ( Bearer authentication )
	GET http://localhost:8080/Profile
	
	- Retornar os dados do usuário logado passando a sua ID ( Bearer authentication )
	GET http://localhost:8080/Profile/{id}

**Considerações**

No desafio, o banco de dados seria um banco de memória (InMemory) criado pelo próprio EntityFramework e as Queries feitas em Dapper.
Dapper não aceita bancos de memória e fiz uma adaptação para incluir no desafio esta tarefa.
As consultas no repositório estão rodando em EF mas estão as de dapper comentadas junto ao projeto. Elas podem ser utilizadas porém deverá usar 
um banco real para isso.
Fiz uma configuração para que, se necessário a visualização via dapper, rodar o migrations em um banco de teste e assim 
descomentar as linhas para que utilize esse micro ORM.

- Como fazer?
	- No projeto principal, editar o arquivo StartUp.cs e comentar a configuração InMemory e utilizar a UseSqlServer
	- Ainda no projeto principal editar o appSettings.Json e configurar um banco de dados para SqlServer.
  - Entrar no console do Nuget Manager dentro do Visual Studio no menu *Tools > Nuget Package Manager > Package Manager Console*
  - Criar um script de banco de dados via migrations 
  > add-migration script -Project Cadastro.Core -Context PrincipalContext
  - Executar o script criado para dentro do banco de dados configurado no appSettings.json.
  > update-database -Project Cadastro.Core -Context PrincipalContext
	- Ir na parte de repositórios dentro do projeto *Cadastro.Core* e descomentar as funcões dapper.
	
**Testes**

Como item desejável, fiz alguns casos de teste usando um outro banco de memória gerado na inicialização dos mesmos.
Os testes são basicamentes focados no EF e suas queries/persistências.

**Uma visão sobre as tecnologias**

	- Linguagem
	 C#
	- Plataformas
	 Docker
	- Padrões
	 JWT
	 JSON
	- Arquiteturas
	 API RESTful
	- Frameworks
	 .NetCore {json:api}
	 Entity Framework Core
	- Ferramentas
	 Swagger
	- Soluções
	 inMemory
