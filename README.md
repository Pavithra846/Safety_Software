# NotifyHub API

NotifyHub API is a backend notification service built with ASP.NET Core Web API.  
It is part of the Safety Software System and provides centralized notification handling for alerts, system messages, and user communications.

The service supports notification creation, storage, and delivery through REST APIs, with support for real-time communication using SignalR.

## Features

- Send and manage system notifications
- Centralized notification service for the Safety Software System
- RESTful API design
- Layered architecture: Controller, Service, Data Access
- SQL Server database integration
- Entity Framework Core support
- Swagger API documentation
- Real-time notification support using SignalR

## Tech Stack

- ASP.NET Core Web API
- C#
- SQL Server
- Entity Framework Core
- SignalR
- Swagger / OpenAPI

## Project Structure

NotifyHub.Api
│
├── Controllers        # API endpoints
├── Services           # Business logic
├── Models / DTOs      # Data models and request/response objects
├── Data Access Layer  # Database context and repository logic
├── appsettings.json   # Application configuration
└── Program.cs         # Application startup configuration

1. Clone the Repository
   git clone https://github.com/Pavithra846/Safety_Software.git
2. Open the Project
   Open the NotifyHub.Api project in Visual Studio.
3. Configure the Database
   Update the SQL Server connection string in appsettings.json.
  "ConnectionStrings": {
    "DefaultConnection": "your-sql-server-connection-string"
  }
4. Apply Database Migrations
   dotnet ef database update
5. Run the Project
   dotnet run
6. Open Swagger
   After running the project, open:
   https://localhost:{port}/swagger

 API Overview
Example endpoints:
Method	Endpoint	                Description
POST	  /api/notifications	      Send a new notification
GET	    /api/notifications	      Get all notifications
GET	    /api/notifications/{id}	  Get notification by ID

## Key Highlights

- Clean layered architecture
- Scalable notification system
- Real-time notification support
- SQL Server-backed persistence
- Enterprise-ready API structure

## Future Enhancements

- User-specific notification delivery
- Notification read/unread status
- Email or SMS notification integration
- Authentication and authorization
- Notification priority levels
