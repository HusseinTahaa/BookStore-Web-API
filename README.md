# BookStore Web API 📚

A complete monolithic .NET 8/10 Web API for a BookStore system, built using clean principles, Repository Pattern, and Dependency Injection.

## Features
- **User Authentication:** JWT-based registration and login (Customer & Admin roles).
- **Global Exception Handling:** Custom middleware to catch exceptions and return structured JSON responses without exposing internal server details.
- **Repository Pattern:** Separated Data Access Layer for all entities.
- **Products & Orders:** Browse books (with search, filtering, and pagination), manage inventory, and place orders.
- **Role-Based Access Control:** Strict authorization for admin-only endpoints.
- **CORS Enabled:** Ready to be consumed by any modern frontend framework running on localhost.

## Prerequisites
- .NET 8.0 SDK (or later)
- SQL Server

## How to Run the Project

1. **Clone the repository:**
   ```bash
   git clone <your-repo-url>
   cd BookStoreAPI

   Configure the Database:

Open appsettings.json and update the DefaultConnection string with your SQL Server instance details.

Update the Jwt:Key with a strong secret key if needed.

Apply Migrations:
Open the Package Manager Console (or terminal) and run:

Bash
Update-Database
Run the API:

Bash
dotnet run
The API will start, and you can navigate to the Swagger UI (usually https://localhost:<port>/swagger).

How to Register the First Admin
To bootstrap the system with an initial Admin user, use the provided admin-specific endpoint:

Open Swagger UI.

Locate the POST /api/auth/register-admin endpoint.

Provide the required payload:

JSON
{
  "fullName": "System Admin",
  "email": "admin@bookstore.com",
  "password": "StrongPassword123!"
}
Execute the request. The user will be created and automatically assigned the Admin role.

How to Test the API (Using Swagger)
Login:
Use the POST /api/auth/login endpoint with your registered credentials.

Copy the Token:
From the response body, copy the token string.

Authorize:
Click the green "Authorize" button at the top of the Swagger page. Paste your token directly into the input field and click "Authorize".

Test Endpoints:
You can now test all secured endpoints. Endpoints requiring Admin role (like Create/Update Book) will only work if you logged in with an Admin account.