# 📚 BookStore Web API

A complete **Monolithic .NET 8 Web API** for a BookStore system, built using **Clean Architecture principles**, **Repository Pattern**, and **Dependency Injection**.

---

# ✨ Features

- 🔐 **JWT Authentication**
  - User Registration
  - Login
  - Customer & Admin Roles

- 🛡️ **Role-Based Authorization**
  - Admin-only endpoints
  - Secure access using JWT Bearer Authentication

- 📚 **Book Management**
  - CRUD Operations
  - Search
  - Filtering
  - Pagination

- 🛒 **Order Management**
  - Place Orders
  - View Order History

- 🗄️ **Repository Pattern**
  - Separate Data Access Layer
  - Clean and maintainable code

- ⚠️ **Global Exception Handling**
  - Custom Middleware
  - Consistent JSON error responses
  - Internal server details are hidden

- 🌐 **CORS Enabled**
  - Ready for frontend applications
  - Supports localhost development

---


- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger / OpenAPI
- Dependency Injection
- Repository Pattern

---

# 📋 Prerequisites

Before running the project, make sure you have installed:

- .NET 8 SDK (or later)
- SQL Server

---

# 🚀 Getting Started

## 1. Clone the Repository

```bash
git clone <your-repository-url>
cd BookStoreAPI
```

---

## 2. Configure the Database

Open:

```
appsettings.json
```

Update:

- `ConnectionStrings:DefaultConnection`
- `Jwt:Key`

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=BookStoreDB;Trusted_Connection=True;TrustServerCertificate=True;"
},

"Jwt": {
  "Key": "YourStrongSecretKey"
}
```

---

## 3. Apply Database Migrations

Using Package Manager Console:

```powershell
Update-Database
```

Or using the .NET CLI:

```bash
dotnet ef database update
```

---

## 4. Run the Application

```bash
dotnet run
```

Swagger will be available at:

```
https://localhost:<port>/swagger
```

---

# 👤 Create the First Admin

To bootstrap the application with an administrator account:

1. Open Swagger.
2. Go to:

```
POST /api/auth/register-admin
```

3. Send the following request:

```json
{
  "fullName": "System Admin",
  "email": "admin@bookstore.com",
  "password": "StrongPassword123!"
}
```

The user will automatically receive the **Admin** role.

---

# 🔑 Authentication

## Login

Call:

```
POST /api/auth/login
```

Example:

```json
{
  "email": "admin@bookstore.com",
  "password": "StrongPassword123!"
}
```

Response:

```json
{
  "token": "your-jwt-token"
}
```

---

## Authorize in Swagger

1. Copy the JWT token.
2. Click **Authorize** in Swagger.
3. Paste the token.
4. Click **Authorize**.

You can now access all protected endpoints.

> **Note:** Admin endpoints require an authenticated user with the **Admin** role.

---

# 📂 Project Structure

```
BookStoreAPI
│
├── Controllers
├── Models
├── DTOs
├── Repositories
├── Interfaces
├── Services
├── Data
├── Middleware
├── Migrations
├── Helpers
└── Program.cs
```

---

# 🔒 Security

- JWT Bearer Authentication
- Password Hashing
- Role-Based Authorization
- Global Exception Middleware
- Secure API Responses

---

# 📖 API Documentation

Swagger UI is enabled by default.

```
https://localhost:<port>/swagger
```

---

# 📌 Main Features

- User Registration
- User Login
- Admin Registration
- JWT Authentication
- CRUD Books
- Search Books
- Filter Books
- Pagination
- Order Placement
- Role-Based Authorization
- Global Exception Handling

---
