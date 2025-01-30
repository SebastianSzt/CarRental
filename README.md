# CarRental

CarRental is a web application for renting cars. The project is built using ASP.NET Core MVC for the web interface and a separate API for handling backend operations. The application uses a SQL Server database to store data.

## Technologies Used

- ASP.NET Core MVC
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server

## Getting Started

To get started with the project, follow the steps below:

### Prerequisites

- .NET 6 SDK
- SQL Server
- Visual Studio or similar application

### Clone the Repository

Clone the repository from GitHub:
git clone https://github.com/SebastianSzt/CarRental.git

### Set Up the Database

1. Create a new database in SQL Server.
2. Update the connection string in `CarRental.Api/appsettings.json` with your database details:
{ "ConnectionStrings": { "AppDbContext": "Data Source=your_connection_string_data_source;Initial Catalog=CarRental;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;MultipleActiveResultSets=True" }

### Run the Application

1. Open the solution in Visual Studio or a similar application.
2. Set the `CarRental.Api` and `CarRental.Web` projects as startup projects.
3. Run both projects simultaneously.

The API will be accessible at `https://localhost:7074` and the web application at `https://localhost:7233`.

### Seed the Database

The application includes a seed data mechanism to populate the database with initial data. This will run automatically when the API project is started.

## Project Structure

- `CarRental.Api`: The backend API project.
- `CarRental.Web`: The frontend web application project.
- `CarRental.Model`: Contains the data models and database context.
- `CarRental.Repository`: Contains the repository classes for data access.
- `CarRental.Dto`: Contains the Data Transfer Objects (DTOs) used for communication between the API and the web application.

## License

This project is licensed under the MIT License.
