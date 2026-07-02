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


