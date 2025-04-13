# Seer System Management MVC

A comprehensive ASP.NET Core MVC application for managing seers and their services. This system provides a platform for managing fortune-tellers, psychics, and their client enquiries.

## 🚀 Features

- User authentication and role-based authorization
- Seer profile management
- Enquiry handling system
- Administrative dashboard
- Client request management

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core MVC
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: ASP.NET Core Identity
- **Frontend**: Razor Views, Bootstrap

## 🏗️ Project Structure

- `Controllers/` - MVC controllers handling request logic
- `Models/` - Domain models and view models
- `Views/` - Razor views for the user interface
- `Services/` - Business logic implementation
- `Data/` - Database context and migrations
- `Interfaces/` - Service contracts
- `wwwroot/` - Static files (CSS, JS, images)

## 🔧 Setup

1. Ensure you have the following prerequisites:
   - .NET Core SDK
   - SQL Server
   
2. Update the connection string in `appsettings.json`
3. Run database migrations:
   ```
   dotnet ef database update
   ```
4. Run the application:
   ```
   dotnet run
   ```

## 🔐 Authentication

The system uses ASP.NET Core Identity for authentication and supports role-based authorization with the following roles:
- Admin
- Seer
- Client

## 📝 Core Features

- **Enquiry Management**: Handle client requests and consultations
- **Seer Profiles**: Manage seer information and availability
- **Administrative Tools**: User management and system monitoring

## 🤝 Contributing

Feel free to submit issues and enhancement requests. 