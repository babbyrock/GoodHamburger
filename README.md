# Good Hamburger API

API REST desenvolvida em **ASP.NET Core / .NET** para gerenciamento de pedidos da lanchonete **Good Hamburger**.

O projeto foi construído com foco em **boas práticas de arquitetura, código limpo, separação de responsabilidades e facilidade de manutenção**.

---

# 📌 Sobre o Projeto

A aplicação permite o gerenciamento completo de pedidos da lanchonete, realizando cadastro, consulta, atualização e remoção de pedidos, além do cálculo automático de subtotal, descontos e total final conforme as regras propostas no desafio.

Também foi desenvolvido um **frontend em Blazor**, consumindo a API REST, permitindo interação visual com o sistema para cadastro e acompanhamento dos pedidos de forma simples e intuitiva.

---

# 📌 Arquitetura Utilizada

O projeto foi estruturado utilizando conceitos de **Clean Architecture**, **Domain-Driven Design (DDD)** e **CQRS (Command Query Responsibility Segregation)**, separando responsabilidades em camadas independentes.

```text id="mvalim"
API
Application
Domain
Infrastructure
Tests
Blazor
```

---

# 📌 Funcionalidades

* CRUD completo de pedidos
* Consulta de cardápio
* Cálculo automático de subtotal
* Aplicação automática de descontos
* Tratamento de erros e validações
* Interface Web em Blazor integrada com a API

---

# 🧾 Regras de Desconto

| Combinação                        | Desconto |
| --------------------------------- | -------- |
| Sanduíche + Batata + Refrigerante | 20%      |
| Sanduíche + Refrigerante          | 15%      |
| Sanduíche + Batata                | 10%      |

---

# 🐳 Como Executar com Docker

## 1. Configure o arquivo `.env`

Crie um arquivo `.env` na raiz do projeto:

```env id="kjxzy0"
SA_PASSWORD=SuaSenha@123
MSSQL_PID=Developer
ACCEPT_EULA=Y
```

---

## 2. Subir banco

```bash id="otmmip"
docker compose up -d
```

---

# 🔌 Connection String

No `appsettings.json` da API:

```json id="fb1fsc"
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=GoodHamburgerDb;User Id=sa;Password=SuaSenha@123;TrustServerCertificate=True;Encrypt=False;"
}
```

---

# ▶️ Como Executar o Projeto

## Executar tudo de uma vez (Visual Studio)

Defina múltiplos projetos de inicialização:

* GoodHamburger.API
* GoodHamburger.Blazor

Depois pressione:

```text id="aa1020"
F5
```

---

## Executar por partes

### 1. API

```bash id="bb2030"
cd GoodHamburger.API
dotnet run
```

Swagger:

```text id="cc3040"
https://localhost:xxxx/swagger
```

---

### 2. Blazor

```bash id="dd4050"
cd GoodHamburger.Blazor
dotnet run
```

---

# 💻 Executar sem Docker

Caso utilize SQL Server instalado localmente:

```json id="ikhs87"
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=GoodHamburgerDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

---

# 🧪 Testes

```bash id="l21r5t"
dotnet test
```
