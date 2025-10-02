Teste Pratico - Desenvolvedor Fullstack
Este repositorio contem a implementacao de uma aplicacao Fullstack minima para um processo seletivo, compreendendo uma API em .NET 8 e um Frontend em Angular.

Estrutura do Projeto
O projeto esta organizado em duas pastas principais para uma clara separacao de responsabilidades:

/backend: Contem a solucao .NET com a API, projetos de dominio, aplicacao, infraestrutura e testes.

/frontend: Contem a aplicacao Angular.

Funcionalidades Implementadas
A aplicacao cumpre os seguintes requisitos do teste:

Backend (.NET 8):

POST /users: Endpoint para cadastro de um novo usuario (nome e e-mail obrigatorios, com e-mail unico).

GET /users: Endpoint para listar os usuarios cadastrados.

Persistencia de dados com Entity Framework Core e SQLite.

Arquitetura com principios de Clean Architecture e CQRS.

Testes automatizados para o handler de cadastro de usuario.

Frontend (Angular):

Tela para listagem dos usuarios cadastrados.

Tela para cadastro de um novo usuario.

Estrutura organizada para futuras implementacoes.

Infelizmente a integracao do cadastro funcional nao foi executada com exito, devido a uma falta de pratica da minha parte com o Angular.

Tecnologias Utilizadas
Backend:

.NET 8

ASP.NET Core (Minimal APIs)

Entity Framework Core 8

SQLite

xUnit (para testes)

Frontend:

Angular

TypeScript

Angular CLI

Pre-requisitos
Antes de comecar, garanta que voce tenha os seguintes softwares instalados:

.NET 8 SDK

Node.js (versao LTS recomendada)

Angular CLI (npm install -g @angular/cli)

Como Executar a Aplicacao
Siga os passos abaixo para rodar a aplicacao em seu ambiente local.

1. Executando o Backend (API)
Bash

# 1. Navegue para a pasta do backend
cd backend

# 2. Restaure as dependencias do .NET
dotnet restore

# 3. Navegue para a pasta da API
cd WebAPI

# 4. Inicie o servidor da API
dotnet run
O terminal indicara que a API esta rodando, geralmente em https://localhost:7123. A base de dados users.db sera criada automaticamente no primeiro uso.

2. Executando o Frontend (Angular)
Bash

# 1. Em um NOVO terminal, navegue para a pasta do frontend
cd frontend

# 2. Restaure as dependencias do Node.js
npm install

# 3. Inicie o servidor de desenvolvimento do Angular
ng serve
A aplicacao frontend estara disponivel em http://localhost:4200/.

Executando os Testes Automatizados
Os testes de unidade do backend foram implementados para validar a logica do handler de cadastro de usuario.

Bash

# 1. Navegue para a pasta do backend
cd backend

# 2. Execute o comando de teste do .NET
dotnet test