# API REST – .NET 8 com Autenticação JWT

API simples desenvolvida em ASP.NET Core 8, com autenticação via JWT e acesso a banco de dados MySQL usando Entity Framework Core.

---

## Tecnologias

- .NET 8 (ASP.NET Core)
- Entity Framework Core
- MySQL
- JWT (JSON Web Token)
- CORS habilitado
- RESTful API

---

## Clonando o projeto

```bash
git clone https://github.com/carlosrodr1/api-rest-dotnet8.git
cd api-rest-dotnet8
dotnet restore
dotnet ef database update
dotnet run
